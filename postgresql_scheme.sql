--
-- Extension: Platformus.Security
-- Version: beta5
--

-- Users
CREATE TABLE "Users" (
    "Id" serial NOT NULL,
    "Name" text NOT NULL,
    "Created" timestamp NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

ALTER TABLE "Users" OWNER TO postgres;

-- CredentialTypes
CREATE TABLE "CredentialTypes" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_CredentialTypes" PRIMARY KEY ("Id")
);

ALTER TABLE "CredentialTypes" OWNER TO postgres;

-- Credentials
CREATE TABLE "Credentials" (
    "Id" serial NOT NULL,
    "UserId" integer NOT NULL,
    "CredentialTypeId" integer NOT NULL,
    "Identifier" text NOT NULL,
    "Secret" text,
    "Extra" text,
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

-- Roles
CREATE TABLE "Roles" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
);

ALTER TABLE "Roles" OWNER TO postgres;

-- UserRoles
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

-- Permissions
CREATE TABLE "Permissions" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_Permissions" PRIMARY KEY ("Id")
);

ALTER TABLE "Permissions" OWNER TO postgres;

-- RolePermissions
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
-- Extension: Platformus.Configurations
-- Version: beta5
--

-- Configurations
CREATE TABLE "Configurations" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
	CONSTRAINT "PK_Configurations" PRIMARY KEY ("Id")
);

ALTER TABLE "Configurations" OWNER TO postgres;

-- Variables
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
-- Extension: Platformus.Globalization
-- Version: beta5
--

-- Cultures
CREATE TABLE "Cultures" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "IsNeutral" boolean NOT NULL,
    "IsFrontendDefault" boolean NOT NULL,
    "IsBackendDefault" boolean NOT NULL,
    CONSTRAINT "PK_Cultures" PRIMARY KEY ("Id")
);

ALTER TABLE "Cultures" OWNER TO postgres;

-- Dictionaries
CREATE TABLE "Dictionaries" (
    "Id" serial NOT NULL,
    CONSTRAINT "PK_Dictionaries" PRIMARY KEY ("Id")
);

ALTER TABLE "Dictionaries" OWNER TO postgres;

-- Localizations
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
-- Version: beta5
--

-- Endpoints
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

-- EndpointPermissions
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

-- DataSources
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
-- Version: beta5
--

-- Classes
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

-- Tabs
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

-- DataTypes
CREATE TABLE "DataTypes" (
    "Id" serial NOT NULL,
    "StorageDataType" text NOT NULL,
    "JavaScriptEditorClassName" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_DataTypes" PRIMARY KEY ("Id")
);

ALTER TABLE "DataTypes" OWNER TO postgres;

-- DataTypeParameters
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

-- Members
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

-- DataTypeParameterValues
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

-- Objects
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

-- Properties
CREATE TABLE "Properties" (
    "Id" serial NOT NULL,
    "ObjectId" integer NOT NULL,
    "MemberId" integer NOT NULL,
    "IntegerValue" integer,
    "DecimalValue" numeric,
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

-- Relations
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

-- SerializedObjects
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
-- Version: beta5
--

-- Menus
CREATE TABLE "Menus" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "NameId" integer NOT NULL,
    CONSTRAINT "PK_Menus" PRIMARY KEY ("Id")
);

ALTER TABLE "Menus" OWNER TO postgres;

-- MenuItems
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

-- SerializedMenus
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
-- Version: beta5
--

