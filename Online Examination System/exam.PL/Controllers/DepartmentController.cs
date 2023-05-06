using AutoMapper;
using exam.BLL.Interfaces;
using exam.DAL.Entities;
using exam.PL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.PL.Controllers
{
    public class DepartmentController : BaseApiController
    {
        private readonly IGenericRepository<Department> departmentRepo;
        private readonly IMapper mapper;

        public DepartmentController(IGenericRepository<Department> departmentRepo,IMapper mapper)
        {
            this.departmentRepo = departmentRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Department>>> GetAllDepartments()
        {
            var result = await departmentRepo.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var department = await departmentRepo.GetByIdAsync(id);
            if(department == null) return NotFound();
            return Ok(department);
        }
        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment(AddBaseEntityWithNameDto department)
        {
            var mappedDep=mapper.Map<Department>(department);
            await departmentRepo.Add(mappedDep);
            return Ok(mappedDep);
        }
        [HttpPut]
        public async Task<ActionResult<Department>>UpdateDepartment(Department department)
        {
            await departmentRepo.Update(department);
            return Ok(department);
        }
        [HttpDelete]
        public async Task<ActionResult<Department>>DeleteDepartment(int departmentId)
        {
            var department = await departmentRepo.GetByIdAsync(departmentId);
            if (department == null) return NotFound();
            await departmentRepo.Delete(department);
            return Ok(department);
        }
    }
}
