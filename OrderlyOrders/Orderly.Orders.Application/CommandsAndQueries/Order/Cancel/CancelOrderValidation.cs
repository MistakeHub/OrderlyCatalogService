using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Cancel
{
    public class CancelOrderValidation:AbstractValidator<CancelOrder>
    {
        public CancelOrderValidation() {

            RuleFor(x => x.OrderId).NotEmpty();
        
        }
    }
}
