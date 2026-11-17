using System;
using System.Collections.Generic;
using System.Text;

namespace api_web_new.Application.Interfaces
{
    public interface IEmployeeService<T,U>
    {
        public Task<T> GetById(int id);
        public Task<List<T>> GetAll();
        public Task<T> Add(U entity);
        public Task<T> Update(U entity, int id);

        public Task<bool> Delete(int id);
    }
}
