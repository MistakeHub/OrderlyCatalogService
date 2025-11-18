using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Create;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Delete;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetAll;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetById;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Update;
using Orderly.Catalog.Application.CommandsAndQueries.Product.ViewModels;
using Orderly.Catalog.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Orderly.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) { _mediator = mediator; }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> TestDb()
        {
            return await _mediator.Send(new GetAllProduct());
        }

        [HttpGet("{id}")]
        public async Task<Domain.Entities.Product> GetById ([FromRoute] int id)
        {
            return await _mediator.Send(new GetByIdProduct { Id = id });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            await _mediator.Send(new DeleteProduct { Id = id });
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProduct create)
        {
            var id = await _mediator.Send(create);
            return Ok(new { Id = id });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProduct update)
        {
            await _mediator.Send(update);
            return Ok();
        }
        
    }
}
