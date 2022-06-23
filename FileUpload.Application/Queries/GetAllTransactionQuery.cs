using FileUpload.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Application.Queries
{
    public class GetAllTransactionQuery : IRequest<List<TransactionResponse>>
    {
        public string Currency { get; set; }
        public DateTime? Datefrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Status { get; set; }
    }
}
