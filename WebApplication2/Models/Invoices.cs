namespace WebApplication2.Models
{
    public class Invoices
    {
        public int Id { get; set; }
        public Guid CartId { get; set; } = Guid.NewGuid();
        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
        public List<InvoiceItems> Items { get; set; } = new List<InvoiceItems>();
        public decimal TotalAmount { get; set; }
        public string PaymentOption { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
    }
}
