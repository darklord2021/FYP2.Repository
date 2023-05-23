using System;
using System.Collections.Generic;
using FYP.DB.DBTables;
using Microsoft.EntityFrameworkCore;

namespace FYP.DB.Context;

public partial class Fyp21Context : DbContext
{
    public Fyp21Context()
    {
    }

    public Fyp21Context(DbContextOptions<Fyp21Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountJournal> AccountJournals { get; set; }

    public virtual DbSet<AccountMove> AccountMoves { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SaleOrder> SaleOrders { get; set; }

    public virtual DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<TransferDetail> TransferDetails { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=FYPData");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountJournal>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AccountNavigation).WithMany(p => p.AccountJournals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Journal_Account_Move");
        });

        modelBuilder.Entity<AccountMove>(entity =>
        {
            entity.HasOne(d => d.PurchaseSourceDocNavigation).WithMany(p => p.AccountMoves)
                .HasPrincipalKey(p => p.DocName)
                .HasForeignKey(d => d.PurchaseSourceDoc)
                .HasConstraintName("FK_Account_Move_Purchase_Order");

            entity.HasOne(d => d.SourceDocNavigation).WithMany(p => p.AccountMoves)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.SourceDoc)
                .HasConstraintName("FK_Account_Move_Sale_Order");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__category__D54EE9B4C97CD641");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85B0A0220F");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Account).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_lines_Account_Move");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payment__3213E83F24B9F395");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product__47027DF5AED1C8E3");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_category");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__purchase__87071CB979BDBC6D");

            entity.Property(e => e.Cost).HasDefaultValueSql("((0.99))");
            entity.Property(e => e.State).HasDefaultValueSql("('RFQ')");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.PurchaseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_purchase_order_payment");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_purchase_order_vendor_master");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_purchase_order_details");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails).HasConstraintName("FK_purchase_order_details_product");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseOrderDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_purchase_order_details_purchase_order");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reviews__3213E83FCAF32842");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews).HasConstraintName("FK_reviews_customers");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews).HasConstraintName("FK_reviews_product");
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__sale_ord__E1EB00B23F3E713F");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.State).HasDefaultValueSql("('Quotation')");
            entity.Property(e => e.TotalAmount).HasDefaultValueSql("((0.00))");

            entity.HasOne(d => d.Customer).WithMany(p => p.SaleOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_customers");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.SaleOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_payment");
        });

        modelBuilder.Entity<SaleOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_sale_order_details");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Price).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleOrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_order_details_product");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleOrderDetails).HasConstraintName("FK_sale_order_details_sale_order");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transfer__3214EC2768AF2B94");

            entity.HasOne(d => d.BackorderDoc).WithMany(p => p.InverseBackorderDoc).HasConstraintName("FK_transfers_transfers");
        });

        modelBuilder.Entity<TransferDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transfer__3213E83FFF168FCA");

            entity.HasOne(d => d.Product).WithMany(p => p.TransferDetails).HasConstraintName("FK_transfer_details_product");

            entity.HasOne(d => d.Transfer).WithMany(p => p.TransferDetails).HasConstraintName("FK_transfer_details_transfers");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__vendor_m__0F7D2B7841498D66");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
