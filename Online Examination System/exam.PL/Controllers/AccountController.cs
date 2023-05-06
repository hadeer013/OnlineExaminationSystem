using AutoMapper;
using exam.BLL.Interfaces;
using exam.BLL.Interfaces.token;
using exam.DAL.Entities;
using exam.DAL.Entities.ProfessorEntities;
using exam.PL.Dtos;
using exam.PL.Helper.Email;
using exam.PL.Helper.Password_generator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Numerics;

namespace exam.PL.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IGenericRepository<ProfessorSignUpRequests> profSignupReqRepo;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager
            ,IGenericRepository<ProfessorSignUpRequests> ProfSignupReqRepo,ITokenService tokenService,IMapper mapper,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            profSignupReqRepo = ProfSignupReqRepo;
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (result.Succeeded)
                {
                    var token = await tokenService.CreateToken(user, userManager);
                    return new UserDto()
                    {
                        UserName = user.UserName,
                        Email = loginDto.Email,
                        Token = token
                    };
                }

            }
            return Unauthorized();
        }


        [Authorize(Roles ="Admin")]
        [HttpGet("AllSignupRequests")]
        public async Task<ActionResult<IReadOnlyList<ProfessorRequestsDto>>> GetAllSignUpRequests()
        {
            var result = await profSignupReqRepo.GetAllAsync();
            var objReturn = result.Where(p => p.IsApproved==false).ToList();
            var mapped = mapper.Map<IReadOnlyList<BaseApplicationUserDto>>(objReturn);
            return Ok(mapped);
        }


        [HttpGet("approveSignUpRequest/{Id}")]
        public async Task<ActionResult> ApproveSignUpRequest(int Id)
        {
            var request=await profSignupReqRepo.GetByIdAsync(Id);
            if (request == null) return NotFound();
            request.IsApproved = true;
            await profSignupReqRepo.Update(request);
            if (CheckEmailExistsAsync(request.Email).Result.Value) return BadRequest();
                //return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = new[] { "Email Address already is in Use !!" } });
            var password = PasswordGeneration.Generator();
            var prof = new Professor()
            {
                Email = request.Email,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                SSN = request.SSN,
                UserName = request.UserName
            };
            var result = await userManager.CreateAsync(prof, password);
            if(result.Succeeded)
            {
                var email = new Email()
                {
                    Subject = "Online Examination System < SignUp Request Approval >",
                    Body = $"Please use this password in login process : {password}",
                    To = prof.Email
                };
                EmailSettings.SendEmail(email);
                var role = await roleManager.FindByNameAsync("Professor");
                var added = await userManager.AddToRoleAsync(prof, role.Name);
                if (added.Succeeded)
                {
                    var user = new BaseUserDto()
                    {
                        Email = prof.Email,
                        UserName = prof.UserName
                    };
                    return Ok(user);
                }
            }
            return BadRequest(result.Errors);
        }
       
       
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }
    }
}
