using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP.DB.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account_Journal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    account = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    debit = table.Column<decimal>(type: "money", nullable: true),
                    credit = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Journal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "date", nullable: false),
                    last_modified = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__category__D54EE9B4C97CD641", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    phone = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    record = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__CD65CB85B0A0220F", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    method_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payment__3213E83F24B9F395", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doc_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Source_Document = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    created_date = table.Column<DateTime>(type: "date", nullable: false),
                    backorder_doc_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    operation_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transfer__3214EC2768AF2B94", x => x.ID);
                    table.ForeignKey(
                        name: "FK_transfers_transfers",
                        column: x => x.backorder_doc_id,
                        principalTable: "Transfers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    vendor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NTN = table.Column<long>(type: "bigint", maxLength: 13, nullable: true),
                    phone_number = table.Column<long>(type: "bigint", nullable: false),
                    email_address = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    vendor_address = table.Column<string>(type: "text", nullable: false),
                    website = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vendor_m__0F7D2B7841498D66", x => x.vendor_id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    unit_price = table.Column<decimal>(type: "money", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    reorder_level = table.Column<int>(type: "int", nullable: true),
                    discontinued = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product__47027DF5AED1C8E3", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_product_category",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "Sale_Order",
                columns: table => new
                {
                    sale_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    total_amount = table.Column<decimal>(type: "money", nullable: false, defaultValueSql: "((0.00))"),
                    payment_method = table.Column<int>(type: "int", nullable: false),
                    date_created = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValueSql: "('Quotation')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sale_ord__E1EB00B23F3E713F", x => x.sale_id);
                    table.UniqueConstraint("AK_Sale_Order_name", x => x.name);
                    table.ForeignKey(
                        name: "FK_sale_order_customers",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK_sale_order_payment",
                        column: x => x.payment_method,
                        principalTable: "Payment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Order",
                columns: table => new
                {
                    purchase_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vendor_id = table.Column<int>(type: "int", nullable: false),
                    doc_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false, defaultValueSql: "((0.99))"),
                    create_date = table.Column<DateTime>(type: "date", nullable: true),
                    state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('RFQ')"),
                    payment_method = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__purchase__87071CB979BDBC6D", x => x.purchase_id);
                    table.UniqueConstraint("AK_Purchase_Order_doc_name", x => x.doc_name);
                    table.ForeignKey(
                        name: "FK_purchase_order_payment",
                        column: x => x.payment_method,
                        principalTable: "Payment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_purchase_order_vendor_master",
                        column: x => x.vendor_id,
                        principalTable: "Vendors",
                        principalColumn: "vendor_id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stars = table.Column<byte>(type: "tinyint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reviews__3213E83FCAF32842", x => x.id);
                    table.ForeignKey(
                        name: "FK_reviews_customers",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK_reviews_product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "Transfer_Details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transfer_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    demand = table.Column<int>(type: "int", nullable: false),
                    done = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transfer__3213E83FFF168FCA", x => x.id);
                    table.ForeignKey(
                        name: "FK_transfer_details_product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transfer_details_transfers",
                        column: x => x.transfer_id,
                        principalTable: "Transfers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale_Order_Details",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sale_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    price = table.Column<decimal>(type: "money", nullable: false, defaultValueSql: "((0.00))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_order_details", x => x.ID);
                    table.ForeignKey(
                        name: "FK_sale_order_details_product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_sale_order_details_sale_order",
                        column: x => x.sale_id,
                        principalTable: "Sale_Order",
                        principalColumn: "sale_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account_Move",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doc_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Total_Amount = table.Column<decimal>(type: "money", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "date", nullable: true),
                    Taxed_Amount = table.Column<double>(type: "float", nullable: true),
                    Source_Doc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    operation_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    tax = table.Column<double>(type: "float", nullable: true),
                    purchase_source_doc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Move", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Account_Move_Purchase_Order",
                        column: x => x.purchase_source_doc,
                        principalTable: "Purchase_Order",
                        principalColumn: "doc_name");
                    table.ForeignKey(
                        name: "FK_Account_Move_Sale_Order",
                        column: x => x.Source_Doc,
                        principalTable: "Sale_Order",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Order_Details",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchase_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order_details", x => x.ID);
                    table.ForeignKey(
                        name: "FK_purchase_order_details_product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_purchase_order_details_purchase_order",
                        column: x => x.purchase_id,
                        principalTable: "Purchase_Order",
                        principalColumn: "purchase_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice_lines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    taxes = table.Column<double>(type: "float", nullable: false),
                    account_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_lines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invoice_lines_Account_Move",
                        column: x => x.account_id,
                        principalTable: "Account_Move",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Move_purchase_source_doc",
                table: "Account_Move",
                column: "purchase_source_doc");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Move_Source_Doc",
                table: "Account_Move",
                column: "Source_Doc");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_lines_account_id",
                table: "Invoice_lines",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_category_id",
                table: "Product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order",
                table: "Purchase_Order",
                column: "doc_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_payment_method",
                table: "Purchase_Order",
                column: "payment_method");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_vendor_id",
                table: "Purchase_Order",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Details_product_id",
                table: "Purchase_Order_Details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Details_purchase_id",
                table: "Purchase_Order_Details",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_customer_id",
                table: "Reviews",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_product_id",
                table: "Reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Order_1",
                table: "Sale_Order",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Order_customer_id",
                table: "Sale_Order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Order_payment_method",
                table: "Sale_Order",
                column: "payment_method");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Order_Details_product_id",
                table: "Sale_Order_Details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Order_Details_sale_id",
                table: "Sale_Order_Details",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Details_product_id",
                table: "Transfer_Details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_Details_transfer_id",
                table: "Transfer_Details",
                column: "transfer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_backorder_doc_id",
                table: "Transfers",
                column: "backorder_doc_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account_Journal");

            migrationBuilder.DropTable(
                name: "Invoice_lines");

            migrationBuilder.DropTable(
                name: "Purchase_Order_Details");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Sale_Order_Details");

            migrationBuilder.DropTable(
                name: "Transfer_Details");

            migrationBuilder.DropTable(
                name: "Account_Move");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Purchase_Order");

            migrationBuilder.DropTable(
                name: "Sale_Order");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
