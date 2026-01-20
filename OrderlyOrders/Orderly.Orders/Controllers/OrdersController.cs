using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orderly.Orders.Application.CommandsAndQueries.Order.Cancel;
using Orderly.Orders.Application.CommandsAndQueries.Order.Create;
using Orderly.Orders.Application.CommandsAndQueries.Order.GetAll;
using Orderly.Orders.Application.CommandsAndQueries.Order.GetById;
using Orderly.Orders.Application.CommandsAndQueries.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Orderly.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<List<OrderListViewModel>> GetAll()
        {
            return await _mediator.Send(new GetAllOrders());
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<OrderViewModel> GetById(int id)
        {
            return await _mediator.Send(new GetByIdOrder() { OrderId = id});
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<int> Create([FromBody] CreateOrder createOrder)
        {
            return await _mediator.Send(createOrder);
        }

        [HttpPost("{id}/cancel")]
        public async Task Cancel(int id)
        {
            await _mediator.Send(new CancelOrder() { OrderId = id });
        }

    
    }
}
