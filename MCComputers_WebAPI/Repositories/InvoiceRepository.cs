using MCComputers_WebAPI.Data;
using MCComputers_WebAPI.Models;
using MCComputers_WebAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MCComputers_WebAPI.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDBContext _context;

        public InvoiceRepository(InvoiceDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Set<Invoice>().Include(i => i.InvoiceItems)
                                          .ThenInclude(ii => ii.Product)
                                          .ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Set<Invoice>().Include(i => i.InvoiceItems)
                                          .ThenInclude(ii => ii.Product)
                                          .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            _context.Set<Invoice>().Add(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
