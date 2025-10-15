using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orderly.Catalog.Database;
using Orderly.Catalog.Domain.Entities;
using Orderly.Catalog.Domain.Interfaces;

namespace Orderly.Catalog.Infrastructure.Implemintation
{
    public class VendorRepository:IVendorRepository
    {
        private readonly CatalogDbContext _context;

        public VendorRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            return await _context.Vendors.AsNoTracking().ToListAsync();
        }

        public async Task<Vendor?> GetByIdAsync(int id)
        {
            return await _context.Vendors.FindAsync(id);
        }

        public async Task<int> AddAsync(Vendor vendor)
        {
            await _context.Vendors.AddAsync(vendor);
            await _context.SaveChangesAsync();
            var newentity = await _context.Vendors.AsNoTracking().FirstOrDefaultAsync(x => x.Name == vendor.Name);
            return newentity!.Id;
        }

        public async Task UpdateAsync(Vendor vendor)
        {
            var oldentity= await _context.Vendors.FirstOrDefaultAsync(x=> x.Id == vendor.Id);

            if (oldentity == null) return;

            oldentity.Name = vendor.Name;
            oldentity.WebSite = vendor.WebSite;

            _context.Vendors.Update(oldentity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor is null) return;
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
        }
    }
}
