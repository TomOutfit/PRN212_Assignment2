using BusinessObjects;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [StringLength(40)]
    public string ProductName { get; set; } = String.Empty;

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public short UnitsInStock { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    public virtual Category? Category { get; set; } // ✅ Không required, nullable
}
