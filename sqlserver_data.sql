BEGIN TRANSACTION;

--
-- Extension: Platformus.Core
-- Version: 2.6.0
--

SET IDENTITY_INSERT [dbo].[Users] ON;
INSERT INTO [dbo].[Users] ([Id], [Name], [Created]) VALUES (1, N'Administrator', N'2017-01-01 00:00:00.000');
SET IDENTITY_INSERT [dbo].[Users] OFF;

SET IDENTITY_INSERT [dbo].[CredentialTypes] ON;
INSERT INTO [dbo].[CredentialTypes] ([Id], [Code], [Name], [Position]) VALUES (1, N'Email', N'Email', 1);
SET IDENTITY_INSERT [dbo].[CredentialTypes] OFF;

SET IDENTITY_INSERT [dbo].[Credentials] ON;
INSERT INTO [dbo].[Credentials] ([Id], [UserId], [CredentialTypeId], [Identifier], [Secret], [Extra]) VALUES (1, 1, 1, N'admin@platformus.net', N'8lE3xN2Ijiv/Y/aIGwaZLrbcqrt/1jDmzPTdudKbVD0=', N'0O/ZGwhScZRGbsmiUEckOg==');
SET IDENTITY_INSERT [dbo].[Credentials] OFF;

SET IDENTITY_INSERT [dbo].[Roles] ON;
INSERT INTO [dbo].[Roles] ([Id], [Code], [Name], [Position]) VALUES (1, N'Developer', N'Developer', 100);
INSERT INTO [dbo].[Roles] ([Id], [Code], [Name], [Position]) VALUES (2, N'Administrator', N'Administrator', 200);
INSERT INTO [dbo].[Roles] ([Id], [Code], [Name], [Position]) VALUES (3, N'ContentManager', N'Content manager', 300);
SET IDENTITY_INSERT [dbo].[Roles] OFF;

INSERT INTO [UserRoles] ([UserId], [RoleId]) VALUES (1, 1);
INSERT INTO [UserRoles] ([UserId], [RoleId]) VALUES (1, 2);

SET IDENTITY_INSERT [dbo].[Permissions] ON;
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (1, N'DoAnything', N'Do anything',100);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (2, N'ManagePermissions', N'Manage permissions',200);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (3, N'ManageRoles', N'Manage roles',300);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (4, N'ManageUsers', N'Manage users',400);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (5, N'ManageConfigurations', N'Manage configurations',500);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (6, N'ManageCultures', N'Manage cultures',600);
SET IDENTITY_INSERT [dbo].[Permissions] OFF;

INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 1);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (2, 1);

SET IDENTITY_INSERT [dbo].[Configurations] ON;
INSERT INTO [dbo].[Configurations] ([Id], [Code], [Name]) VALUES (1, N'Email', N'Email');
INSERT INTO [dbo].[Configurations] ([Id], [Code], [Name]) VALUES (2, N'Globalization', N'Globalization');
SET IDENTITY_INSERT [dbo].[Configurations] OFF;

SET IDENTITY_INSERT [dbo].[Variables] ON;
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (1, 1, N'SmtpServer', N'SMTP server', N'test', 1);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (2, 1, N'SmtpPort', N'SMTP port', N'25', 2);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (3, 1, N'SmtpUseSsl', N'SMTP use SSL', N'no', 3);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (4, 1, N'SmtpLogin', N'SMTP login', N'test', 4);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (5, 1, N'SmtpPassword', N'SMTP password', N'test', 5);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (6, 1, N'SmtpSenderEmail', N'SMTP sender email', N'test', 6);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (7, 1, N'SmtpSenderName', N'SMTP sender name', N'test', 7);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (8, 2, N'SpecifyCultureInUrl', N'Specify culture in URL', N'yes', 6);
SET IDENTITY_INSERT [dbo].[Variables] OFF;

INSERT INTO [dbo].[Cultures] ([Id], [Name], [IsNeutral], [IsFrontendDefault], [IsBackendDefault]) VALUES (N'__', N'Neutral', 1, 0, 0);
INSERT INTO [dbo].[Cultures] ([Id], [Name], [IsNeutral], [IsFrontendDefault], [IsBackendDefault]) VALUES (N'en', N'English', 0, 1, 1);

