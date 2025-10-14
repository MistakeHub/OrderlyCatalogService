using Orderly.Catalog.Domain.Entities;

namespace Orderly.Catalog.Entities
{
    public class Product
    {

        public int Id {  get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string SKU { get; set; } = default!;

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

    }
}
