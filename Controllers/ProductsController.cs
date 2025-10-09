using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orderly.Catalog.Database;

namespace Orderly.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        CatalogDbContext _db;
        public ProductsController(CatalogDbContext db) { _db = db; }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDb()
        {
            var count = await _db.Products.CountAsync();
            return Ok($"Products count: {count}");
        }
    }
}
