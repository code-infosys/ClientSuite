DECLARE @IdentityId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Identity', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @IdentityId = SCOPE_IDENTITY();
DECLARE @ClientId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Client', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @ClientId = SCOPE_IDENTITY();
DECLARE @SettingsId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Settings', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @SettingsId = SCOPE_IDENTITY();
DECLARE @MasterId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Master', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @MasterId = SCOPE_IDENTITY();
DECLARE @BrandId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Brand', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @BrandId = SCOPE_IDENTITY();
DECLARE @QuoteId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Quote', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @QuoteId = SCOPE_IDENTITY();
DECLARE @SupportId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Support', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @SupportId = SCOPE_IDENTITY();
DECLARE @EmailId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Email', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @EmailId = SCOPE_IDENTITY();
DECLARE @PaymentId INTEGER
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Payment', 'root', NULL, NULL,'<i class="fa fa-circle"></i>') SELECT @PaymentId = SCOPE_IDENTITY();

INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Role', 'Identity/Role', @IdentityId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('User', 'Client/User', @ClientId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Role User', 'Identity/RoleUser', @IdentityId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Menu', 'Identity/Menu', @IdentityId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Menu Permission', 'Identity/MenuPermission', @IdentityId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('App Setting', 'Settings/AppSetting', @SettingsId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('General Setting', 'Settings/GeneralSetting', @SettingsId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Currency', 'Master/Currency', @MasterId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Country', 'Master/Country', @MasterId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Client Product', 'Client/ClientProduct', @ClientId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Product', 'Brand/Product', @BrandId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Purchase Status', 'Master/PurchaseStatus', @MasterId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Client Category', 'Brand/ClientCategory', @BrandId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Category', 'Master/Category', @MasterId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Invoice', 'Quote/Invoice', @QuoteId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Contacts', 'Client/Contacts', @ClientId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Ticket', 'Support/Ticket', @SupportId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Priority', 'Master/Priority', @MasterId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Email Message', 'Email/EmailMessage', @EmailId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Email To', 'Email/EmailTo', @EmailId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Product Documentation', 'Brand/ProductDocumentation', @BrandId, NULL,'<i class="fa fa-arrow-circle-right"></i>');
INSERT INTO Menu (MenuText, MenuURL, ParentId, SortOrder,MenuIcon) VALUES('Transaction', 'Payment/Transaction', @PaymentId, NULL,'<i class="fa fa-arrow-circle-right"></i>');

SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([Id], [RoleName], [IsActive]) VALUES (1, N'Admin', 1)
INSERT [dbo].[Role] ([Id], [RoleName], [IsActive]) VALUES (2, N'User', 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON
INSERT [dbo].[Menu] ([Id], [MenuText], [MenuURL], [ParentId], [SortOrder], [MenuIcon]) VALUES (7, N'Menu Assigner', N'MenuAssigner', 1, NULL, N'<i class="fa fa-gg"></i>')
SET IDENTITY_INSERT [dbo].[Menu] OFF
INSERT [dbo].[AppSetting] ( [AppName], [AppShortName], [AppVersion], [IsToggleSidebar], [IsBoxedLayout], [IsFixedLayout], [IsToggleRightSidebar], [Skin], [FooterText], [Logo], [LoginPageBackground], [TimeZone]) VALUES (  N'<b>Code</b> Infosys', N'<b>C</b>I', N'Version 1.0', 0, 0, 0, 0, N'skin-yellow', N'$year$ . All Rights reserved.', N'GetCode.png', N'solidbg.jpg', N'India Standard Time')
SET IDENTITY_INSERT [dbo].[GeneralSetting] ON
INSERT [dbo].[GeneralSetting] ([Id], [SettingKey], [SettingValue], [Description], [SettingGroup]) VALUES (1, N'SmptServer', N'smtp.gmail.com', N'smtp details', N'smtp')
INSERT [dbo].[GeneralSetting] ([Id], [SettingKey], [SettingValue], [Description], [SettingGroup]) VALUES (2, N'SmtpPort', N'587', N'smtp details', N'smtp')
INSERT [dbo].[GeneralSetting] ([Id], [SettingKey], [SettingValue], [Description], [SettingGroup]) VALUES (3, N'SmtpUsername', N'username', N'smtp details', N'smtp')
INSERT [dbo].[GeneralSetting] ([Id], [SettingKey], [SettingValue], [Description], [SettingGroup]) VALUES (4, N'SmtpPassword', N'password', N'smtp details', N'smtp')
INSERT [dbo].[GeneralSetting] ([Id], [SettingKey], [SettingValue], [Description], [SettingGroup]) VALUES (5, N'FbAppId', N'Your FbAppId', N'for facebook appid', NULL)
INSERT [dbo].[GeneralSetting] ([Id], [SettingKey], [SettingValue], [Description], [SettingGroup]) VALUES (6, N'GoogleKeyForLogin', N'yourgooglekey', N'google key for login', NULL)
SET IDENTITY_INSERT [dbo].[GeneralSetting] OFF
INSERT INTO MenuPermission (MenuId, RoleId,SortOrder,UserId,IsCreate,IsRead,IsUpdate,IsDelete,IsActive) SELECT Id as MenuId, 1 as RoleId, 1 as SortOrder,NULL AS UserId,1 as IsCreate,1 as IsRead,1 as IsUpdate,1 as IsDelete,1 as IsActive  FROM Menu

