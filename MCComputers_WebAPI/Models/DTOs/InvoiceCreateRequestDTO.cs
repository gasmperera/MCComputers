using System.ComponentModel.DataAnnotations;

namespace MCComputers_WebAPI.Models.DTOs
{
    public class InvoiceCreateRequestDTO
    {
        [Required(ErrorMessage = "Invoice items are required.")]
        [MinLength(1, ErrorMessage = "At least one invoice item is required.")]
        public List<InvoiceItemDTO> InvoiceItemDTO { get; set; }
    }
}
