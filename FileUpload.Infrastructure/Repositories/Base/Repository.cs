using FileUpload.Core.Repositories.Base;
using FileUpload.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TransactionContext _transactionContext;

        public Repository(TransactionContext employeeContext)
        {
            _transactionContext = employeeContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _transactionContext.Set<T>().AddAsync(entity);
            await _transactionContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _transactionContext.Set<T>().Remove(entity);
            await _transactionContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _transactionContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _transactionContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
