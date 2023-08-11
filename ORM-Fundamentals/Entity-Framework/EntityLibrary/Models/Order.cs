using System;
using System.Collections.Generic;

namespace EntityLibrary.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
