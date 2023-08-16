using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables;

[Table("Product")]
[Index("category_id", Name = "IX_Product_category_id")]
public partial class Product
{
    [Key]
    public int product_id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(50)]
    [Display(Name = "Product Name")]
    [Unicode(false)]
    public string name { get; set; } = null!;

    [Column(TypeName = "text")]
    [Display(Name = "Description")]
    public string? description { get; set; }

    [Required(ErrorMessage = "The Quantity field is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
    [Display(Name = "Quantity")]
    public int? quantity { get; set; }

    [Required(ErrorMessage = "The Unit Price field is required.")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    [Display(Name ="Unit Price")]
    public decimal? unit_price { get; set; }
    [Display(Name = "Category")]
    [Required(ErrorMessage = "The Category ID field is required.")]
    public int category_id { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Reorder Level must be a non-negative integer.")]
    [Display(Name = "Reorder Level")]

    public int? reorder_level { get; set; }
    [Display(Name ="Discontinued")]
    public bool discontinued { get; set; }

    [InverseProperty("product")]
    public virtual ICollection<Purchase_Order_Detail> Purchase_Order_Details { get; set; } = new List<Purchase_Order_Detail>();

    [InverseProperty("product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("product")]
    public virtual ICollection<Sale_Order_Detail> Sale_Order_Details { get; set; } = new List<Sale_Order_Detail>();

    [InverseProperty("product")]
    public virtual ICollection<Transfer_Detail> Transfer_Details { get; set; } = new List<Transfer_Detail>();

    [ForeignKey("category_id")]
    [InverseProperty("Products")]
    [Display(Name = "Category")]
    public virtual Category category { get; set; } = null!;
}
