using FileUpload.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace FileUpload.Application.Commands
{
    public class UploadTransactionCommand : IRequest<Boolean> 
    {
        public IFormFile File { get; set; }
    }
}
