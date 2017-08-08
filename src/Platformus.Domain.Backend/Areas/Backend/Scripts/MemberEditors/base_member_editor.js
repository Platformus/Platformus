// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.base = {};
  platformus.memberEditors.base.getDataTypeParameterValue = function (member, code, defaultValue) {
    for (var i = 0; i < member.propertyDataType.dataTypeParameters.length; i++) {
      if (member.propertyDataType.dataTypeParameters[i].code == code) {
        var value = member.propertyDataType.dataTypeParameters[i].value;

        if (platformus.string.isNullOrEmpty(value))
          return defaultValue;

        return value;
      }
    }

    return defaultValue;
  };

  platformus.memberEditors.base.getIsRequiredDataTypeParameterValue = function (member) {
    var value = platformus.memberEditors.base.getDataTypeParameterValue(member, "IsRequired", null);

    if (value == null)
      return null;

    return value == "true";
  };

  platformus.memberEditors.base.getMaxLengthDataTypeParameterValue = function (member) {
    var value = platformus.memberEditors.base.getDataTypeParameterValue(member, "MaxLength", null);

    if (value == null)
      return null;

    return parseInt(value);
  };

  platformus.memberEditors.base.createCulture = function (localization) {
    var culture = $("<div>").addClass("field__culture");

    platformus.memberEditors.base.createFlag(localization).appendTo(culture);
    return culture;
  };

  platformus.memberEditors.base.createFlag = function (localization) {
    return $("<div>").addClass("field__culture-flag").html(localization.culture.code);
  };

  platformus.memberEditors.base.createMultilingualSeparator = function () {
    return $("<div>").addClass("field__multilingual-separator");
  };

  platformus.memberEditors.base.getIdentity = function (member, localization) {
    return "propertyMember" + member.id + (localization == null ? "__" : localization.culture.code);
  };
})(window.platformus = window.platformus || {});