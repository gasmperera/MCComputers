using MCComputers_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace MCComputers_WebAPI.Data
{
    public class InvoiceDBContext : DbContext
    {
        public InvoiceDBContext(DbContextOptions options) : base(options)
        {

        }

        DbSet<Invoice> Invoices { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        DbSet<Product> Products { get; set; }
    }
}
