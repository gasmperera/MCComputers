using MCComputers_WebAPI.Models;
using MCComputers_WebAPI.Models.DTOs;
using MCComputers_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MCComputers_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetAllInvoices()
        {
            try
            {
                var invoices = await _invoiceService.GetAllInvoicesAsync();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

       

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoice(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
                if (invoice == null)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Message = $"Invoice with ID {id} not found.",
                        Details = "The specified invoice does not exist in the system."
                    };
                    return NotFound(errorResponse);
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddInvoice([FromBody] InvoiceCreateRequestDTO invoiceCreateRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var createdInvoice = await _invoiceService.CreateInvoiceAsync(invoiceCreateRequestDTO);
                return CreatedAtAction(nameof(GetInvoice), new { id = createdInvoice.Id }, createdInvoice);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        protected ActionResult HandleException(Exception ex)
        {
            var errorResponse = new ErrorResponse
            {
                Message = "An unexpected error occurred.",
                Details = ex.Message
            };
            return StatusCode(500, errorResponse);
        }




    }
}
