using AutoMapper;
using FileUpload.Application.Commands;
using FileUpload.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Application.Mapper
{
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<FileUpload.Core.Entities.Transaction, TransactionResponse>().ReverseMap();
            CreateMap<FileUpload.Core.Entities.Transaction, UploadTransactionCommand>().ReverseMap();
        }
    }
}
