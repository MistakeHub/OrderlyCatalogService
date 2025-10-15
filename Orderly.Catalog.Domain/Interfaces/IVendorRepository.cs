using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderly.Catalog.Domain.Entities;


namespace Orderly.Catalog.Domain.Interfaces
{
    public interface IVendorRepository
    {

        Task<IEnumerable<Vendor>> GetAllAsync();
        Task<Vendor?> GetByIdAsync(int id);
        Task<int> AddAsync(Vendor vendor);
        Task UpdateAsync(Vendor vendor);
        Task DeleteAsync(int id);
    }
}
