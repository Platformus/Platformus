BEGIN TRANSACTION;
--
-- Extension: Platformus.Core
-- Version: 2.6.0
--

-- ModelStates
CREATE TABLE "ModelStates" (
	"Id" TEXT NOT NULL CONSTRAINT "PK_ModelState" PRIMARY KEY,
	"Value" TEXT NOT NULL,
	"Created" TEXT NOT NULL
);

-- Users
CREATE TABLE "Users" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_User" PRIMARY KEY AUTOINCREMENT,
	"Name" TEXT NOT NULL,
	"Created" TEXT NOT NULL
);

-- CredentialTypes
CREATE TABLE "CredentialTypes" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_CredentialType" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"Position" INTEGER
);

-- Credentials
CREATE TABLE "Credentials" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Credential" PRIMARY KEY AUTOINCREMENT,
	"UserId" INTEGER NOT NULL,
	"CredentialTypeId" INTEGER NOT NULL,
	"Identifier" TEXT NOT NULL,
	"Secret" TEXT,
	"Extra" TEXT,
	CONSTRAINT "FK_Credential_User_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Credential_CredentialType_CredentialTypeId" FOREIGN KEY ("CredentialTypeId") REFERENCES "CredentialTypes" ("Id") ON DELETE CASCADE
);

-- Roles
CREATE TABLE "Roles" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Role" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"Position" INTEGER
);

-- UserRoles
CREATE TABLE "UserRoles" (
	"UserId" INTEGER NOT NULL,
	"RoleId" INTEGER NOT NULL,
	CONSTRAINT "PK_UserRole" PRIMARY KEY ("UserId", "RoleId"),
	CONSTRAINT "FK_UserRole_User_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_UserRole_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
);

-- Permissions
CREATE TABLE "Permissions" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Permission" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"Position" INTEGER
);

-- RolePermissions
CREATE TABLE "RolePermissions" (
	"RoleId" INTEGER NOT NULL,
	"PermissionId" INTEGER NOT NULL,
	CONSTRAINT "PK_RolePermission" PRIMARY KEY ("RoleId", "PermissionId"),
	CONSTRAINT "FK_RolePermission_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_RolePermission_Permission_PermissionId" FOREIGN KEY ("PermissionId") REFERENCES "Permissions" ("Id") ON DELETE CASCADE
);

-- Configurations
CREATE TABLE "Configurations" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Configuration" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL
);

-- Variables
CREATE TABLE "Variables" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Variable" PRIMARY KEY AUTOINCREMENT,
	"ConfigurationId" INTEGER NOT NULL,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"Value" TEXT NOT NULL,
	"Position" INTEGER,
	CONSTRAINT "FK_Variable_Configuration_ConfigurationId" FOREIGN KEY("ConfigurationId") REFERENCES "Configurations" ("Id") ON DELETE CASCADE
);

-- Cultures
CREATE TABLE "Cultures" (
	"Id" TEXT NOT NULL CONSTRAINT "PK_Culture" PRIMARY KEY,
	"Name" TEXT NOT NULL,
	"IsNeutral" INTEGER NOT NULL,
	"IsFrontendDefault" INTEGER NOT NULL,
	"IsBackendDefault" INTEGER NOT NULL
);

-- Dictionaries
CREATE TABLE "Dictionaries" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Dictionary" PRIMARY KEY AUTOINCREMENT
);

-- Localizations
CREATE TABLE "Localizations" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Localization" PRIMARY KEY AUTOINCREMENT,
	"DictionaryId" INTEGER NOT NULL,
	"CultureId" TEXT NOT NULL,
	"Value" TEXT,
	CONSTRAINT "FK_Localization_Dictionary_DictionaryId" FOREIGN KEY ("DictionaryId") REFERENCES "Dictionaries" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Localization_Culture_CultureId" FOREIGN KEY ("CultureId") REFERENCES "Cultures" ("Id") ON DELETE CASCADE
);

--
-- Extension: Platformus.Website
-- Version: 2.6.0
--

-- Endpoints
CREATE TABLE "Endpoints" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Endpoint" PRIMARY KEY AUTOINCREMENT,
	"Name" TEXT NOT NULL,
	"UrlTemplate" TEXT,
	"Position" INTEGER,
	"DisallowAnonymous" INTEGER NOT NULL,
	"SignInUrl" TEXT,
	"RequestProcessorCSharpClassName" TEXT NOT NULL,
	"RequestProcessorParameters" TEXT,
  "ResponseCacheCSharpClassName" TEXT,
  "ResponseCacheParameters" TEXT
);

