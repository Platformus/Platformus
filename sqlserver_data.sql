BEGIN TRANSACTION;
--
-- Extension: Platformus.Configurations
-- Version: alpha-18
--
SET IDENTITY_INSERT [dbo].[Configurations] ON;
INSERT [dbo].[Configurations] ([Id], [Code], [Name]) VALUES (1, N'Email', N'Email');
INSERT [dbo].[Configurations] ([Id], [Code], [Name]) VALUES (2, N'Globalization', N'Globalization');
SET IDENTITY_INSERT [dbo].[Configurations] OFF;

SET IDENTITY_INSERT [dbo].[Variables] ON;
INSERT [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (1, 1, N'SmtpServer', N'SMTP server', N'test', 1);
INSERT [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (2, 1, N'SmtpPort', N'SMTP port', N'25', 2);
INSERT [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (3, 1, N'SmtpLogin', N'SMTP login', N'test', 3);
INSERT [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (4, 1, N'SmtpPassword', N'SMTP password', N'test', 4);
INSERT [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (5, 1, N'SmtpSenderEmail', N'SMTP sender email', N'test', 5);
INSERT [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (6, 1, N'SmtpSenderName', N'SMTP sender name', N'test', 6);
INSERT [dbo].[Variables] ([Id], [ConfigurationId], [Code], [Name], [Value], [Position]) VALUES (7, 2, N'SpecifyCultureInUrl', N'Specify culture in URL', N'yes', 6);
SET IDENTITY_INSERT [dbo].[Variables] OFF;

--
-- Extension: Platformus.Security
-- Version: alpha-18
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
INSERT INTO [dbo].[Roles] ([Id], [Code], [Name], [Position]) VALUES (1, N'Administrator', N'Administrator', 1);
SET IDENTITY_INSERT [dbo].[Roles] OFF;

INSERT INTO [UserRoles] ([UserId], [RoleId]) VALUES (1, 1);

SET IDENTITY_INSERT [dbo].[Permissions] ON;
INSERT INTO [dbo].[Permissions] ([Id], [Code], [Name], [Position]) VALUES (1, N'DoEverything', N'Do everything', 1);
SET IDENTITY_INSERT [dbo].[Permissions] OFF;

INSERT INTO [dbo].[RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 1);

--
-- Extension: Platformus.Globalization
-- Version: alpha-18
--
SET IDENTITY_INSERT [dbo].[Cultures] ON;
INSERT [dbo].[Cultures] ([Id], [Code], [Name], [IsNeutral], [IsDefault]) VALUES (1, N'__', N'Neutral', 1, 0);
INSERT [dbo].[Cultures] ([Id], [Code], [Name], [IsNeutral], [IsDefault]) VALUES (2, N'en', N'English', 0, 1);
SET IDENTITY_INSERT [dbo].[Cultures] OFF;

--
-- Extension: Platformus.Domain
-- Version: alpha-18
--
SET IDENTITY_INSERT [dbo].[DataTypes] ON;
INSERT [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (1, N'string', N'singleLinePlainText', N'Single line plain text', 1);
INSERT [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (2, N'string', N'multilinePlainText', N'Multiline plain text', 2);
INSERT [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (3, N'string', N'html', N'Html', 3);
INSERT [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (4, N'string', N'image', N'Image', 4);
INSERT [dbo].[DataTypes] ([Id], [StorageDataType], [JavaScriptEditorClassName], [Name], [Position]) VALUES (5, N'datetime', N'date', N'Date', 5);
SET IDENTITY_INSERT [dbo].[DataTypes] OFF;

--
-- Extension: Platformus.Forms
-- Version: alpha-18
--
SET IDENTITY_INSERT [dbo].[FieldTypes] ON;
INSERT [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (1, N'TextBox', N'Text box', 1);
INSERT [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (2, N'TextArea', N'Text area', 2);
INSERT [dbo].[FieldTypes] ([Id], [Code], [Name], [Position]) VALUES (3, N'DropDownList', N'Drop down list', 3);
SET IDENTITY_INSERT [dbo].[FieldTypes] OFF;

COMMIT;
