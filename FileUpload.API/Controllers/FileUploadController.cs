using FileUpload.Application.Commands;
using FileUpload.Application.Queries;
using FileUpload.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FileUploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<TransactionResponse>> Get(string currency, DateTime dateFrom, DateTime dateTo, string status)
        {
            return await _mediator.Send(new GetAllTransactionQuery() 
            {
                Currency = currency,
                Datefrom = dateFrom,
                DateTo = dateTo,
                Status = status
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionResponse>> UploadFile([FromForm] UploadTransactionCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return ValidationProblem();
            return Ok(result);
        }
    }
}
