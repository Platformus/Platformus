// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.editors = platformus.editors || [];
  platformus.editors.base = {};
  platformus.editors.base.createLabel = function (member) {
    return $("<label>").addClass("field__label").addClass("label").html(member.name);
  };

  platformus.editors.base.createCulture = function (localization) {
    var culture = $("<div>").addClass("field__culture");

    platformus.editors.base.createFlag(localization).appendTo(culture);
    return culture;
  };

  platformus.editors.base.createFlag = function (localization) {
    return $("<div>").addClass("field__culture-flag").html(localization.culture.code);
  };

  platformus.editors.base.createMultilingualSeparator = function () {
    return $("<div>").addClass("field__multilingual-separator");
  };

  platformus.editors.base.getIdentity = function (member, localization) {
    return "propertyMember" + member.id + (localization == null ? platformus.string.empty : localization.culture.code);
  };
})(window.platformus = window.platformus || {});