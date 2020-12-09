using Clean.Application.Commands;
using Clean.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch_Transaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator mediator;

        public BlogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> DoCommand([FromBody] TransactionCommand command)
        {
            var updateddata =  await  mediator.Send(command);
            return Ok(updateddata);
        }
        [HttpGet]
        public async Task<IActionResult> GetQuery()
        {
            var data =  await mediator.Send(new BlogPostQuery());
            return Ok(data);
        }
    }
}