-- EndpointPermissions
CREATE TABLE "EndpointPermissions" (
	"EndpointId" INTEGER NOT NULL,
	"PermissionId" INTEGER NOT NULL,
	CONSTRAINT "PK_EndpointPermission" PRIMARY KEY ("EndpointId", "PermissionId"),
	CONSTRAINT "FK_EndpointPermission_Endpoint_EndpointId" FOREIGN KEY ("EndpointId") REFERENCES "Endpoints" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_EndpointPermission_Permission_PermissionId" FOREIGN KEY ("PermissionId") REFERENCES "Permissions" ("Id") ON DELETE CASCADE
);

-- DataSources
CREATE TABLE "DataSources" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_DataSource" PRIMARY KEY AUTOINCREMENT,
	"EndpointId" INTEGER NOT NULL,
	"Code" TEXT NOT NULL,
	"DataProviderCSharpClassName" TEXT NOT NULL,
	"DataProviderParameters" TEXT,
	CONSTRAINT "FK_DataSource_Endpoint_EndpointId" FOREIGN KEY("EndpointId") REFERENCES "Endpoints"("Id") ON DELETE CASCADE
);

-- Classes
CREATE TABLE "Classes" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Class" PRIMARY KEY AUTOINCREMENT,
	"ClassId" INTEGER,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"PluralizedName" TEXT NOT NULL,
	"IsAbstract" INTEGER NOT NULL,
	CONSTRAINT "FK_Class_Class_ClassId" FOREIGN KEY("ClassId") REFERENCES "Classes" ("Id") ON DELETE SET NULL
);

-- Tabs
CREATE TABLE "Tabs" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Tab" PRIMARY KEY AUTOINCREMENT,
	"ClassId" INTEGER NOT NULL,
	"Name" TEXT NOT NULL,
	"Position" INTEGER,
	CONSTRAINT "FK_Tab_Class_ClassId" FOREIGN KEY("ClassId") REFERENCES "Classes" ("Id") ON DELETE CASCADE
);

-- DataTypes
CREATE TABLE "DataTypes" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_DataType" PRIMARY KEY AUTOINCREMENT,
	"StorageDataType" TEXT NOT NULL,
	"JavaScriptEditorClassName" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"Position" INTEGER
);

-- DataTypeParameters
CREATE TABLE "DataTypeParameters" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_DataTypeParameter" PRIMARY KEY AUTOINCREMENT,
	"DataTypeId" INT NOT NULL,
	"JavaScriptEditorClassName" TEXT NOT NULL,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	CONSTRAINT "FK_DataTypeParameter_DataType_DataTypeId" FOREIGN KEY("DataTypeId") REFERENCES "DataTypes" ("Id") ON DELETE CASCADE
);

-- Members
CREATE TABLE "Members" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Member" PRIMARY KEY AUTOINCREMENT,
	"ClassId" INTEGER NOT NULL,
	"TabId" INTEGER,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"Position" INTEGER,
	"PropertyDataTypeId" INTEGER,
	"IsPropertyLocalizable" INTEGER,
	"IsPropertyVisibleInList" INTEGER,
	"RelationClassId" INTEGER,
	"IsRelationSingleParent" INTEGER,
  "MinRelatedObjectsNumber" INTEGER,
  "MaxRelatedObjectsNumber" INTEGER,
	CONSTRAINT "FK_Member_Class_ClassId" FOREIGN KEY("ClassId") REFERENCES "Classes" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Member_Tab_TabId" FOREIGN KEY("TabId") REFERENCES "Tabs" ("Id") ON DELETE SET NULL,
	CONSTRAINT "FK_Member_DataType_PropertyDataTypeId" FOREIGN KEY("PropertyDataTypeId") REFERENCES "DataTypes" ("Id") ON DELETE SET NULL,
	CONSTRAINT "FK_Member_Class_RelationClassId" FOREIGN KEY("RelationClassId") REFERENCES "Classes" ("Id") ON DELETE SET NULL
);

-- DataTypeParameterValues
CREATE TABLE "DataTypeParameterValues" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_DataTypeParameterValue" PRIMARY KEY AUTOINCREMENT,
	"DataTypeParameterId" INT NOT NULL,
	"MemberId" INT NOT NULL,
	"Value" TEXT NOT NULL,
	CONSTRAINT "FK_DataTypeParameterValue_DataTypeParameter_DataTypeParameterId" FOREIGN KEY("DataTypeParameterId") REFERENCES "DataTypeParameters" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_DataTypeParameterValue_Member_MemberId" FOREIGN KEY("MemberId") REFERENCES "Members" ("Id") ON DELETE CASCADE
);

