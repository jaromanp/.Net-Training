using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Principles.Models;

public partial class Product
{
    public int ProductId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Product name cannot be more than 50 characters.")]
    [DisplayName("Product Name")]
    public string ProductName { get; set; } = null!;

    public int? SupplierId { get; set; }

    public int? CategoryId { get; set; }

    [DisplayName("Quantity Per Unit")]
    public string? QuantityPerUnit { get; set; }

    [Range(1, 1000, ErrorMessage = "Product price must be between 1 and 1000.")]
    [DisplayName("Unit Price")]
    public decimal? UnitPrice { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Product must be in stock (quantity greater than 0).")]
    [DisplayName("Units in Stock")]
    public short? UnitsInStock { get; set; }

    [DisplayName("Units on Order")]
    public short? UnitsOnOrder { get; set; }

    [DisplayName("Reorder Level")]
    public short? ReorderLevel { get; set; }
    
    public bool Discontinued { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
