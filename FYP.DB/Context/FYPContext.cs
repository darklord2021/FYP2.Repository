using System;
using System.Collections.Generic;
using FYP.DB.DBTables;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.Context;

public partial class FYPContext : DbContext
{
    public FYPContext()
    {
    }

    public FYPContext(DbContextOptions<FYPContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account_Journal> Account_Journals { get; set; }

    public virtual DbSet<Account_Move> Account_Moves { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice_line> Invoice_lines { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase_Order> Purchase_Orders { get; set; }

    public virtual DbSet<Purchase_Order_Detail> Purchase_Order_Details { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Sale_Order> Sale_Orders { get; set; }

    public virtual DbSet<Sale_Order_Detail> Sale_Order_Details { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<Transfer_Detail> Transfer_Details { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=fyp21;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account_Journal>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.accountNavigation).WithMany(p => p.Account_Journals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Journal_Account_Move");
        });

        modelBuilder.Entity<Account_Move>(entity =>
        {
            entity.HasOne(d => d.Source_DocNavigation).WithMany(p => p.Account_Moves)
                .HasPrincipalKey(p => p.name)
                .HasForeignKey(d => d.Source_Doc)
                .HasConstraintName("FK_Account_Move_Sale_Order");

            entity.HasOne(d => d.purchase_source_docNavigation).WithMany(p => p.Account_Moves)
                .HasPrincipalKey(p => p.doc_name)
                .HasForeignKey(d => d.purchase_source_doc)
                .HasConstraintName("FK_Account_Move_Purchase_Order");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.category_id).HasName("PK__category__D54EE9B4C97CD641");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.customer_id).HasName("PK__customer__CD65CB85B0A0220F");
        });

        modelBuilder.Entity<Invoice_line>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();

            entity.HasOne(d => d.account).WithMany(p => p.Invoice_lines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_lines_Account_Move");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__payment__3213E83F24B9F395");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.product_id).HasName("PK__product__47027DF5AED1C8E3");

            entity.HasOne(d => d.category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_category");
        });

        modelBuilder.Entity<Purchase_Order>(entity =>
        {
            entity.HasKey(e => e.purchase_id).HasName("PK__purchase__87071CB979BDBC6D");

            entity.Property(e => e.cost).HasDefaultValueSql("((0.99))");
            entity.Property(e => e.state).HasDefaultValueSql("('RFQ')");

            entity.HasOne(d => d.payment_methodNavigation).WithMany(p => p.Purchase_Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_purchase_order_payment");

            entity.HasOne(d => d.vendor).WithMany(p => p.Purchase_Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_purchase_order_vendor_master");
        });

        modelBuilder.Entity<Purchase_Order_Detail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_purchase_order_details");

            entity.HasOne(d => d.product).WithMany(p => p.Purchase_Order_Details).HasConstraintName("FK_purchase_order_details_product");

            entity.HasOne(d => d.purchase).WithMany(p => p.Purchase_Order_Details)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_purchase_order_details_purchase_order");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__reviews__3213E83FCAF32842");

            entity.HasOne(d => d.customer).WithMany(p => p.Reviews).HasConstraintName("FK_reviews_customers");

            entity.HasOne(d => d.product).WithMany(p => p.Reviews).HasConstraintName("FK_reviews_product");
        });

        modelBuilder.Entity<Sale_Order>(entity =>
        {
            entity.HasKey(e => e.sale_id).HasName("PK__sale_ord__E1EB00B23F3E713F");

            entity.Property(e => e.date_created).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.state).HasDefaultValueSql("('Quotation')");
            entity.Property(e => e.total_amount).HasDefaultValueSql("((0.00))");

            entity.HasOne(d => d.customer).WithMany(p => p.Sale_Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_customers");

            entity.HasOne(d => d.payment_methodNavigation).WithMany(p => p.Sale_Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_payment");
        });

        modelBuilder.Entity<Sale_Order_Detail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK_sale_order_details");

            entity.Property(e => e.price).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.quantity).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.product).WithMany(p => p.Sale_Order_Details)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_details_product");

            entity.HasOne(d => d.sale).WithMany(p => p.Sale_Order_Details).HasConstraintName("FK_sale_order_details_sale_order");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__transfer__3214EC2768AF2B94");

            entity.HasOne(d => d.backorder_doc).WithMany(p => p.Inversebackorder_doc).HasConstraintName("FK_transfers_transfers");
        });

        modelBuilder.Entity<Transfer_Detail>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__transfer__3213E83FFF168FCA");

            entity.HasOne(d => d.product).WithMany(p => p.Transfer_Details).HasConstraintName("FK_transfer_details_product");

            entity.HasOne(d => d.transfer).WithMany(p => p.Transfer_Details).HasConstraintName("FK_transfer_details_transfers");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.vendor_id).HasName("PK__vendor_m__0F7D2B7841498D66");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