-- Objects
CREATE TABLE "Objects" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Object" PRIMARY KEY AUTOINCREMENT,
	"ClassId" INTEGER NOT NULL,
	CONSTRAINT "FK_Object_Class_ClassId" FOREIGN KEY("ClassId") REFERENCES "Classes" ("Id") ON DELETE CASCADE
);

-- Properties
CREATE TABLE "Properties" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Property" PRIMARY KEY AUTOINCREMENT,
	"ObjectId" INTEGER NOT NULL,
	"MemberId" INTEGER NOT NULL,
	"IntegerValue" INTEGER,
	"DecimalValue" REAL,
	"StringValueId" INTEGER,
	"DateTimeValue" TEXT,
	CONSTRAINT "FK_Property_Object_ObjectId" FOREIGN KEY("ObjectId") REFERENCES "Objects"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Property_Member_MemberId" FOREIGN KEY("MemberId") REFERENCES "Members"("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Property_Dictionary_StringValueId" FOREIGN KEY("StringValueId") REFERENCES "Dictionaries"("Id")
);

-- Relations
CREATE TABLE "Relations" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Relation" PRIMARY KEY AUTOINCREMENT,
	"MemberId" INTEGER NOT NULL,
	"PrimaryId" INTEGER NOT NULL,
	"ForeignId" INTEGER NOT NULL,
	CONSTRAINT "FK_Relation_Member_MemberId" FOREIGN KEY ("MemberId") REFERENCES "Members" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Relation_Object_PrimaryId" FOREIGN KEY ("PrimaryId") REFERENCES "Objects" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Relation_Object_ForeignId" FOREIGN KEY ("ForeignId") REFERENCES "Objects" ("Id") ON DELETE CASCADE
);

-- Menus
CREATE TABLE "Menus" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Menu" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"NameId" INTEGER NOT NULL,
	CONSTRAINT "FK_Menu_Dictionary_NameId" FOREIGN KEY ("NameId") REFERENCES "Dictionaries" ("Id")
);

-- MenuItems
CREATE TABLE "MenuItems" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_MenuItem" PRIMARY KEY AUTOINCREMENT,
	"MenuId" INTEGER,
	"MenuItemId" INTEGER,
	"NameId" INTEGER NOT NULL,
	"Url" TEXT,
	"Position" INTEGER,
	CONSTRAINT "FK_MenuItem_Menu_MenuId" FOREIGN KEY("MenuId") REFERENCES "Menus" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_MenuItem_MenuItem_MenuItemId" FOREIGN KEY("MenuItemId") REFERENCES "MenuItems" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_MenuItem_Dictionary_NameId" FOREIGN KEY("NameId") REFERENCES "Dictionaries" ("Id")
);

-- Forms
CREATE TABLE "Forms" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Form" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"NameId" INTEGER NOT NULL,
	"SubmitButtonTitleId" INTEGER NOT NULL,
	"ProduceCompletedForms" INTEGER NOT NULL,
	"FormHandlerCSharpClassName" TEXT NOT NULL,
	"FormHandlerParameters" TEXT,
	CONSTRAINT "FK_Form_Dictionary_NameId" FOREIGN KEY ("NameId") REFERENCES "Dictionaries" ("Id"),
	CONSTRAINT "FK_Form_Dictionary_SubmitButtonTitleId" FOREIGN KEY ("SubmitButtonTitleId") REFERENCES "Dictionaries" ("Id")
);

-- FieldTypes
CREATE TABLE "FieldTypes" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_FieldType" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"Name" TEXT NOT NULL,
	"Position" INTEGER,
  "ValidatorCSharpClassName" TEXT
);

-- Fields
CREATE TABLE "Fields" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Field" PRIMARY KEY AUTOINCREMENT,
	"FormId" INTEGER NOT NULL,
	"FieldTypeId" INTEGER NOT NULL,
	"Code" TEXT NOT NULL,
	"NameId" INTEGER NOT NULL,
	"IsRequired" INTEGER NOT NULL,
	"MaxLength" INTEGER,
	"Position" INTEGER,
	CONSTRAINT "FK_Field_Form_FormId" FOREIGN KEY ("FormId") REFERENCES "Forms" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Field_FieldType_FieldTypeId" FOREIGN KEY ("FieldTypeId") REFERENCES "FieldTypes" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Field_Dictionary_NameId" FOREIGN KEY ("NameId") REFERENCES "Dictionaries" ("Id")
);

