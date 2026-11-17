using api_web_new.Application.Interfaces;
using api_web_new.csproj.Domain.DTO.Request;
using api_web_new.csproj.Domain.Entities;
using api_web_new.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace api_web_new.Application.Services
{
    public class EmployeeService : IEmployeeService<Employee, EmployeeRequestDTO>
    {

        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Add(EmployeeRequestDTO entity)
        {
            Employee e = new Employee
            {
                Id = _employeeRepository.GetNextId(),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Departments  = new List<Department>()

            };

            return _employeeRepository.Add(e);
        }

        public Task<bool> Delete(int id)
        {
            return _employeeRepository.Delete(id);
        }

        public Task<List<Employee>> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public Task<Employee> GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public Task<Employee> Update(EmployeeRequestDTO entity, int id)
        {
            Employee employee = new Employee
            {
                Id = id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Departments = new List<Department>()
            };
            return _employeeRepository.Update(employee,id);
        }

        
    }
}
