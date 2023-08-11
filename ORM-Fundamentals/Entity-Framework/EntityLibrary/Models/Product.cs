using System;
using System.Collections.Generic;

namespace EntityLibrary.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public decimal Weight { get; set; }

    public decimal Height { get; set; }

    public decimal Width { get; set; }

    public decimal Length { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
