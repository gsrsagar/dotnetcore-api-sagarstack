using System;
using System.Collections.Generic;
using System.Text;

namespace api_web_new.Infrastructure.Repositories.Interfaces
{
    interface IRepository<T>
    {

        public Task<T> GetById(int id);
        public Task<List<T>> GetAll();
        public Task<T> Add(T entity);
        public Task<T> Update(T entity, int id);

        public Task<bool> Delete(int id);
    }
}
