BEGIN TRANSACTION;
--
-- Extension: Platformus.Configurations
-- Version: beta1
--
SET IDENTITY_INSERT [dbo].[Configurations] ON;
INSERT INTO [dbo].[Configurations] ([Id], [Code], [Name]) VALUES (1, N'Email', N'Email');
INSERT INTO [dbo].[Configurations] ([Id], [Code], [Name]) VALUES (2, N'Globalization', N'Globalization');
SET IDENTITY_INSERT [dbo].[Configurations] OFF;

SET IDENTITY_INSERT [dbo].[Variables] ON;
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (1, 1, N'SmtpServer', N'SMTP server', N'test', 1);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (2, 1, N'SmtpPort', N'SMTP port', N'25', 2);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (3, 1, N'SmtpLogin', N'SMTP login', N'test', 3);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (4, 1, N'SmtpPassword', N'SMTP password', N'test', 4);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (5, 1, N'SmtpSenderEmail', N'SMTP sender email', N'test', 5);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (6, 1, N'SmtpSenderName', N'SMTP sender name', N'test', 6);
INSERT INTO [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (7, 2, N'SpecifyCultureInUrl', N'Specify culture in URL', N'yes', 6);
SET IDENTITY_INSERT [dbo].[Variables] OFF;

--
-- Extension: Platformus.Security
-- Version: beta1
--
SET IDENTITY_INSERT [dbo].[Users] ON;
INSERT INTO [dbo].[Users] ([Id], [Name], [Created]) VALUES (1, N'Administrator', N'2017-01-01 00:00:00.000');
SET IDENTITY_INSERT [dbo].[Users] OFF;

SET IDENTITY_INSERT [dbo].[CredentialTypes] ON;
INSERT INTO [dbo].[CredentialTypes] ([Id], [Code], [Name], [Position]) VALUES (1, N'Email', N'Email', 1);
SET IDENTITY_INSERT [dbo].[CredentialTypes] OFF;

SET IDENTITY_INSERT [dbo].[Credentials] ON;
INSERT INTO [dbo].[Credentials] ([Id], [UserId], [CredentialTypeId], [Identifier], [Secret]) VALUES (1, 1, 1, N'admin@platformus.net', N'21-23-2F-29-7A-57-A5-A7-43-89-4A-0E-4A-80-1F-C3');
SET IDENTITY_INSERT [dbo].[Credentials] OFF;

SET IDENTITY_INSERT [dbo].[Roles] ON;
INSERT INTO [dbo].[Roles] ([Id], [Code], [Name], [Position]) VALUES (1, N'Administrator', N'Administrator', 100);
INSERT INTO [dbo].[Roles] ([Id], [Code], [Name], [Position]) VALUES (2, N'ApplicationOwner', N'Application owner', 200);
INSERT INTO [dbo].[Roles] ([Id], [Code], [Name], [Position]) VALUES (3, N'ContentManager', N'Content manager', 300);
SET IDENTITY_INSERT [dbo].[Roles] OFF;

INSERT INTO [UserRoles] ([UserId], [RoleId]) VALUES (1, 1);
INSERT INTO [UserRoles] ([UserId], [RoleId]) VALUES (1, 2);

SET IDENTITY_INSERT [dbo].[Permissions] ON;
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (1, N'BrowseBackend', N'Browse backend',1);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (2, N'DoEverything', N'Do everything',100);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (3, N'BrowseConfigurations', N'Browse configurations',200);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (4, N'BrowsePermissions', N'Browse permissions',300);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (5, N'BrowseRoles', N'Browse roles',310);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (6, N'BrowseUsers', N'Browse users',320);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (7, N'BrowseFileManager', N'Browse file manager',400);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (8, N'BrowseCultures', N'Browse cultures',500);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (9, N'BrowseObjects', N'Browse objects',600);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (10, N'BrowseDataTypes', N'Browse data types',610);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (11, N'BrowseClasses', N'Browse classes',620);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (12, N'BrowseEndpoints', N'Browse endpoints',630);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (13, N'BrowseMenus', N'Browse menus',700);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (14, N'BrowseForms', N'Browse forms',800);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (15, N'BrowseViews', N'Browse views',900);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (16, N'BrowseStyles', N'Browse styles',910);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (17, N'BrowseScripts', N'Browse scripts',920);
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (18, N'BrowseBundles', N'Browse bundles',930);
SET IDENTITY_INSERT [dbo].[Permissions] OFF;

INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 1);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (2, 2);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 7);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 9);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 13);
INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (3, 14);

--
-- Extension: Platformus.Globalization
-- Version: beta1
--
SET IDENTITY_INSERT [dbo].[Cultures] ON;
INSERT INTO [dbo].[Cultures] ([Id], [Code], [Name], [IsNeutral], [IsDefault], [IsBackendUi]) VALUES (1, N'__', N'Neutral', 1, 0, 0);
INSERT INTO [dbo].[Cultures] ([Id], [Code], [Name], [IsNeutral], [IsDefault], [IsBackendUi]) VALUES (2, N'en', N'English', 0, 1, 1);
SET IDENTITY_INSERT [dbo].[Cultures] OFF;

--
-- Extension: Platformus.Domain
-- Version: beta1
--
SET IDENTITY_INSERT [dbo].[DataTypes] ON;
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (1, N'string', N'singleLinePlainText', N'Single line plain text', 1);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (2, N'string', N'multilinePlainText', N'Multiline plain text', 2);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (3, N'string', N'html', N'Html', 3);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (4, N'integer', N'integerNumber', N'Integer number', 4);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (5, N'decimal', N'decimalNumber', N'Decimal number', 5);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (6, N'integer', N'booleanFlag', N'Boolean flag', 6);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (7, N'string', N'image', N'Image', 7);
INSERT INTO [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (8, N'datetime', N'date', N'Date', 8);
SET IDENTITY_INSERT [dbo].[DataTypes] OFF;

SET IDENTITY_INSERT [dbo].[DataTypeParameters] ON;
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (1, 1, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (2, 1, N'numericTextBox', N'MaxLength', N'Max length');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (3, 2, N'checkbox', N'IsRequired', N'Is required');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (4, 2, N'numericTextBox', N'MaxLength', N'Max length');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (5, 7, N'numericTextBox', N'Width', N'Width');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (6, 7, N'numericTextBox', N'Height', N'Height');
INSERT INTO [dbo].[DataTypeParameters] ([Id], [DataTypeId], [JavaScriptEditorClassName], [Code], [Name]) VALUES (7, 8, N'checkbox', N'IsRequired', N'Is required');
SET IDENTITY_INSERT [dbo].[DataTypeParameters] OFF;

--
-- Extension: Platformus.Forms
-- Version: beta1
--
SET IDENTITY_INSERT [dbo].[FieldTypes] ON;
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (1, N'TextBox', N'Text box', 1);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (2, N'TextArea', N'Text area', 2);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (3, N'Checkbox', N'Checkbox', 3);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (4, N'RadioButtonList', N'Radio button list', 4);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (5, N'DropDownList', N'Drop down list', 5);
INSERT INTO [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (6, N'FileUpload', N'File upload', 6);
SET IDENTITY_INSERT [dbo].[FieldTypes] OFF;

COMMIT;
