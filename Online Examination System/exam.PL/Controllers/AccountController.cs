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
        private readonly IGenericRepository<ProfessorSignUpRequests> profSignupRequestRepo;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager
            ,IGenericRepository<ProfessorSignUpRequests> ProfSignupReqRepo,ITokenService tokenService,
            IMapper mapper,RoleManager<IdentityRole> roleManager, IGenericRepository<ProfessorSignUpRequests> profSignupRequestRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            profSignupReqRepo = ProfSignupReqRepo;
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.profSignupRequestRepo = profSignupRequestRepo;
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

        [HttpPost("requestToSignUp")]
        public async Task<ActionResult<BaseApplicationUserDto>> RequestToSignUp(SignUpDto signUpDto)
        {
            var mapped = mapper.Map<ProfessorSignUpRequests>(signUpDto);
            await profSignupRequestRepo.Add(mapped);
            var obj = mapper.Map<BaseApplicationUserDto>(signUpDto);
            return Ok(obj);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("approveSignUpRequest")]
        public async Task<ActionResult> ApproveSignUpRequest(ApproveSignUpRequestDto approveRequest)
        {
            var request=await profSignupReqRepo.GetByIdAsync(approveRequest.RequestId);
            if (request == null) return NotFound();
            request.IsApproved = approveRequest.IsApproved;
            
            if (approveRequest.IsApproved)
            {
                await profSignupReqRepo.Update(request);
                if (CheckEmailExistsAsync(request.Email).Result.Value) return BadRequest();
                
                var prof = new Professor()
                {
                    Email = request.Email,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    UserName = request.UserName
                };
                var result = await userManager.CreateAsync(prof, request.Password);
                if (result.Succeeded)
                {
                    var email = new Email()
                    {
                        Subject = "Online Examination System < SignUp Request Approval >",
                        Body = $"Your Request has been approved.Now,you can login to the system",
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
            await profSignupReqRepo.Delete(request);
            var declinedemail = new Email()
            {
                Subject = "Online Examination System < SignUp Request >",
                Body = $"Your Request has been declined.Please make sure your data is correct and try again!!",
                To = request.Email
            };
            EmailSettings.SendEmail(declinedemail);
            var userdec = new BaseUserDto()
            {
                Email = request.Email,
                UserName = request.UserName
            };
            return Ok(userdec);
        }

        [HttpPost("studentSignup")]
        public async Task<ActionResult> StudentSignUp(StudentSignupDto studentSignupDto)
        {
            if (CheckEmailExistsAsync(studentSignupDto.Email).Result.Value) return BadRequest();
            var Student = new Student()
            {
                UserName = studentSignupDto.userName,
                Email = studentSignupDto.Email,
                PhoneNumber = studentSignupDto.PhoneNumber,
                Code = studentSignupDto.Code,
                Address = studentSignupDto.Address,
                DepartmentId = studentSignupDto.DepartmentId,
                LevelId = studentSignupDto.LevelId,
            };

            var result = await userManager.CreateAsync(Student, studentSignupDto.Password);
            if (result.Succeeded)
            {
                var role = await roleManager.FindByNameAsync("Student");
                var added = await userManager.AddToRoleAsync(Student, role.Name);
                if (added.Succeeded)
                {
                    var user = new BaseUserDto()
                    {
                        Email = Student.Email,
                        UserName = Student.UserName
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
