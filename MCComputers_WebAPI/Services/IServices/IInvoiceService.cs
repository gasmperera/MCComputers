using MCComputers_WebAPI.Models.DTOs;

namespace MCComputers_WebAPI.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> CreateInvoiceAsync(InvoiceCreateRequestDTO invoiceCreateRequest);
        Task<IEnumerable<InvoiceDTO>> GetAllInvoicesAsync();
        Task<InvoiceDTO> GetInvoiceByIdAsync(int id);
    }
}
