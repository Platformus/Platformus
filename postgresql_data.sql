BEGIN TRANSACTION;
--
-- Extension: Platformus.Security
-- Version: beta4
--
INSERT INTO public."Users" ("Id","Name","Created") VALUES (1,'Administrator','2017-01-01 00:00:00.000000');
ALTER SEQUENCE "Users_Id_seq" RESTART WITH 2;
INSERT INTO public."CredentialTypes" ("Id","Code","Name","Position") VALUES (1,'Email','Email',1);
ALTER SEQUENCE "CredentialTypes_Id_seq" RESTART WITH 2;
INSERT INTO public."Credentials" ("Id","UserId","CredentialTypeId","Identifier","Secret","Extra") VALUES (1,1,1,'admin@platformus.net','8lE3xN2Ijiv/Y/aIGwaZLrbcqrt/1jDmzPTdudKbVD0=','0O/ZGwhScZRGbsmiUEckOg==');
ALTER SEQUENCE "Credentials_Id_seq" RESTART WITH 2;
INSERT INTO public."Roles" ("Id","Code","Name","Position") VALUES (1,'Administrator','Administrator',100);
INSERT INTO public."Roles" ("Id","Code","Name","Position") VALUES (2,'ApplicationOwner','Application owner',200);
INSERT INTO public."Roles" ("Id","Code","Name","Position") VALUES (3,'ContentManager','Content manager',300);
ALTER SEQUENCE "Roles_Id_seq" RESTART WITH 4;
INSERT INTO public."UserRoles" ("UserId","RoleId") VALUES (1,1);
INSERT INTO public."UserRoles" ("UserId","RoleId") VALUES (1,2);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (1,'BrowseBackend','Browse backend',1);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (2,'DoEverything','Do everything',100);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (3,'BrowsePermissions','Browse permissions',200);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (4,'BrowseRoles','Browse roles',210);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (5,'BrowseUsers','Browse users',220);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (6,'BrowseConfigurations','Browse configurations',300);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (7,'BrowseCultures','Browse cultures',400);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (8,'BrowseEndpoints','Browse endpoints',500);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (9,'BrowseObjects','Browse objects',600);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (10,'BrowseDataTypes','Browse data types',610);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (11,'BrowseClasses','Browse classes',620);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (12,'BrowseMenus','Browse menus',700);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (13,'BrowseForms','Browse forms',800);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (14,'BrowseFileManager','Browse file manager',900);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (15,'BrowseViews','Browse views',1000);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (16,'BrowseStyles','Browse styles',1010);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (17,'BrowseScripts','Browse scripts',1020);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (18,'BrowseBundles','Browse bundles',1030);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (19,'BrowseCatalogs','Browse catalogs',1100);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (20,'BrowseCategories','Browse categories',1110);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (21,'BrowseProducts','Browse products',1120);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (22,'BrowseCarts','Browse carts',1130);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (23,'BrowseOrderStates','Browse order states',1140);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (24,'BrowsePaymentMethods','Browse payment methods',1150);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (25,'BrowseDeliveryMethods','Browse delivery methods',1160);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (26,'BrowseOrders','Browse orders',1170);
ALTER SEQUENCE "Permissions_Id_seq" RESTART WITH 27;
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (1,1);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (2,2);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,9);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,12);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,13);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,14);

--
-- Extension: Platformus.Configurations
-- Version: beta4
--
INSERT INTO public."Configurations" ("Id","Code","Name") VALUES (1,'Email','Email');
INSERT INTO public."Configurations" ("Id","Code","Name") VALUES (2,'Globalization','Globalization');
ALTER SEQUENCE "Configurations_Id_seq" RESTART WITH 3;
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (1,1,'SmtpServer','SMTP server','test',1);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (2,1,'SmtpPort','SMTP port','25',2);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (3,1,'SmtpLogi','SMTP logi','test',3);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (4,1,'SmtpPassword','SMTP password','test',4);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (5,1,'SmtpSenderEmail','SMTP sender email','test',5);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (6,1,'SmtpSenderName','SMTP sender name','test',6);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (7,2,'SpecifyCultureInUrl','Specify culture in URL','yes',1);
ALTER SEQUENCE "Variables_Id_seq" RESTART WITH 8;

--
-- Extension: Platformus.Globalization
-- Version: beta4
--
INSERT INTO public."Cultures" ("Id","Code","Name","IsNeutral","IsFrontendDefault","IsBackendDefault") VALUES (1,'__','Neutral',TRUE,FALSE,FALSE);
INSERT INTO public."Cultures" ("Id","Code","Name","IsNeutral","IsFrontendDefault","IsBackendDefault") VALUES (2,'en','English',FALSE,TRUE,TRUE);
ALTER SEQUENCE "Cultures_Id_seq" RESTART WITH 3;

--
-- Extension: Platformus.Domain
-- Version: beta4
--
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (1,'string','singleLinePlainText','Single line plain text',1);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (2,'string','multilinePlainText','Multiline plain text',2);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (3,'string','html','Html',3);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (4,'integer','integerNumber','Integer number',4);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (5,'decimal','decimalNumber','Decimal number',5);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (6,'integer','booleanFlag','Boolean flag',6);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (7,'string','image','Image',7);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (8,'datetime','date','Date',8);
ALTER SEQUENCE "DataTypes_Id_seq" RESTART WITH 9;
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","JavaScriptEditorClassName","Code","Name") VALUES (1,1,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","JavaScriptEditorClassName","Code","Name") VALUES (2,1,'numericTextBox','MaxLength','Max length');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","JavaScriptEditorClassName","Code","Name") VALUES (3,2,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","JavaScriptEditorClassName","Code","Name") VALUES (4,2,'numericTextBox','MaxLength','Max length');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","JavaScriptEditorClassName","Code","Name") VALUES (5,7,'numericTextBox','Width','Width');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","JavaScriptEditorClassName","Code","Name") VALUES (6,7,'numericTextBox','Height','Height');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","JavaScriptEditorClassName","Code","Name") VALUES (7,8,'checkbox','IsRequired','Is required');
ALTER SEQUENCE "DataTypeParameters_Id_seq" RESTART WITH 8;

--
-- Extension: Platformus.Forms
-- Version: beta4
--
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (1,'TextBox','Text box',1);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (2,'TextArea','Text area',2);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (3,'Checkbox','Checkbox',3);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (4,'RadioButtonList','Radio button list',4);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (5,'DropDownList','Drop down list',5);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (6,'FileUpload','File upload',6);
ALTER SEQUENCE "FieldTypes_Id_seq" RESTART WITH 7;

COMMIT;
