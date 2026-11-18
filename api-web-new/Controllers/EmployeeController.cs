using api_web_new.Application.Interfaces;
using api_web_new.csproj.Domain.DTO.Request;
using api_web_new.csproj.Domain.Entities;
using api_web_new.csproj.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_web_new.Controllers
{
   
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v2/employee
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService<Employee, EmployeeRequestDTO> _employeeService;
        private readonly IEmployeeValidator _validator;
        public EmployeeController(IEmployeeService<Employee, EmployeeRequestDTO> employeeService,
             IEmployeeValidator validator
            )
        {
            _employeeService = employeeService;
            _validator = validator;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public Task<List<Employee>> Get()
        {
            return _employeeService.GetAll();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Task<Employee> Get(int id)
        {
            return _employeeService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeRequestDTO value)
        {
            var validated = _validator.Validate(value);

            if (!(validated != null && validated != ValidationResult.Success))
            {
                var result = await _employeeService.Add(value);
                return Ok(result); // 200 OK
            }

            return BadRequest(new { message = validated.ErrorMessage }); // 400 Bad Request
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public Task<Employee> Put(int id, [FromBody] EmployeeRequestDTO value)
        {
            return _employeeService.Update(value,id);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public Task<bool> Delete(int id)
        {
            return _employeeService.Delete(id);
        }
    }
}