-- Forms
CREATE TABLE "Forms" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "NameId" integer NOT NULL,
    "SubmitButtonTitleId" integer NOT NULL,
	"ProduceCompletedForms" boolean NOT NULL,
    "CSharpClassName" text NOT NULL,
	"Parameters" text,
    CONSTRAINT "PK_Forms" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Forms_Dictionaries_NameId" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Forms_Dictionaries_SubmitButtonTitleId" FOREIGN KEY ("SubmitButtonTitleId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Forms" OWNER TO postgres;

-- FieldTypes
CREATE TABLE "FieldTypes" (
    "Id" serial NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_FieldTypes" PRIMARY KEY ("Id")
);

ALTER TABLE "FieldTypes" OWNER TO postgres;

-- Fields
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

-- FieldOptions
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

-- CompletedForms
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

-- CompletedFields
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

-- SerializedForms
CREATE TABLE "SerializedForms" (
    "CultureId" integer NOT NULL,
    "FormId" integer NOT NULL,
    "Code" text NOT NULL,
    "Name" text NOT NULL,
	"SubmitButtonTitle" text NOT NULL,
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

--
-- Extension: Platformus.FileManager
-- Version: beta5
--

-- Files
CREATE TABLE "Files" (
    "Id" serial NOT NULL,
    "Name" text NOT NULL,
    "Size" bigint NOT NULL,
    CONSTRAINT "PK_Files" PRIMARY KEY ("Id")
);

ALTER TABLE "Files" OWNER TO postgres;

--
-- Extension: Platformus.ECommerce
-- Version: beta5
--

-- Catalogs
CREATE TABLE "Catalogs" (
    "Id" serial NOT NULL,
    "CatalogId" integer,
	"Url" text NOT NULL,
    "NameId" integer NOT NULL,
	"CSharpClassName" text NOT NULL,
	"Parameters" text,
    "Position" integer,
    CONSTRAINT "PK_Catalogs" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Catalogs_Catalogs" FOREIGN KEY ("CatalogId")
        REFERENCES public."Catalogs" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Catalogs_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Catalogs" OWNER TO postgres;

-- Categories
CREATE TABLE "Categories" (
    "Id" serial NOT NULL,
    "CategoryId" integer,
    "NameId" integer NOT NULL,
    "Position" integer,
    CONSTRAINT "PK_Categories" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Categories_Categories" FOREIGN KEY ("CategoryId")
        REFERENCES public."Categories" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_Categories_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Categories" OWNER TO postgres;

-- Features
CREATE TABLE "Features" (
    "Id" serial NOT NULL,
	"Code" text NOT NULL,
    "NameId" integer NOT NULL,
    "Position" integer,
	CONSTRAINT "PK_Features" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_Features_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Features" OWNER TO postgres;

-- Attributes
CREATE TABLE "Attributes" (
    "Id" serial NOT NULL,
	"FeatureId" integer NOT NULL,
    "ValueId" integer NOT NULL,
    "Position" integer,
	CONSTRAINT "PK_Attributes" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_Attributes_Features" FOREIGN KEY ("FeatureId")
        REFERENCES public."Features" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Attributes_Dictionaries" FOREIGN KEY ("ValueId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Attributes" OWNER TO postgres;

-- Products
CREATE TABLE "Products" (
    "Id" serial NOT NULL,
    "CategoryId" integer NOT NULL,
    "Url" text NOT NULL,
    "Code" text NOT NULL,
    "NameId" integer NOT NULL,
    "DescriptionId" integer NOT NULL,
    "Price" numeric NOT NULL,
	"TitleId" integer NOT NULL,
	"MetaDescriptionId" integer NOT NULL,
	"MetaKeywordsId" integer NOT NULL,
	CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Products_Categories" FOREIGN KEY ("CategoryId")
        REFERENCES public."Categories" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Products_Dictionaries_NameId" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Products_Dictionaries_DescriptionId" FOREIGN KEY ("DescriptionId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Products_Dictionaries_TitleId" FOREIGN KEY ("TitleId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Products_Dictionaries_MetaDescriptionId" FOREIGN KEY ("MetaDescriptionId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Products_Dictionaries_MetaKeywordsId" FOREIGN KEY ("MetaKeywordsId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Products" OWNER TO postgres;

-- ProductAttributes
CREATE TABLE "ProductAttributes" (
    "ProductId" integer NOT NULL,
    "AttributeId" integer NOT NULL,
    CONSTRAINT "PK_ProductAttributes" PRIMARY KEY ("ProductId", "AttributeId"),
    CONSTRAINT "FK_ProductAttributes_Products" FOREIGN KEY ("ProductId")
        REFERENCES public."Products" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_ProductAttributes_Attributes" FOREIGN KEY ("AttributeId")
        REFERENCES public."Attributes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "ProductAttributes" OWNER TO postgres;

-- Photos
CREATE TABLE "Photos" (
    "Id" serial NOT NULL,
    "ProductId" integer NOT NULL,
	"Filename" text NOT NULL,
    "IsCover" boolean NOT NULL,
    "Position" integer,
	CONSTRAINT "PK_Photos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Photos_Products" FOREIGN KEY ("ProductId")
        REFERENCES public."Products" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Photos" OWNER TO postgres;

-- OrderStates
CREATE TABLE "OrderStates" (
    "Id" serial NOT NULL,
	"Code" text NOT NULL,
    "NameId" integer NOT NULL,
    "Position" integer,
	CONSTRAINT "PK_OrderStates" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_OrderStates_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "OrderStates" OWNER TO postgres;

-- PaymentMethods
CREATE TABLE "PaymentMethods" (
    "Id" serial NOT NULL,
	"Code" text NOT NULL,
    "NameId" integer NOT NULL,
    "Position" integer,
	CONSTRAINT "PK_PaymentMethods" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_PaymentMethods_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "PaymentMethods" OWNER TO postgres;

-- DeliveryMethods
CREATE TABLE "DeliveryMethods" (
    "Id" serial NOT NULL,
	"Code" text NOT NULL,
    "NameId" integer NOT NULL,
    "Position" integer,
	CONSTRAINT "PK_DeliveryMethods" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_DeliveryMethods_Dictionaries" FOREIGN KEY ("NameId")
        REFERENCES public."Dictionaries" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "DeliveryMethods" OWNER TO postgres;

-- Orders
CREATE TABLE "Orders" (
    "Id" serial NOT NULL,
    "OrderStateId" integer NOT NULL,
	"PaymentMethodId" integer NOT NULL,
	"DeliveryMethodId" integer NOT NULL,
	"CustomerFirstName" text NOT NULL,
	"CustomerLastName" text,
	"CustomerPhone" text NOT NULL,
	"CustomerEmail" text,
	"CustomerAddress" text,
	"Note" text,
    "Created" timestamp NOT NULL,
	CONSTRAINT "PK_Orders" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Orders_OrderStates" FOREIGN KEY ("OrderStateId")
        REFERENCES public."OrderStates" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Orders_PaymentMethods" FOREIGN KEY ("PaymentMethodId")
        REFERENCES public."PaymentMethods" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Orders_DeliveryMethods" FOREIGN KEY ("DeliveryMethodId")
        REFERENCES public."DeliveryMethods" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Orders" OWNER TO postgres;

-- Carts
CREATE TABLE "Carts" (
    "Id" serial NOT NULL,
    "OrderId" integer,
	"ClientSideId" text NOT NULL,
	"Created" timestamp NOT NULL,
	CONSTRAINT "PK_Carts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Carts_Orders" FOREIGN KEY ("OrderId")
        REFERENCES public."Orders" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Carts" OWNER TO postgres;

-- Positions
CREATE TABLE "Positions" (
    "Id" serial NOT NULL,
    "CartId" integer NOT NULL,
	"ProductId" integer NOT NULL,
	CONSTRAINT "PK_Positions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Positions_Carts" FOREIGN KEY ("CartId")
        REFERENCES public."Carts" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
	CONSTRAINT "FK_Positions_Products" FOREIGN KEY ("ProductId")
        REFERENCES public."Products" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "Positions" OWNER TO postgres;

-- SerializedProducts
CREATE TABLE "SerializedProducts" (
    "CultureId" integer NOT NULL,
    "ProductId" integer NOT NULL,
    "CategoryId" integer NOT NULL,
    "Url" text NOT NULL,
	"Code" text NOT NULL,
	"Name" text NOT NULL,
	"Description" text,
	"Price" numeric NOT NULL,
	"Title" text,
	"MetaDescription" text,
	"MetaKeywords" text,
    "SerializedAttributes" text,
	"SerializedPhotos" text,
    CONSTRAINT "PK_SerializedProducts" PRIMARY KEY ("CultureId", "ProductId"),
    CONSTRAINT "FK_SerializedProducts_Cultures" FOREIGN KEY ("CultureId")
        REFERENCES public."Cultures" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_SerializedProducts_Products" FOREIGN KEY ("ProductId")
        REFERENCES public."Products" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_SerializedProducts_Categories" FOREIGN KEY ("CategoryId")
        REFERENCES public."Categories" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE "SerializedProducts" OWNER TO postgres;