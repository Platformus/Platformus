BEGIN TRANSACTION;

--
-- Extension: Platformus.Core
-- Version: 3.0.0
--

INSERT INTO public."Users" ("Id","Name","Created") VALUES (1,'Administrator','2017-01-01 00:00:00.000000');
ALTER SEQUENCE "Users_Id_seq" RESTART WITH 2;
INSERT INTO public."CredentialTypes" ("Id","Code","Name","Position") VALUES (1,'Email','Email',1);
ALTER SEQUENCE "CredentialTypes_Id_seq" RESTART WITH 2;
INSERT INTO public."Credentials" ("Id","UserId","CredentialTypeId","Identifier","Secret","Extra") VALUES (1,1,1,'admin@platformus.net','8lE3xN2Ijiv/Y/aIGwaZLrbcqrt/1jDmzPTdudKbVD0=','0O/ZGwhScZRGbsmiUEckOg==');
ALTER SEQUENCE "Credentials_Id_seq" RESTART WITH 2;
INSERT INTO public."Roles" ("Id","Code","Name","Position") VALUES (1,'Developer','Developer',100);
INSERT INTO public."Roles" ("Id","Code","Name","Position") VALUES (2,'Administrator','Administrator',200);
INSERT INTO public."Roles" ("Id","Code","Name","Position") VALUES (3,'ContentManager','Content manager',300);
ALTER SEQUENCE "Roles_Id_seq" RESTART WITH 4;
INSERT INTO public."UserRoles" ("UserId","RoleId") VALUES (1,1);
INSERT INTO public."UserRoles" ("UserId","RoleId") VALUES (1,2);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (1,'DoAnything','Do anything',100);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (2,'ManagePermissions','Manage permissions',200);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (3,'ManageRoles','Manage roles',300);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (4,'ManageUsers','Manage users',400);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (5,'ManageConfigurations','Manage configurations',500);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (6,'ManageCultures','Manage cultures',600);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (1,1);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (2,1);
INSERT INTO public."Configurations" ("Id","Code","Name") VALUES (1,'Email','Email');
INSERT INTO public."Configurations" ("Id","Code","Name") VALUES (2,'Globalization','Globalization');
ALTER SEQUENCE "Configurations_Id_seq" RESTART WITH 3;
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (1,1,'SmtpServer','SMTP server','test',1);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (2,1,'SmtpPort','SMTP port','25',2);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (3,1,'SmtpUseSsl','SMTP use SSL','no',3);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (4,1,'SmtpLogin','SMTP login','test',4);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (5,1,'SmtpPassword','SMTP password','test',5);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (6,1,'SmtpSenderEmail','SMTP sender email','test',6);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (7,1,'SmtpSenderName','SMTP sender name','test',7);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (8,2,'SpecifyCultureInUrl','Specify culture in URL','yes',1);
ALTER SEQUENCE "Variables_Id_seq" RESTART WITH 9;
INSERT INTO public."Cultures" ("Id","Name","IsNeutral","IsFrontendDefault","IsBackendDefault") VALUES ('__','Neutral',TRUE,FALSE,FALSE);
INSERT INTO public."Cultures" ("Id","Name","IsNeutral","IsFrontendDefault","IsBackendDefault") VALUES ('en','English',FALSE,TRUE,TRUE);

--
-- Extension: Platformus.Website
-- Version: 3.0.0
--

INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (7,'ManageEndpoints','Manage endpoints',700);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (8,'ManageObjects','Manage objects',800);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (9,'ManageClasses','Manage classes',900);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (10,'ManageMenus','Manage menus',1000);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (11,'ManageForms','Manage forms',1100);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (12,'ManageFileManager','Manage file manager',1200);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,8);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,10);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,11);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (3,12);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (1,'string','singleLinePlainText','Single line plain text',1);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (2,'string','multilinePlainText','Multiline plain text',2);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (3,'string','html','Html',3);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (4,'integer','integerNumber','Integer number',4);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (5,'decimal','decimalNumber','Decimal number',5);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (6,'integer','booleanFlag','Boolean flag',6);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (7,'datetime','date','Date',7);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (8,'datetime','dateTime','DateTime',8);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (9,'string','image','Image',9);
INSERT INTO public."DataTypes" ("Id","StorageDataType","ParameterEditorCode","Name","Position") VALUES (10,'string','sourceCode','Source code',10);
ALTER SEQUENCE "DataTypes_Id_seq" RESTART WITH 11;
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (1,1,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (2,1,'integerBox','MaxLength','Max length');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (3,2,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (4,2,'integerBox','MaxLength','Max length');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (5,4,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (6,4,'integerBox','MinValue','Min value');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (7,4,'integerBox','MaxValue','Max value');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (8,5,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (9,5,'integerBox','MinValue','Min value');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (10,5,'integerBox','MaxValue','Max value');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (11,7,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (12,8,'checkbox','IsRequired','Is required');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (13,9,'integerBox','Width','Width');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (14,9,'integerBox','Height','Height');
INSERT INTO public."DataTypeParameters" ("Id","DataTypeId","ParameterEditorCode","Code","Name") VALUES (15,10,'textBox','Mode','Mode');
ALTER SEQUENCE "DataTypeParameters_Id_seq" RESTART WITH 16;
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position","ValidatorCSharpClassName") VALUES (1,'TextBox','Text box',1,NULL);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position","ValidatorCSharpClassName") VALUES (2,'TextArea','Text area',2,NULL);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position","ValidatorCSharpClassName") VALUES (3,'Checkbox','Checkbox',3,NULL);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position","ValidatorCSharpClassName") VALUES (4,'RadioButtonList','Radio button list',4,NULL);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position","ValidatorCSharpClassName") VALUES (5,'DropDownList','Drop down list',5,NULL);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position","ValidatorCSharpClassName") VALUES (6,'FileUpload','File upload',6,NULL);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position","ValidatorCSharpClassName") VALUES (7,'ReCAPTCHA','ReCAPTCHA',7,'Platformus.Website.Frontend.FieldValidators.ReCaptchaFieldValidator');
ALTER SEQUENCE "FieldTypes_Id_seq" RESTART WITH 8;

--
-- Extension: Platformus.ECommerce
-- Version: 3.0.0
--

INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (13,'ManageCategories','Manage categories',1300);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (14,'ManageProducts','Manage products',1400);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (15,'ManageOrderStates','Manage order states',1500);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (16,'ManagePaymentMethods','Manage payment methods',1600);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (17,'ManageDeliveryMethods','Manage delivery methods',1700);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (18,'ManageCarts','Manage carts',1800);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (19,'ManageOrders','Manage orders',1900);
ALTER SEQUENCE "Permissions_Id_seq" RESTART WITH 20;

COMMIT;