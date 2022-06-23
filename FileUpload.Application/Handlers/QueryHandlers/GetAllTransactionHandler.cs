using FileUpload.Application.Mapper;
using FileUpload.Application.Queries;
using FileUpload.Application.Responses;
using FileUpload.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileUpload.Application.Handlers.QueryHandlers
{
    public class GetAllTransactionHandler : IRequestHandler<GetAllTransactionQuery, List<TransactionResponse>>
    {
        private readonly ITransactionRepository _transactionRepo;

        public GetAllTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepo = transactionRepository;
        }
        public async Task<List<TransactionResponse>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepo.GetAllAsync();
            var transactionResponseList = new List<TransactionResponse>();
            foreach (var transaction in transactions)
            {
                transactionResponseList.Add(new TransactionResponse()
                {
                    Id = transaction.TransactionId,
                    Payment = $"{transaction.Amount} {transaction.CurrencyCode}",
                    Status = StatusMapping(transaction.Status)
                });
            }
            return transactionResponseList;
        }

        public string StatusMapping(string status)
        {
            switch(status)
            {
                case "Approved":
                    return "A";
                case "Failed":
                case "Rejected":
                    return "R";
                case "Finish":
                case "Done":
                    return "D";
            }

            return null;
        }
    }
}
