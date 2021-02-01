BEGIN TRANSACTION;
--
-- Extension: Platformus.Core
-- Version: 2.0.0-alpha5
--
INSERT INTO "Users" VALUES (1,'Administrator','2017-01-01 00:00:00.0000000');
INSERT INTO "CredentialTypes" VALUES (1,'Email','Email',1);
INSERT INTO "Credentials" VALUES (1,1,1,'admin@platformus.net','8lE3xN2Ijiv/Y/aIGwaZLrbcqrt/1jDmzPTdudKbVD0=','0O/ZGwhScZRGbsmiUEckOg==');
INSERT INTO "Roles" VALUES (1,'Developer','Developer',100);
INSERT INTO "Roles" VALUES (2,'Administrator','Administrator',200);
INSERT INTO "Roles" VALUES (3,'ContentManager','Content manager',300);
INSERT INTO "UserRoles" VALUES (1,1);
INSERT INTO "UserRoles" VALUES (1,2);
INSERT INTO "Permissions" VALUES (1,'DoAnything','Do anything',100);
INSERT INTO "Permissions" VALUES (2,'ManagePermissions','Manage permissions',200);
INSERT INTO "Permissions" VALUES (3,'ManageRoles','Manage roles',300);
INSERT INTO "Permissions" VALUES (4,'ManageUsers','Manage users',400);
INSERT INTO "Permissions" VALUES (5,'ManageConfigurations','Manage configurations',500);
INSERT INTO "Permissions" VALUES (6,'ManageCultures','Manage cultures',600);
INSERT INTO "Permissions" VALUES (7,'ManageEndpoints','Manage endpoints',700);
INSERT INTO "Permissions" VALUES (8,'ManageObjects','Manage objects',800);
INSERT INTO "Permissions" VALUES (9,'ManageDataTypes','Manage data types',900);
INSERT INTO "Permissions" VALUES (10,'ManageClasses','Manage classes',1000);
INSERT INTO "Permissions" VALUES (11,'ManageMenus','Manage menus',1100);
INSERT INTO "Permissions" VALUES (12,'ManageForms','Manage forms',1200);
INSERT INTO "Permissions" VALUES (13,'ManageFileManager','Manage file manager',1300);
INSERT INTO "Permissions" VALUES (14,'ManageCatalogs','Manage catalogs',1400);
INSERT INTO "Permissions" VALUES (15,'ManageCategories','Manage categories',1500);
INSERT INTO "Permissions" VALUES (16,'ManageProducts','Manage products',1600);
INSERT INTO "Permissions" VALUES (17,'ManageOrderStates','Manage order states',1700);
INSERT INTO "Permissions" VALUES (18,'ManagePaymentMethods','Manage payment methods',1800);
INSERT INTO "Permissions" VALUES (19,'ManageDeliveryMethods','Manage delivery methods',1900);
INSERT INTO "Permissions" VALUES (20,'ManageCarts','Manage carts',2000);
INSERT INTO "Permissions" VALUES (21,'ManageOrders','Manage orders',2100);
INSERT INTO "RolePermissions" VALUES (1,1);
INSERT INTO "RolePermissions" VALUES (2,1);
INSERT INTO "RolePermissions" VALUES (3,8);
INSERT INTO "RolePermissions" VALUES (3,11);
INSERT INTO "RolePermissions" VALUES (3,12);
INSERT INTO "RolePermissions" VALUES (3,13);
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
-- Version: 2.0.0-alpha5
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
INSERT INTO "FieldTypes" VALUES (1,'TextBox','Text box',1,NULL);
INSERT INTO "FieldTypes" VALUES (2,'TextArea','Text area',2,NULL);
INSERT INTO "FieldTypes" VALUES (3,'Checkbox','Checkbox',3,NULL);
INSERT INTO "FieldTypes" VALUES (4,'RadioButtonList','Radio button list',4,NULL);
INSERT INTO "FieldTypes" VALUES (5,'DropDownList','Drop down list',5,NULL);
INSERT INTO "FieldTypes" VALUES (6,'FileUpload','File upload',6,NULL);
INSERT INTO "FieldTypes" VALUES (7,'ReCAPTCHA','ReCAPTCHA',7,'Platformus.Website.Frontend.FormHandlers.ReCaptchaFieldValidator');

COMMIT;
