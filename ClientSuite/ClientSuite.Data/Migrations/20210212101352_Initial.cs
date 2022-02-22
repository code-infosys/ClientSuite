using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientSuite.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Settings");

            migrationBuilder.EnsureSchema(
                name: "Master");

            migrationBuilder.EnsureSchema(
                name: "Brand");

            migrationBuilder.EnsureSchema(
                name: "Client");

            migrationBuilder.EnsureSchema(
                name: "Email");

            migrationBuilder.EnsureSchema(
                name: "Quote");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.EnsureSchema(
                name: "Support");

            migrationBuilder.EnsureSchema(
                name: "Payment");

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Picture = table.Column<string>(maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailMessage",
                schema: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(maxLength: 1000, nullable: true),
                    Body = table.Column<string>(nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AttachmentJson = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTo",
                schema: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailMessageId = table.Column<int>(nullable: false),
                    ToUserId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuText = table.Column<string>(maxLength: 100, nullable: false),
                    MenuURL = table.Column<string>(maxLength: 400, nullable: false),
                    SortOrder = table.Column<int>(nullable: true),
                    MenuIcon = table.Column<string>(maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Identity",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(maxLength: 100, nullable: false),
                    TableId = table.Column<int>(nullable: true),
                    Details = table.Column<string>(maxLength: 300, nullable: false),
                    ProcessToUrl = table.Column<string>(maxLength: 400, nullable: true),
                    IsRead = table.Column<bool>(nullable: true),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToUserId = table.Column<int>(nullable: true),
                    ToRoleId = table.Column<int>(nullable: true),
                    UniqueId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseStatus",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSetting",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(maxLength: 100, nullable: false),
                    AppShortName = table.Column<string>(maxLength: 50, nullable: true),
                    AppVersion = table.Column<string>(maxLength: 15, nullable: true),
                    IsToggleSidebar = table.Column<bool>(nullable: false),
                    IsBoxedLayout = table.Column<bool>(nullable: false),
                    IsFixedLayout = table.Column<bool>(nullable: false),
                    IsToggleRightSidebar = table.Column<bool>(nullable: false),
                    Skin = table.Column<string>(maxLength: 20, nullable: true),
                    FooterText = table.Column<string>(maxLength: 150, nullable: true),
                    Logo = table.Column<string>(maxLength: 100, nullable: true),
                    LoginPageBackground = table.Column<string>(maxLength: 100, nullable: true),
                    TimeZone = table.Column<string>(maxLength: 200, nullable: true),
                    Currency = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralSetting",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingKey = table.Column<string>(maxLength: 50, nullable: false),
                    SettingValue = table.Column<string>(maxLength: 2000, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    SettingGroup = table.Column<string>(maxLength: 50, nullable: true),
                    FieldType = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDocumentation",
                schema: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(nullable: false),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDocumentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDocumentation_ProductDocumentation_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Brand",
                        principalTable: "ProductDocumentation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductDocumentation_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Brand",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source = table.Column<string>(maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    TransactionNumber = table.Column<string>(maxLength: 300, nullable: false),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Brand",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(maxLength: 20, nullable: true),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Master",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    FullName = table.Column<string>(maxLength: 300, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ChangePasswordCode = table.Column<string>(maxLength: 100, nullable: true),
                    Otp = table.Column<string>(maxLength: 20, nullable: true),
                    IsConfirm = table.Column<bool>(nullable: false),
                    ProfilePicture = table.Column<string>(maxLength: 100, nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 200, nullable: false),
                    CompanyPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyMobile = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Master",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Master",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "Support",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketSubject = table.Column<string>(maxLength: 500, nullable: false),
                    TicketBody = table.Column<string>(nullable: false),
                    AddedBy = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    Attachment = table.Column<string>(maxLength: 100, nullable: true),
                    IsClose = table.Column<bool>(nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsKnowledgeBase = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    PriorityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Ticket_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Support",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalSchema: "Master",
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Brand",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientCategory",
                schema: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Master",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientCategory_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Client",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientProduct",
                schema: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    FollowUpDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    PurchaseStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Brand",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientProduct_PurchaseStatus_PurchaseStatusId",
                        column: x => x.PurchaseStatusId,
                        principalSchema: "Master",
                        principalTable: "PurchaseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientProduct_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Client",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuPermission",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortOrder = table.Column<int>(nullable: true),
                    IsCreate = table.Column<bool>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    IsUpdate = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    MenuId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuPermission_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "Identity",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPermission_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Client",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Client",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "Quote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    BusinessLogo = table.Column<string>(maxLength: 100, nullable: true),
                    BusinessDetails = table.Column<string>(maxLength: 1000, nullable: false),
                    BillTo = table.Column<string>(maxLength: 200, nullable: true),
                    ClientDetails = table.Column<string>(maxLength: 1000, nullable: false),
                    QuoteNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Dated = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ItemJson = table.Column<string>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    TotalDue = table.Column<decimal>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: true),
                    BalanceDue = table.Column<decimal>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    AddedBy = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsQuote = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 1000, nullable: true),
                    TermsAndCondition = table.Column<string>(maxLength: 1000, nullable: true),
                    Sign = table.Column<string>(maxLength: 200, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Client",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCategory_CategoryId",
                schema: "Brand",
                table: "ClientCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCategory_UserId",
                schema: "Brand",
                table: "ClientCategory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocumentation_ParentId",
                schema: "Brand",
                table: "ProductDocumentation",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocumentation_ProductId",
                schema: "Brand",
                table: "ProductDocumentation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProduct_ProductId",
                schema: "Client",
                table: "ClientProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProduct_PurchaseStatusId",
                schema: "Client",
                table: "ClientProduct",
                column: "PurchaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProduct_UserId",
                schema: "Client",
                table: "ClientProduct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                schema: "Client",
                table: "Contacts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CountryId",
                schema: "Client",
                table: "User",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CurrencyId",
                schema: "Client",
                table: "User",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "Client",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                schema: "Identity",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_MenuId",
                schema: "Identity",
                table: "MenuPermission",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_RoleId",
                schema: "Identity",
                table: "MenuPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_UserId",
                schema: "Identity",
                table: "MenuPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_RoleId",
                schema: "Identity",
                table: "RoleUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserId",
                schema: "Identity",
                table: "RoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ProductId",
                schema: "Payment",
                table: "Transaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId",
                schema: "Quote",
                table: "Invoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ParentId",
                schema: "Support",
                table: "Ticket",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PriorityId",
                schema: "Support",
                table: "Ticket",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ProductId",
                schema: "Support",
                table: "Ticket",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCategory",
                schema: "Brand");

            migrationBuilder.DropTable(
                name: "ProductDocumentation",
                schema: "Brand");

            migrationBuilder.DropTable(
                name: "ClientProduct",
                schema: "Client");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "Client");

            migrationBuilder.DropTable(
                name: "EmailMessage",
                schema: "Email");

            migrationBuilder.DropTable(
                name: "EmailTo",
                schema: "Email");

            migrationBuilder.DropTable(
                name: "MenuPermission",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RoleUser",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "Payment");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "Quote");

            migrationBuilder.DropTable(
                name: "AppSetting",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "GeneralSetting",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "Support");

            migrationBuilder.DropTable(
                name: "PurchaseStatus",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Client");

            migrationBuilder.DropTable(
                name: "Priority",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Brand");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");
        }
    }
}
