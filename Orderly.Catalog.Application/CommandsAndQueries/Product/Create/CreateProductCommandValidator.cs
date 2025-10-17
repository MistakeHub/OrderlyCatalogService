using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Orderly.Catalog.Application.CommandsAndQueries.Product.Create
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProduct>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.SKU)
                .NotEmpty();

        }
    }
}
