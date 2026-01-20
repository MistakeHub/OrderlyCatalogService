using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Create
{
    public class CreateOrderValidation:AbstractValidator<CreateOrder>
    {
        public CreateOrderValidation() {

            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.Items).NotNull().NotEmpty();
 
        }
    }
}
