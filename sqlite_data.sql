BEGIN TRANSACTION;
--
-- Extension: Platformus.Core
-- Version: 2.0.0-alpha1
--
INSERT INTO "Users" VALUES (1,'Administrator','2017-01-01 00:00:00.0000000');
INSERT INTO "CredentialTypes" VALUES (1,'Email','Email',1);
INSERT INTO "Credentials" VALUES (1,1,1,'admin@platformus.net','8lE3xN2Ijiv/Y/aIGwaZLrbcqrt/1jDmzPTdudKbVD0=','0O/ZGwhScZRGbsmiUEckOg==');
INSERT INTO "Roles" VALUES (1,'Administrator','Administrator',100);
INSERT INTO "Roles" VALUES (2,'ApplicationOwner','Application owner',200);
INSERT INTO "Roles" VALUES (3,'ContentManager','Content manager',300);
INSERT INTO "UserRoles" VALUES (1,1);
INSERT INTO "UserRoles" VALUES (1,2);
INSERT INTO "Permissions" VALUES (2,'DoAnything','Do anything',100);
INSERT INTO "Permissions" VALUES (3,'ManagePermissions','Manage permissions',200);
INSERT INTO "Permissions" VALUES (4,'ManageRoles','Manage roles',210);
INSERT INTO "Permissions" VALUES (5,'ManageUsers','Manage users',220);
INSERT INTO "Permissions" VALUES (6,'ManageConfigurations','Manage configurations',300);
INSERT INTO "Permissions" VALUES (7,'ManageCultures','Manage cultures',400);
INSERT INTO "Permissions" VALUES (8,'ManageEndpoints','Manage endpoints',500);
INSERT INTO "Permissions" VALUES (9,'ManageObjects','Manage objects',600);
INSERT INTO "Permissions" VALUES (10,'ManageDataTypes','Manage data types',610);
INSERT INTO "Permissions" VALUES (11,'ManageClasses','Manage classes',620);
INSERT INTO "Permissions" VALUES (12,'ManageMenus','Manage menus',700);
INSERT INTO "Permissions" VALUES (13,'ManageForms','Manage forms',800);
INSERT INTO "Permissions" VALUES (14,'ManageFileManager','Manage file manager',900);
INSERT INTO "Permissions" VALUES (15,'ManageViews','Manage views',1000);
INSERT INTO "Permissions" VALUES (16,'ManageStyles','Manage styles',1010);
INSERT INTO "Permissions" VALUES (17,'ManageScripts','Manage scripts',1020);
INSERT INTO "Permissions" VALUES (18,'ManageBundles','Manage bundles',1030);
INSERT INTO "Permissions" VALUES (19,'ManageCatalogs','Manage catalogs',1100);
INSERT INTO "Permissions" VALUES (20,'ManageCategories','Manage categories',1110);
INSERT INTO "Permissions" VALUES (21,'ManageProducts','Manage products',1120);
INSERT INTO "Permissions" VALUES (22,'ManageCarts','Manage carts',1130);
INSERT INTO "Permissions" VALUES (23,'ManageOrderStates','Manage order states',1140);
INSERT INTO "Permissions" VALUES (24,'ManagePaymentMethods','Manage payment methods',1150);
INSERT INTO "Permissions" VALUES (25,'ManageDeliveryMethods','Manage delivery methods',1160);
INSERT INTO "Permissions" VALUES (26,'ManageOrders','Manage orders',1170);
INSERT INTO "RolePermissions" VALUES (1,1);
INSERT INTO "RolePermissions" VALUES (2,2);
INSERT INTO "RolePermissions" VALUES (3,9);
INSERT INTO "RolePermissions" VALUES (3,12);
INSERT INTO "RolePermissions" VALUES (3,13);
INSERT INTO "RolePermissions" VALUES (3,14);
INSERT INTO "Configurations" VALUES (1,'Email','Email');
INSERT INTO "Configurations" VALUES (2,'Globalization','Globalization');
INSERT INTO "Variables" VALUES (1,1,'SmtpServer','SMTP server','test',1);
INSERT INTO "Variables" VALUES (2,1,'SmtpPort','SMTP port','25',2);
INSERT INTO "Variables" VALUES (3,1,'SmtpUseSsl','SMTP use SSL','no',3);
INSERT INTO "Variables" VALUES (4,1,'SmtpLogin','SMTP login','test',4);
INSERT INTO "Variables" VALUES (5,1,'SmtpPassword','SMTP password','test',5);
INSERT INTO "Variables" VALUES (6,1,'SmtpSenderEmail','SMTP sender email','test',6);
INSERT INTO "Variables" VALUES (7,1,'SmtpSenderName','SMTP sender name','test',7);
INSERT INTO "Variables" VALUES (8,2,'SpecifyCultureInUrl','Specify culture in URL','yes',1);
INSERT INTO "Cultures" VALUES (1,'__','Neutral',1,0,0);
INSERT INTO "Cultures" VALUES (2,'en','English',0,1,1);

--
-- Extension: Platformus.Website
-- Version: 2.0.0-alpha1
--
INSERT INTO "DataTypes" VALUES (1,'string','singleLinePlainText','Single line plain text',1);
INSERT INTO "DataTypes" VALUES (2,'string','multilinePlainText','Multiline plain text',2);
INSERT INTO "DataTypes" VALUES (3,'string','html','Html',3);
INSERT INTO "DataTypes" VALUES (4,'integer','integerNumber','Integer number',4);
INSERT INTO "DataTypes" VALUES (5,'decimal','decimalNumber','Decimal number',5);
INSERT INTO "DataTypes" VALUES (6,'integer','booleanFlag','Boolean flag',6);
INSERT INTO "DataTypes" VALUES (7,'datetime','date','Date',7);
INSERT INTO "DataTypes" VALUES (8,'string','image','Image',8);
INSERT INTO "DataTypes" VALUES (9,'string','sourceCode','Source code',9);
INSERT INTO "DataTypeParameters" VALUES (1,1,'checkbox','IsRequired','Is required');
INSERT INTO "DataTypeParameters" VALUES (2,1,'numericTextBox','MaxLength','Max length');
INSERT INTO "DataTypeParameters" VALUES (3,2,'checkbox','IsRequired','Is required');
INSERT INTO "DataTypeParameters" VALUES (4,2,'numericTextBox','MaxLength','Max length');
INSERT INTO "DataTypeParameters" VALUES (5,7,'checkbox','IsRequired','Is required');
INSERT INTO "DataTypeParameters" VALUES (6,8,'numericTextBox','Width','Width');
INSERT INTO "DataTypeParameters" VALUES (7,8,'numericTextBox','Height','Height');
INSERT INTO "DataTypeParameters" VALUES (8,9,'textBox','Mode','Mode');

INSERT INTO "FieldTypes" VALUES (1,'TextBox','Text box',1);
INSERT INTO "FieldTypes" VALUES (2,'TextArea','Text area',2);
INSERT INTO "FieldTypes" VALUES (3,'Checkbox','Checkbox',3);
INSERT INTO "FieldTypes" VALUES (4,'RadioButtonList','Radio button list',4);
INSERT INTO "FieldTypes" VALUES (5,'DropDownList','Drop down list',5);
INSERT INTO "FieldTypes" VALUES (6,'FileUpload','File upload',6);

COMMIT;