-- FieldOptions
CREATE TABLE "FieldOptions" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_FieldOption" PRIMARY KEY AUTOINCREMENT,
	"FieldId" INTEGER NOT NULL,
	"ValueId" INTEGER NOT NULL,
	"Position" INTEGER,
	CONSTRAINT "FK_FieldOption_Field_FieldId" FOREIGN KEY ("FieldId") REFERENCES "Fields" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_FieldOption_Dictionary_ValueId" FOREIGN KEY ("ValueId") REFERENCES "Dictionaries" ("Id")
);

-- CompletedForms
CREATE TABLE "CompletedForms" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_CompletedForm" PRIMARY KEY AUTOINCREMENT,
	"FormId" INTEGER NOT NULL,
	"Created" TEXT NOT NULL,
	CONSTRAINT "FK_CompletedForm_Form_FormId" FOREIGN KEY ("FormId") REFERENCES "Forms" ("Id") ON DELETE CASCADE
);

-- CompletedFields
CREATE TABLE "CompletedFields" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_CompletedField" PRIMARY KEY AUTOINCREMENT,
	"CompletedFormId" INTEGER NOT NULL,
	"FieldId" INTEGER NOT NULL,
	"Value" TEXT,
	CONSTRAINT "FK_CompletedField_CompletedForm_CompletedFormId" FOREIGN KEY ("CompletedFormId") REFERENCES "CompletedForms" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_CompletedField_Field_FieldId" FOREIGN KEY ("FieldId") REFERENCES "Fields" ("Id") ON DELETE CASCADE
);

-- Files
CREATE TABLE "Files" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_File" PRIMARY KEY AUTOINCREMENT,
	"Name" TEXT NOT NULL,
	"Size" INTEGER NOT NULL
);

--
-- Extension: Platformus.ECommerce
-- Version: 2.6.0
--

-- Categories
CREATE TABLE "Categories" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Category" PRIMARY KEY AUTOINCREMENT,
	"CategoryId" INTEGER,
  "Url" TEXT,
	"NameId" INTEGER NOT NULL,
  "DescriptionId" INTEGER NOT NULL,
  "Position" INTEGER,
  "TitleId" INTEGER NOT NULL,
	"MetaDescriptionId" INTEGER NOT NULL,
	"MetaKeywordsId" INTEGER NOT NULL,
  "ProductProviderCSharpClassName" TEXT NOT NULL,
  "ProductProviderParameters" TEXT,
	CONSTRAINT "FK_Category_Category_CategoryId" FOREIGN KEY("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Category_Dictionary_NameId" FOREIGN KEY("NameId") REFERENCES "Dictionaries" ("Id"),
  CONSTRAINT "FK_Category_Dictionary_DescriptionId" FOREIGN KEY("DescriptionId") REFERENCES "Dictionaries" ("Id"),
  CONSTRAINT "FK_Category_Dictionary_TitleId" FOREIGN KEY("TitleId") REFERENCES "Dictionaries" ("Id"),
	CONSTRAINT "FK_Category_Dictionary_MetaDescriptionId" FOREIGN KEY("MetaDescriptionId") REFERENCES "Dictionaries" ("Id"),
	CONSTRAINT "FK_Category_Dictionary_MetaKeywordsId" FOREIGN KEY("MetaKeywordsId") REFERENCES "Dictionaries" ("Id")
);

