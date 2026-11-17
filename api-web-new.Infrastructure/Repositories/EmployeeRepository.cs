using api_web_new.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using api_web_new.csproj.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_web_new.Infrastructure.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {

        private readonly DbContext _context;

        public EmployeeRepository(DbContext context)
        {
          _context = context;
        }

        public Task<Employee> Add(Employee entity)
        {
            return _context.Set<Employee>().AddAsync(entity).AsTask().ContinueWith(t =>
            {
                _context.SaveChanges();
                return t.Result.Entity;
            });
        }

        public Task<bool> Delete(int id)
        {
          return 
               _context.Set<Employee>().FindAsync(id).AsTask().ContinueWith(t =>
                {
                    if (t.Result == null)
                    {
                        return false;
                    }
                    _context.Set<Employee>().Remove(t.Result);
                    _context.SaveChanges();
                    return true;
                }); 
        }

        public Task<List<Employee>> GetAll()
        {
            return _context.Set<Employee>().ToListAsync();
        }

        public Task<Employee?> GetById(int id)
        {
            return _context.Set<Employee>().FindAsync(id).AsTask();
        }

        public Task<Employee?> Update(Employee entity, int id)
        {
            return _context.Set<Employee>().FindAsync(id).AsTask().ContinueWith(t =>
            {
                if (t.Result == null)
                {
                    return null;
                }
                t.Result.FirstName = entity.FirstName;
                t.Result.LastName = entity.LastName;
                t.Result.Departments = entity.Departments;
                _context.Set<Employee>().Update(t.Result);
                _context.SaveChanges();
                return t.Result;
            }); 
        }

        public int GetNextId()
        {
            var lastEmployee = _context.Set<Employee>().OrderByDescending(e => e.Id).FirstOrDefault();
            return lastEmployee != null ? lastEmployee.Id + 1 : 1;
        }
    }
}
