//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace FYP.DB.DBTables;

//[Table("Sale_Order")]
//[Index("name", Name = "AK_Sale_Order_name", IsUnique = true)]
//[Index("name", Name = "IX_Sale_Order_1", IsUnique = true)]
//[Index("customer_id", Name = "IX_Sale_Order_customer_id")]
//[Index("payment_method", Name = "IX_Sale_Order_payment_method")]
//public partial class Sale_Order
//{
//    [Key]
//    public int sale_id { get; set; }

//    [Required(ErrorMessage = "The Customer ID field is required.")]
//    public int customer_id { get; set; }

//    [Required(ErrorMessage = "The Name field is required.")]
//    [StringLength(50)]
//    [Display(Name = "Name")]
//    [Unicode(false)]
//    public string name { get; set; } = null!;

//    [Required(ErrorMessage = "The Total Amount field is required.")]
//    [Column(TypeName = "money")]
//    [DataType(DataType.Currency)]
//    public decimal total_amount { get; set; }

//    [Required(ErrorMessage = "The Payment Method field is required.")]
//    public int payment_method { get; set; }

//    [Column(TypeName = "date")]
//    [DataType(DataType.Date)]
//    public DateTime? date_created { get; set; }

//    [StringLength(50)]
//    [Display(Name = "State")]
//    [Unicode(false)]
//    public string? state { get; set; }

//    public virtual ICollection<Account_Move> Account_Moves { get; set; } = new List<Account_Move>();

//    [InverseProperty("sale")]
//    public virtual ICollection<Sale_Order_Detail> Sale_Order_Details { get; set; } = new List<Sale_Order_Detail>();

//    [ForeignKey("customer_id")]
//    [InverseProperty("Sale_Orders")]
//    public virtual Customer customer { get; set; } = null!;

//    [ForeignKey("payment_method")]
//    [InverseProperty("Sale_Orders")]
//    public virtual Payment payment_methodNavigation { get; set; } = null!;
//}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.DBTables
{
    [Table("Sale_Order")]
    [Index("name", Name = "AK_Sale_Order_name", IsUnique = true)]
    [Index("name", Name = "IX_Sale_Order_1", IsUnique = true)]
    [Index("customer_id", Name = "IX_Sale_Order_customer_id")]
    [Index("payment_method", Name = "IX_Sale_Order_payment_method")]
    public partial class Sale_Order
    {
        [Key]
        public int sale_id { get; set; }

        [Required(ErrorMessage = "The Customer ID field is required.")]
        [Display(Name ="Customer")]
        public int customer_id { get; set; }

        //[Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50)]
        [Display(Name = "Name")]
        [Unicode(false)]
        public string name { get; set; } = null!;

        [Required(ErrorMessage = "The Total Amount field is required.")]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        [Display(Name = "Total Amount")]
        public decimal total_amount { get; set; }

        [Required(ErrorMessage = "The Payment Method field is required.")]
        [Display(Name ="Payment Method")]
        public int payment_method { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime? date_created { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        [Unicode(false)]
        public string? state { get; set; }

        public virtual ICollection<Account_Move> Account_Moves { get; set; } = new List<Account_Move>();

        [InverseProperty("sale")]
        public virtual ICollection<Sale_Order_Detail> Sale_Order_Details { get; set; } = new List<Sale_Order_Detail>();

        [ForeignKey("customer_id")]
        [InverseProperty("Sale_Orders")]
        [Display(Name = "Customer")]
        public virtual Customer customer { get; set; } = null!;

        [ForeignKey("payment_method")]
        [InverseProperty("Sale_Orders")]
        [Display(Name = "Payment Method")]
        public virtual Payment payment_methodNavigation { get; set; } = null!;

        [NotMapped]
        public string FormattedOrderNumber => $"S{sale_id:D5}";
    }
}

