using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCore7.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    IsOld = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContactDetailsAddressStreet = table.Column<string>(name: "ContactDetails_Address_Street", type: "TEXT", nullable: false),
                    ContactDetailsAddressCity = table.Column<string>(name: "ContactDetails_Address_City", type: "TEXT", nullable: false),
                    ContactDetailsAddressPostcode = table.Column<string>(name: "ContactDetails_Address_Postcode", type: "TEXT", nullable: false),
                    ContactDetailsAddressCountry = table.Column<string>(name: "ContactDetails_Address_Country", type: "TEXT", nullable: false),
                    ContactDetailsPhoneNumber = table.Column<string>(name: "ContactDetails_PhoneNumber", type: "TEXT", maxLength: 999999999, nullable: true),
                    Added = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Name",
                table: "Employees",
                column: "Name",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
