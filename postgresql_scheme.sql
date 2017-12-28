--
-- Extension: Platformus.Configurations
-- Version: beta1
--
CREATE TABLE "Configurations" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
	CONSTRAINT "PK_Configurations" PRIMARY KEY ("Id")
);

ALTER TABLE "Configurations" OWNER TO postgres;

CREATE TABLE "Variables" (
    "Id" serial NOT NULL,
    "ConfigurationId" integer NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Value" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_Variable" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Variables_Configurations" FOREIGN KEY ("ConfigurationId")
        REFERENCES public."Configurations" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Variables" OWNER TO postgres;

--
-- Extension: Platformus.Security
-- Version: beta1
--
CREATE TABLE "Users" (
    "Id" serial NOT NULL,
    "Name" text NOT NULL,
    "Created" timestamp NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

ALTER TABLE "Users" OWNER TO postgres;

CREATE TABLE "CredentialTypes" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_CredentialTypes" PRIMARY KEY ("Id")
);

ALTER TABLE "CredentialTypes" OWNER TO postgres;

CREATE TABLE "Credentials" (
    "Id" serial NOT NULL,
    "UserId" integer NOT NULL,
    "CredentialTypeId" integer NOT NULL,
    "Identifier" text NOT NULL,
    "Secret" text,
    CONSTRAINT "PK_Credentials" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Credentials_Users" FOREIGN KEY ("UserId")
        REFERENCES public."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Credentials_CredentialTypes" FOREIGN KEY ("CredentialTypeId")
        REFERENCES public."CredentialTypes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Credentials" OWNER TO postgres;

CREATE TABLE "Roles" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
);

ALTER TABLE "Roles" OWNER TO postgres;

CREATE TABLE "UserRoles" (
    "UserId" integer NOT NULL,
    "RoleId" integer NOT NULL,
    CONSTRAINT "PK_UserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_UserRoles_Users" FOREIGN KEY ("UserId")
        REFERENCES public."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_UserRoles_Roles" FOREIGN KEY ("RoleId")
        REFERENCES public."Roles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "UserRoles" OWNER TO postgres;

CREATE TABLE "Permissions" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_Permissions" PRIMARY KEY ("Id")
);

ALTER TABLE "Permissions" OWNER TO postgres;

CREATE TABLE "RolePermissions" (
    "RoleId" integer NOT NULL,
    "PermissionId" integer NOT NULL,
    CONSTRAINT "PK_RolePermissions" PRIMARY KEY ("RoleId", "PermissionId"),
    CONSTRAINT "FK_RolePermissions_Roles" FOREIGN KEY ("RoleId")
        REFERENCES public."Roles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_RolePermissions_Permissions" FOREIGN KEY ("PermissionId")
        REFERENCES public."Permissions" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "RolePermissions" OWNER TO postgres;

--
-- Extension: Platformus.FileManager
-- Version: beta1
--
CREATE TABLE "Files" (
    "Id" serial NOT NULL,
    "Name" text NOT NULL,
    "Size" bigint NOT NULL,
    CONSTRAINT "PK_Files" PRIMARY KEY ("Id")
);

ALTER TABLE "Files" OWNER TO postgres;

--
-- Extension: Platformus.Globalization
-- Version: beta1
--
CREATE TABLE "Cultures" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "IsNeutral" boolean NOT NULL,
    "IsDefault" boolean NOT NULL,
    "IsBackendUi" boolean NOT NULL,
    CONSTRAINT "PK_Cultures" PRIMARY KEY ("Id")
);

ALTER TABLE "Cultures" OWNER TO postgres;

CREATE TABLE "Dictionaries" (
    "Id" serial NOT NULL,
    CONSTRAINT "PK_Dictionaries" PRIMARY KEY ("Id")
);

ALTER TABLE "Dictionaries" OWNER TO postgres;

CREATE TABLE "Localizations" (
    "Id" serial NOT NULL,
    "DictionaryId" integer NOT NULL,
    "CultureId" integer NOT NULL,
    "Value" text,
    CONSTRAINT "PK_Localizations" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Localizations_Dictionaries" FOREIGN KEY ("DictionaryId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Localizations_Cultures" FOREIGN KEY ("CultureId")
        REFERENCES public."Cultures" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Localizations" OWNER TO postgres;

--
-- Extension: Platformus.Routing
-- Version: beta1
--
CREATE TABLE "Endpoints" (
    "Id" serial NOT NULL,
    "Name" text NOT NULL,
    "UrlTemplate" text,
	"Position" integer,
	"DisallowAnonymous" boolean NOT NULL,
	"SignInUrl" text,
    "CSharpClassName" text NOT NULL,
    "Parameters" text,
    CONSTRAINT "PK_Endpoints" PRIMARY KEY ("Id")
);

ALTER TABLE "Endpoints" OWNER TO postgres;

CREATE TABLE "EndpointPermissions" (
    "EndpointId" integer NOT NULL,
    "PermissionId" integer NOT NULL,
    CONSTRAINT "PK_EndpointPermissions" PRIMARY KEY ("EndpointId", "PermissionId"),
    CONSTRAINT "FK_EndpointPermissions_Roles" FOREIGN KEY ("EndpointId")
        REFERENCES public."Endpoints" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_RolePermissions_Permissions" FOREIGN KEY ("PermissionId")
        REFERENCES public."Permissions" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "EndpointPermissions" OWNER TO postgres;

CREATE TABLE "DataSources" (
    "Id" serial NOT NULL,
    "EndpointId" integer NOT NULL,
    "Code" text NOT NULL,
    "CSharpClassName" text NOT NULL,
    "Parameters" text,
    CONSTRAINT "PK_DataSources" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_DataSources_Endpoints" FOREIGN KEY ("EndpointId")
        REFERENCES public."Endpoints" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "DataSources" OWNER TO postgres;

--
-- Extension: Platformus.Domain
-- Version: beta1
--
CREATE TABLE "Classes" (
    "Id" serial NOT NULL,
    "ClassId" integer,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "PluralizedName" text NOT NULL,
    "IsAbstract" boolean NOT NULL,
    CONSTRAINT "PK_Classes" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Classes_Classes" FOREIGN KEY ("ClassId")
        REFERENCES public."Classes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Classes" OWNER TO postgres;

CREATE TABLE "Tabs" (
    "Id" serial NOT NULL,
    "ClassId" integer NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
	CONSTRAINT "PK_Tabs" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Tabs_Classes" FOREIGN KEY ("ClassId")
        REFERENCES public."Classes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Tabs" OWNER TO postgres;

CREATE TABLE "DataTypes" (
    "Id" serial NOT NULL,
    "StorageDataType" text NOT NULL,
    "JavaScriptEditorClassName" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_DataTypes" PRIMARY KEY ("Id")
);

ALTER TABLE "DataTypes" OWNER TO postgres;

CREATE TABLE "DataTypeParameters" (
    "Id" serial NOT NULL,
    "DataTypeId" integer NOT NULL,
    "JavaScriptEditorClassName" text NOT NULL,
    "Code" text NOT NULL,
    "Name" text,
    CONSTRAINT "PK_DataTypeParameters" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_DataTypeParameters_DataTypes_DataTypeId" FOREIGN KEY ("DataTypeId")
        REFERENCES public."DataTypes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "DataTypeParameters" OWNER TO postgres;

CREATE TABLE "Members" (
    "Id" serial NOT NULL,
    "ClassId" integer NOT NULL,
    "TabId" integer,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    "PropertyDataTypeId" integer,
    "IsPropertyLocalizable" boolean,
    "IsPropertyVisibleInList" boolean,
    "RelationClassId" integer,
    "IsRelationSingleParent" boolean,
    "MinRelatedObjectsNumber" integer,
    "MaxRelatedObjectsNumber" integer,
    CONSTRAINT "PK_Members" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Members_Classes_ClassId" FOREIGN KEY ("ClassId")
        REFERENCES public."Classes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Members_Tabs" FOREIGN KEY ("TabId")
        REFERENCES public."Tabs" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Members_DataTypes" FOREIGN KEY ("PropertyDataTypeId")
        REFERENCES public."DataTypes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Members_Classes_RelationClassId" FOREIGN KEY ("RelationClassId")
        REFERENCES public."Classes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Members" OWNER TO postgres;

CREATE TABLE "DataTypeParameterValues" (
    "Id" serial NOT NULL,
    "DataTypeParameterId" integer NOT NULL,
    "MemberId" integer NOT NULL,
    "Value" text NOT NULL,
    CONSTRAINT "PK_DataTypeParameterValues" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_DataTypeParameterValues_DataTypeParameters_DataTypeParameterId" FOREIGN KEY ("DataTypeParameterId")
        REFERENCES public."DataTypeParameters" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_DataTypeParameterValues_Members_MemberId" FOREIGN KEY ("MemberId")
        REFERENCES public."Members" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "DataTypeParameterValues" OWNER TO postgres;

CREATE TABLE "Objects" (
    "Id" serial NOT NULL,
    "ClassId" integer NOT NULL,
    CONSTRAINT "PK_Objects" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Objects_Classes" FOREIGN KEY ("ClassId")
        REFERENCES public."Classes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Objects" OWNER TO postgres;

CREATE TABLE "Properties" (
    "Id" serial NOT NULL,
    "ObjectId" integer NOT NULL,
    "MemberId" integer NOT NULL,
    "IntegerValue" integer,
    "DecimalValue" real,
    "StringValueId" integer,
    "DateTimeValue" timestamp,
    CONSTRAINT "PK_Properties" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Properties_Objects" FOREIGN KEY ("ObjectId")
        REFERENCES public."Objects" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Properties_Members" FOREIGN KEY ("MemberId")
        REFERENCES public."Members" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Properties_Dictionaries" FOREIGN KEY ("StringValueId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Properties" OWNER TO postgres;

CREATE TABLE "Relations" (
    "Id" serial NOT NULL,
    "MemberId" integer NOT NULL,
    "PrimaryId" integer NOT NULL,
    "ForeignId" integer NOT NULL,
    CONSTRAINT "PK_Relations" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Relations_Members" FOREIGN KEY ("MemberId")
        REFERENCES public."Members" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Relations_Objects_PrimaryId" FOREIGN KEY ("PrimaryId")
        REFERENCES public."Objects" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Relations_Objects_ForeignId" FOREIGN KEY ("ForeignId")
        REFERENCES public."Objects" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Relations" OWNER TO postgres;

CREATE TABLE "SerializedObjects" (
    "CultureId" integer NOT NULL,
    "ObjectId" integer NOT NULL,
    "ClassId" integer NOT NULL,
    "UrlPropertyStringValue" text,
    "SerializedProperties" text,
    CONSTRAINT "PK_SerializedObjects" PRIMARY KEY ("CultureId", "ObjectId"),
    CONSTRAINT "FK_SerializedObjects_Cultures" FOREIGN KEY ("CultureId")
        REFERENCES public."Cultures" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_SerializedObjects_Objects" FOREIGN KEY ("ObjectId")
        REFERENCES public."Objects" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_SerializedObjects_Classes" FOREIGN KEY ("ClassId")
        REFERENCES public."Classes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "SerializedObjects" OWNER TO postgres;

--
-- Extension: Platformus.Menus
-- Version: beta1
--
CREATE TABLE "Menus" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "NameId" integer NOT NULL,
    CONSTRAINT "PK_Menus" PRIMARY KEY ("Id")
);

ALTER TABLE "Menus" OWNER TO postgres;

CREATE TABLE "MenuItems" (
    "Id" serial NOT NULL,
    "MenuId" integer,
    "MenuItemId" integer,
    "NameId" integer NOT NULL,
    "Url" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_MenuItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_MenuItems_Menus" FOREIGN KEY ("MenuId")
        REFERENCES public."Menus" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_MenuItems_MenuItems" FOREIGN KEY ("MenuItemId")
        REFERENCES public."MenuItems" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_MenuItems_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "MenuItems" OWNER TO postgres;

CREATE TABLE "SerializedMenus" (
    "CultureId" integer NOT NULL,
    "MenuId" integer NOT NULL,
    "Code" text NOT NULL,
    "SerializedMenuItems" text,
    CONSTRAINT "PK_SerializedMenus" PRIMARY KEY ("CultureId", "MenuId"),
    CONSTRAINT "FK_SerializedMenus_Cultures" FOREIGN KEY ("CultureId")
        REFERENCES public."Cultures" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_SerializedMenus_Menus" FOREIGN KEY ("MenuId")
        REFERENCES public."Menus" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "SerializedMenus" OWNER TO postgres;

--
-- Extension: Platformus.Forms
-- Version: beta1
--
CREATE TABLE "Forms" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "NameId" integer NOT NULL,
	"ProduceCompletedForms" boolean NOT NULL,
    "CSharpClassName" text NOT NULL,
	"Parameters" text,
    CONSTRAINT "PK_Forms" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Forms_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Forms" OWNER TO postgres;

CREATE TABLE "FieldTypes" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_FieldTypes" PRIMARY KEY ("Id")
);

ALTER TABLE "FieldTypes" OWNER TO postgres;

CREATE TABLE "Fields" (
    "Id" serial NOT NULL,
    "FormId" integer NOT NULL,
    "FieldTypeId" integer NOT NULL,
	"Code" text NOT NULL,
    "NameId" integer NOT NULL,
    "IsRequired" boolean NOT NULL,
    "MaxLength" integer,
    "Position" integer,
    CONSTRAINT "PK_Fields" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Fields_Forms" FOREIGN KEY ("FormId")
        REFERENCES public."Forms" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Fields_FieldTypes" FOREIGN KEY ("FieldTypeId")
        REFERENCES public."FieldTypes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Fields_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Fields" OWNER TO postgres;

CREATE TABLE "FieldOptions" (
    "Id" serial NOT NULL,
    "FieldId" integer NOT NULL,
    "ValueId" integer NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_FieldOptions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_FieldOptions_Fields" FOREIGN KEY ("FieldId")
        REFERENCES public."Fields" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_FieldOptions_Dictionaries" FOREIGN KEY ("ValueId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "FieldOptions" OWNER TO postgres;

CREATE TABLE "CompletedForms" (
    "Id" serial NOT NULL,
    "FormId" integer NOT NULL,
    "Created" timestamp NOT NULL,
    CONSTRAINT "PK_CompletedForms" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CompletedForms_Forms" FOREIGN KEY ("FormId")
        REFERENCES public."Forms" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "CompletedForms" OWNER TO postgres;

CREATE TABLE "CompletedFields" (
    "Id" serial NOT NULL,
    "CompletedFormId" integer NOT NULL,
    "FieldId" integer NOT NULL,
    "Value" text,
    CONSTRAINT "PK_CompletedFields" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CompletedFields_CompletedForms" FOREIGN KEY ("CompletedFormId")
        REFERENCES public."CompletedForms" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_CompletedFields_Fields" FOREIGN KEY ("FieldId")
        REFERENCES public."Fields" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "CompletedFields" OWNER TO postgres;

CREATE TABLE "SerializedForms" (
    "CultureId" integer NOT NULL,
    "FormId" integer NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "SerializedFields" text,
    CONSTRAINT "PK_SerializedForms" PRIMARY KEY ("CultureId", "FormId"),
    CONSTRAINT "FK_SerializedForms_Cultures" FOREIGN KEY ("CultureId")
        REFERENCES public."Cultures" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_SerializedForms_Forms" FOREIGN KEY ("FormId")
        REFERENCES public."Forms" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "SerializedForms" OWNER TO postgres;