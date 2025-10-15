using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Create;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Delete;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetAll;
using Orderly.Catalog.Application.CommandsAndQueries.Product.GetById;
using Orderly.Catalog.Application.CommandsAndQueries.Product.Update;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.Create;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.Delete;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetAll;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.GetById;
using Orderly.Catalog.Application.CommandsAndQueries.Vendor.Update;

namespace Orderly.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VendorsController(IMediator mediator) { _mediator = mediator; }
        
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Domain.Entities.Vendor>> TestDb()
        {
            return await _mediator.Send(new GetAllVendor());
        }

        [HttpGet("GetByid/{id}")]
        public async Task<Domain.Entities.Vendor> GetById([FromRoute] int id)
        {
            return await _mediator.Send(new GetByIdVendor { Id = id });
        }
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            await _mediator.Send(new DeleteVendor { Id = id });
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVendor create)
        {
            var id = await _mediator.Send(create);
            return Ok(new { Id = id });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVendor update)
        {
            await _mediator.Send(update);
            return Ok();
        }
    }
}
