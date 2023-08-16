using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP.DB.Migrations
{
    /// <inheritdoc />
    public partial class sale_sequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchase_order_details_product",
                table: "Purchase_Order_Details");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Purchase_Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "purchase_id",
                table: "Purchase_Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "product_id",
                table: "Purchase_Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Purchase_Order_Details",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "Purchase_Order",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "Product",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "paid",
                table: "Account_Move",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(0)))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Doc_Name",
                table: "Account_Move",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "Account_Journal",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Billing_Address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Billing_Address = table.Column<string>(type: "text", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billing_Address", x => x.id);
                    table.ForeignKey(
                        name: "FK_Billing Address_Customers",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "Shipping_Address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Shipping_Address = table.Column<string>(type: "text", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping_Address", x => x.id);
                    table.ForeignKey(
                        name: "FK_Shipping_Address_Customers",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "User_Auth",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Auth", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "AK_Sale_Order_name",
                table: "Sale_Order",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AK_Purchase_Order_doc_name",
                table: "Purchase_Order",
                column: "doc_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Billing_Address_customer_id",
                table: "Billing_Address",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_Address_customer_id",
                table: "Shipping_Address",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_order_details_product",
                table: "Purchase_Order_Details",
                column: "product_id",
                principalTable: "Product",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchase_order_details_product",
                table: "Purchase_Order_Details");

            migrationBuilder.DropTable(
                name: "Billing_Address");

            migrationBuilder.DropTable(
                name: "Shipping_Address");

            migrationBuilder.DropTable(
                name: "User_Auth");

            migrationBuilder.DropIndex(
                name: "AK_Sale_Order_name",
                table: "Sale_Order");

            migrationBuilder.DropIndex(
                name: "AK_Purchase_Order_doc_name",
                table: "Purchase_Order");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Purchase_Order_Details",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "purchase_id",
                table: "Purchase_Order_Details",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "product_id",
                table: "Purchase_Order_Details",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Purchase_Order_Details",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_date",
                table: "Purchase_Order",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "Product",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "paid",
                table: "Account_Move",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(0)))");

            migrationBuilder.AlterColumn<string>(
                name: "Doc_Name",
                table: "Account_Move",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "Account_Journal",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_order_details_product",
                table: "Purchase_Order_Details",
                column: "product_id",
                principalTable: "Product",
                principalColumn: "product_id");
        }
    }
}
