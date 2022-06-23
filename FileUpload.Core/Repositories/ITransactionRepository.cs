using FileUpload.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Core.Repositories
{
    public interface ITransactionRepository : IRepository<FileUpload.Core.Entities.Transaction>
    {
        //custom operations here
        Task<IEnumerable<FileUpload.Core.Entities.Transaction>> GetEmployeeByLastName(string lastname);
    }
}
