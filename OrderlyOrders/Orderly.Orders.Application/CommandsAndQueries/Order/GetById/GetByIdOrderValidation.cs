using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.GetById
{
    public class GetByIdOrderValidation:AbstractValidator<GetByIdOrder>
    {
     
        public GetByIdOrderValidation() {


            RuleFor(x => x.OrderId).NotEmpty();
        
        }
    }
}
