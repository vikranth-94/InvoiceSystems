using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IRepository<Invoices> _invoiceRepository;
    private readonly IRepository<Customers> _customerRepository;
    private readonly IRepository<Products> _productRepository;
    public InvoicesController(
        IRepository<Invoices> invoiceRepository,
        IRepository<Customers> customerRepository,
        IRepository<Products> productRepository)
    {
        _invoiceRepository = invoiceRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Invoices>> CreateInvoice(Invoices invoice)
    {
        var customer = await _customerRepository.GetByIdAsync(invoice.CustomerId);
        if (customer == null) return BadRequest("Invalid customer");
        decimal total = 0;
        foreach (var item in invoice.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null || product.Quantity < item.Quantity)
                return BadRequest("Invalid product or insufficient quantity");
            item.Price = product.Price;
            total += item.Quantity * item.Price;
            product.Quantity -= item.Quantity;  
        }
        total -= invoice.Discount;
        total += invoice.Tax;
        invoice.TotalAmount = total;
        await _invoiceRepository.AddAsync(invoice);
        return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoices>> GetInvoice(int id)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(id);
        if (invoice == null) return NotFound();
        return invoice;
    }
}
