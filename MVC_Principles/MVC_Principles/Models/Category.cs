using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVC_Principles.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    [DisplayName("Category Name")]
    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
