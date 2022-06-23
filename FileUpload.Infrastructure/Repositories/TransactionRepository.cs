using FileUpload.Core.Repositories;
using FileUpload.Infrastructure.Data;
using FileUpload.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Infrastructure.Repositories
{
    public class TransactionRepository : Repository<FileUpload.Core.Entities.Transaction>, ITransactionRepository
    {
        public TransactionRepository(TransactionContext transactionContext) : base(transactionContext)
        {

        }
        public async Task<IEnumerable<Core.Entities.Transaction>> GetEmployeeByLastName(string lastname)
        {
            return await _transactionContext.Transactions
                .Where(m => m.CurrencyCode == lastname)
                .ToListAsync();
        }
    }
}
