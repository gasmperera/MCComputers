using AutoMapper;
using MCComputers_WebAPI.Models;
using MCComputers_WebAPI.Models.DTOs;
using MCComputers_WebAPI.Repositories.IRepositories;
using MCComputers_WebAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MCComputers_WebAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<InvoiceDTO> CreateInvoiceAsync(InvoiceCreateRequestDTO invoiceCreateRequest)
        {
            if (invoiceCreateRequest == null || !invoiceCreateRequest.InvoiceItemDTO.Any())
            {
                throw new ArgumentException("Invalid invoice request.");
            }
            var invoice = new Invoice
            {
                TransactionDate = DateTime.Now,
                TotalAmount = 0,
                InvoiceItems = new List<InvoiceItem>()
            };

            foreach (var item in invoiceCreateRequest.InvoiceItemDTO)
            {
                if (item.Quantity <= 0 || item.Price < 0)
                {
                    throw new ArgumentException($"Invalid quantity or price for product {item.ProductId}.");
                }

                decimal discountedPrice = item.Price * (1 - item.Discount / 100);

                decimal totalPrice = discountedPrice * item.Quantity;

                // Create invoice item and add to invoice
                var invoiceItem = new InvoiceItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price,
                    Discount = item.Discount,
                    Price = totalPrice
                };

                invoice.InvoiceItems.Add(invoiceItem);
                invoice.TotalAmount += totalPrice;
            }

            await _invoiceRepository.AddAsync(invoice);

            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task<IEnumerable<InvoiceDTO>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            if (invoices == null || !invoices.Any())
            {
                throw new Exception("No invoices found.");
            }

            return _mapper.Map<List<InvoiceDTO>>(invoices);
        }

        public async Task<InvoiceDTO> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException($"Invoice with ID {id} not found.");
            }

            return _mapper.Map<InvoiceDTO>(invoice);
        }
    }
}
