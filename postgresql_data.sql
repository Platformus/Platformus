BEGIN TRANSACTION;
--
-- Extension: Platformus.Configurations
-- Version: alpha-18
--
INSERT INTO public."Configurations" ("Id","Code","Name") VALUES (1,'Email','Email');
INSERT INTO public."Configurations" ("Id","Code","Name") VALUES (2,'Globalization','Globalization');
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (1,1,'SmtpServer','SMTP server','test',1);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (2,1,'SmtpPort','SMTP port','25',2);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (3,1,'SmtpLogi','SMTP logi','test',3);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (4,1,'SmtpPassword','SMTP password','test',4);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (5,1,'SmtpSenderEmail','SMTP sender email','test',5);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (6,1,'SmtpSenderName','SMTP sender name','test',6);
INSERT INTO public."Variables" ("Id","ConfigurationId","Code","Name","Value","Position") VALUES (7,2,'SpecifyCultureInUrl','Specify culture in URL','yes',1);

--
-- Extension: Platformus.Security
-- Version: alpha-18
--
INSERT INTO public."Users" ("Id","Name","Created") VALUES (1,'Administrator','2017-01-01 00:00:00.000000');
INSERT INTO public."CredentialTypes" ("Id","Code","Name","Position") VALUES (1,'Email','Email',1);
INSERT INTO public."Credentials" ("Id","CredentialTypeId","Identifier","Secret","UserId") VALUES (1,1,'admin@platformus.net','21-23-2F-29-7A-57-A5-A7-43-89-4A-0E-4A-80-1F-C3',1);
INSERT INTO public."Roles" ("Id","Code","Name","Position") VALUES (1,'Administrator','Administrator',1);
INSERT INTO public."UserRoles" ("UserId","RoleId") VALUES (1,1);
INSERT INTO public."Permissions" ("Id","Code","Name","Position") VALUES (1,'DoEverything','Do everything',1);
INSERT INTO public."RolePermissions" ("RoleId","PermissionId") VALUES (1,1);

--
-- Extension: Platformus.Globalization
-- Version: alpha-18
--
INSERT INTO public."Cultures" ("Id","Code","Name","IsNeutral","IsDefault") VALUES (1,'__','Neutral',TRUE,FALSE);
INSERT INTO public."Cultures" ("Id","Code","Name","IsNeutral","IsDefault") VALUES (2,'en','English',FALSE,TRUE);

--
-- Extension: Platformus.Domain
-- Version: alpha-18
--
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (1,'string','singleLinePlainText','Single line plain text',1);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (2,'string','multilinePlainText','Multiline plain text',2);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (3,'string','html','Html',3);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (4,'string','image','Image',4);
INSERT INTO public."DataTypes" ("Id","StorageDataType","JavaScriptEditorClassName","Name","Position") VALUES (5,'datetime','date','Date',5);

--
-- Extension: Platformus.Forms
-- Version: alpha-18
--
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (1,'TextBox','Text box',1);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (2,'TextArea','Text area',2);
INSERT INTO public."FieldTypes" ("Id","Code","Name","Position") VALUES (3,'DropDownList','Drop down list',3);

COMMIT;
