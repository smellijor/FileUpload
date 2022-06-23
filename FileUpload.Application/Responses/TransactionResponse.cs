using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Application.Responses
{
    public class TransactionResponse
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
    }
}
