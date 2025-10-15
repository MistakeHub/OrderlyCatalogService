using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Catalog.Domain.Entities;

namespace Orderly.Catalog.Domain.Entities
{
    public class Vendor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? WebSite { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
