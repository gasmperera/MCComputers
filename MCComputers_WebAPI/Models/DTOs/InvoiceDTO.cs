using MCComputers_WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MCComputers_WebAPI.Models.DTOs
{
    public class InvoiceDTO
    {
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0.")]
        public decimal TotalAmount { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
