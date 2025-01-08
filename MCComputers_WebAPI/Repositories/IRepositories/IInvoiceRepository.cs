using MCComputers_WebAPI.Models;

namespace MCComputers_WebAPI.Repositories.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> GetByIdAsync(int id);
        Task AddAsync(Invoice invoice);
    }
}
