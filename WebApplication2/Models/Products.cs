using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

public class Products
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Range(0.01, 10000.00)]
    public decimal Price { get; set; }

    [Range(0, 1000)]
    public int Quantity { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
