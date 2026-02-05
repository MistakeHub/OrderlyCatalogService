using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Orders.Domain.Entities
{

    public class RabbitMqOptions
    {
        public string Exchange { get; set; } = null!;
    }
}
