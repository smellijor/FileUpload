using FileUpload.Application.Commands;
using FileUpload.Application.Mapper;
using FileUpload.Application.Responses;
using FileUpload.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace FileUpload.Application.Handlers.CommandHandlers
{

    public class UploadTransactionHandler : IRequestHandler<UploadTransactionCommand, Boolean>
    {
        private readonly ITransactionRepository _transactionRepo;

        public UploadTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepo = transactionRepository;
        }
        public async Task<Boolean> Handle(UploadTransactionCommand request, CancellationToken cancellationToken)
        {
            long size = request.File.Length / (1024 * 1024);
            if (size > 1)
                throw new ApplicationException("File Max size is 1MB");

            var sr = new StreamReader(request.File.OpenReadStream());
            if (request.File.ContentType == "text/csv")
            {
                return await ProcessCSV(sr);
            } 
            else if (request.File.ContentType == "text/xml")
            {
                return await ProcessXML(sr);
            }

            throw new ApplicationException("Unknown Format");
        }

        public async Task<bool> ProcessCSV(StreamReader sr)
        {
            try
            {
                string line;
                string[] row = new string[6];
                while ((line = sr.ReadLine()) != null)
                {
                    row = line.Split("\",\"");
                    var row1 = line.Split(",");
                    var transaction = new FileUpload.Core.Entities.Transaction
                    {
                        TransactionId = row[0].Replace("\"", ""),
                        Amount = decimal.Parse(row[1]),
                        CurrencyCode = row[2],
                        TransactionDate = DateTime.Parse(row[3]),
                        Status = row[4].Replace("\"", "")
                    };

                    await _transactionRepo.AddAsync(transaction);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ProcessXML(StreamReader sr)
        {
            try
            {
                var reader = XmlReader.Create(sr);
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(reader);

                var transactions = xDoc.GetElementsByTagName("Transaction");
                foreach (XmlNode _transaction in transactions)
                {
                    var transaction = new FileUpload.Core.Entities.Transaction
                    {
                        TransactionId = _transaction.Attributes[0].InnerText,
                        Amount = decimal.Parse(_transaction.ChildNodes[1].ChildNodes[0].InnerText),
                        CurrencyCode = _transaction.ChildNodes[1].ChildNodes[1].InnerText,
                        TransactionDate = DateTime.Parse(_transaction.ChildNodes[0].InnerText),
                        Status = _transaction.ChildNodes[2].InnerText
                    };

                    await _transactionRepo.AddAsync(transaction);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
