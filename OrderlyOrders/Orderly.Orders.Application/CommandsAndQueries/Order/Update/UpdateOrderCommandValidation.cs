using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Orderly.Orders.Domain.Entities;

namespace Orderly.Orders.Application.CommandsAndQueries.Order.Update
{
    public class UpdateOrderCommandValidation : AbstractValidator<UpdateOrder>
    {
        public UpdateOrderCommandValidation()
        {

            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.TotalPrice).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.Items).NotNull();
            RuleFor(x => x.CreatedAt).GreaterThan(new DateTime(2025, 01, 01));

        }
    }
}