-- Products
CREATE TABLE "Products" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Product" PRIMARY KEY AUTOINCREMENT,
	"CategoryId" INTEGER NOT NULL,
	"Url" TEXT,
	"Code" TEXT NOT NULL,
	"NameId" INTEGER NOT NULL,
	"DescriptionId" INTEGER NOT NULL,
  "UnitsId" INTEGER NOT NULL,
	"Price" REAL NOT NULL,
	"TitleId" INTEGER NOT NULL,
	"MetaDescriptionId" INTEGER NOT NULL,
	"MetaKeywordsId" INTEGER NOT NULL,
	CONSTRAINT "FK_Product_Category_CategoryId" FOREIGN KEY("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Product_Dictionary_NameId" FOREIGN KEY("NameId") REFERENCES "Dictionaries" ("Id"),
	CONSTRAINT "FK_Product_Dictionary_DescriptionId" FOREIGN KEY("DescriptionId") REFERENCES "Dictionaries" ("Id"),
  CONSTRAINT "FK_Product_Dictionary_UnitsId" FOREIGN KEY("UnitsId") REFERENCES "Dictionaries" ("Id"),
	CONSTRAINT "FK_Product_Dictionary_TitleId" FOREIGN KEY("TitleId") REFERENCES "Dictionaries" ("Id"),
	CONSTRAINT "FK_Product_Dictionary_MetaDescriptionId" FOREIGN KEY("MetaDescriptionId") REFERENCES "Dictionaries" ("Id"),
	CONSTRAINT "FK_Product_Dictionary_MetaKeywordsId" FOREIGN KEY("MetaKeywordsId") REFERENCES "Dictionaries" ("Id")
);

-- Photos
CREATE TABLE "Photos" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Photo" PRIMARY KEY AUTOINCREMENT,
	"ProductId" INTEGER NOT NULL,
	"Filename" TEXT NOT NULL,
	"IsCover" INTEGER NOT NULL,
	"Position" INTEGER,
	CONSTRAINT "FK_Photo_Product_ProductId" FOREIGN KEY("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
);

-- OrderStates
CREATE TABLE "OrderStates" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_OrderState" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"NameId" INTEGER NOT NULL,
	"Position" INTEGER,
	CONSTRAINT "FK_OrderState_Dictionary_NameId" FOREIGN KEY("NameId") REFERENCES "Dictionaries" ("Id")
);

-- PaymentMethods
CREATE TABLE "PaymentMethods" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_PaymentMethod" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"NameId" INTEGER NOT NULL,
	"Position" INTEGER,
	CONSTRAINT "FK_PaymentMethod_Dictionary_NameId" FOREIGN KEY("NameId") REFERENCES "Dictionaries" ("Id")
);

-- DeliveryMethods
CREATE TABLE "DeliveryMethods" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_DeliveryMethod" PRIMARY KEY AUTOINCREMENT,
	"Code" TEXT NOT NULL,
	"NameId" INTEGER NOT NULL,
	"Position" INTEGER,
	CONSTRAINT "FK_DeliveryMethod_Dictionary_NameId" FOREIGN KEY("NameId") REFERENCES "Dictionaries" ("Id")
);

-- Carts
CREATE TABLE "Carts" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Cart" PRIMARY KEY AUTOINCREMENT,
	"ClientSideId" TEXT NOT NULL,
	"Created" TEXT NOT NULL
);

-- Orders
CREATE TABLE "Orders" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Order" PRIMARY KEY AUTOINCREMENT,
	"OrderStateId" INTEGER NOT NULL,
	"PaymentMethodId" INTEGER NOT NULL,
	"DeliveryMethodId" INTEGER NOT NULL,
	"CustomerFirstName" TEXT NOT NULL,
	"CustomerLastName" TEXT,
	"CustomerPhone" TEXT NOT NULL,
	"CustomerEmail" TEXT,
	"CustomerAddress" TEXT,
	"Note" TEXT,
	"Created" TEXT NOT NULL,
	CONSTRAINT "FK_Order_OrderState_OrderStateId" FOREIGN KEY("OrderStateId") REFERENCES "OrderStates" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Order_PaymentMethod_PaymentMethodId" FOREIGN KEY("PaymentMethodId") REFERENCES "PaymentMethods" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Order_DeliveryMethod_DeliveryMethodId" FOREIGN KEY("DeliveryMethodId") REFERENCES "DeliveryMethods" ("Id") ON DELETE CASCADE
);

-- Positions
CREATE TABLE "Positions" (
	"Id" INTEGER NOT NULL CONSTRAINT "PK_Position" PRIMARY KEY AUTOINCREMENT,
	"CartId" INTEGER,
  "OrderId" INTEGER,
	"ProductId" INTEGER NOT NULL,
  "Price" REAL NOT NULL,
  "Quantity" REAL NOT NULL,
  "Subtotal" REAL NOT NULL,
	CONSTRAINT "FK_Position_Cart_CartId" FOREIGN KEY("CartId") REFERENCES "Carts" ("Id") ON DELETE CASCADE,
  CONSTRAINT "FK_Position_Order_OrderId" FOREIGN KEY("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE,
	CONSTRAINT "FK_Position_Product_ProductId" FOREIGN KEY("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
);

COMMIT;