--
-- Extension: Platformus.Website
-- Version: 2.6.0
--

SET IDENTITY_INSERT [dbo].[Permissions] ON;
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (7, N'ManageEndpoints', N'Manage endpoints',700);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (8, N'ManageObjects', N'Manage objects',800);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (9, N'ManageDataTypes', N'Manage data types',900);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (10, N'ManageClasses', N'Manage classes',1000);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (11, N'ManageMenus', N'Manage menus',1100);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (12, N'ManageForms', N'Manage forms',1200);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (13, N'ManageFileManager', N'Manage file manager',1300);
SET IDENTITY_INSERT [dbo].[Permissions] OFF;

INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 8);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 11);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 12);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 13);

SET IDENTITY_INSERT [dbo].[DataTypes] ON;
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (1, N'string', N'singleLinePlainText', N'Single line plain text', 1);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (2, N'string', N'multilinePlainText', N'Multiline plain text', 2);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (3, N'string', N'html', N'Html', 3);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (4, N'integer', N'integerNumber', N'Integer number', 4);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (5, N'decimal', N'decimalNumber', N'Decimal number', 5);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (6, N'integer', N'booleanFlag', N'Boolean flag', 6);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (7, N'datetime', N'date', N'Date', 7);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (8, N'datetime', N'dateTime', N'DateTime', 8);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (9, N'string', N'image', N'Image', 9);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (10, N'string', N'sourceCode', N'Source code', 10);
SET IDENTITY_INSERT [dbo].[DataTypes] OFF;

SET IDENTITY_INSERT [dbo].[DataTypeParameters] ON;
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (1, 1, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (2, 1, N'numericTextBox', N'MaxLength', N'Max length');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (3, 2, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (4, 2, N'numericTextBox', N'MaxLength', N'Max length');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (5, 4, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (6, 4, N'numericTextBox', N'MinValue', N'Min value');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (7, 4, N'numericTextBox', N'MaxValue', N'Max value');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (8, 5, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (9, 5, N'numericTextBox', N'MinValue', N'Min value');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (10, 5, N'numericTextBox', N'MaxValue', N'Max value');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (11, 7, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (12, 8, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (13, 9, N'numericTextBox', N'Width', N'Width');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (14, 9, N'numericTextBox', N'Height', N'Height');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (15, 10, N'textBox', N'Mode', N'Mode');
SET IDENTITY_INSERT [dbo].[DataTypeParameters] OFF;

SET IDENTITY_INSERT [dbo].[FieldTypes] ON;
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position], [ValidatorCSharpClassName]) VALUES (1, N'TextBox', N'Text box', 1, NULL);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position], [ValidatorCSharpClassName]) VALUES (2, N'TextArea', N'Text area', 2, NULL);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position], [ValidatorCSharpClassName]) VALUES (3, N'Checkbox', N'Checkbox', 3, NULL);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position], [ValidatorCSharpClassName]) VALUES (4, N'RadioButtonList', N'Radio button list', 4, NULL);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position], [ValidatorCSharpClassName]) VALUES (5, N'DropDownList', N'Drop down list', 5, NULL);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position], [ValidatorCSharpClassName]) VALUES (6, N'FileUpload', N'File upload', 6, NULL);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position], [ValidatorCSharpClassName]) VALUES (7, N'ReCAPTCHA', N'ReCAPTCHA', 7, N'Platformus.Website.Frontend.FieldValidators.ReCaptchaFieldValidator');
SET IDENTITY_INSERT [dbo].[FieldTypes] OFF;

--
-- Extension: Platformus.ECommerce
-- Version: 2.6.0
--

SET IDENTITY_INSERT [dbo].[Permissions] ON;
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (14, N'ManageCategories', N'Manage categories',1400);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (15, N'ManageProducts', N'Manage products',1500);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (16, N'ManageOrderStates', N'Manage order states',1600);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (17, N'ManagePaymentMethods', N'Manage payment methods',1700);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (18, N'ManageDeliveryMethods', N'Manage delivery methods',1800);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (19, N'ManageCarts', N'Manage carts',1900);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (20, N'ManageOrders', N'Manage orders',2000);
SET IDENTITY_INSERT [dbo].[Permissions] OFF;

COMMIT;