﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.multilinePlainText = {};
  platformus.memberEditors.multilinePlainText.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);

    if (member.isPropertyLocalizable) {
      field.addClass("field--multilingual")

      for (var i = 0; i < member.property.stringValue.localizations.length; i++) {
        var localization = member.property.stringValue.localizations[i];

        if (localization.culture.id != "__") {
          platformus.memberEditors.base.createCulture(localization, true).appendTo(field);
          createTextArea(member, localization).appendTo(field);

          if (i != member.property.stringValue.localizations.length - 1) {
            platformus.memberEditors.base.createMultilingualSeparator().appendTo(field);
          }
        }
      }
    }

    else {
      member.property.stringValue.localizations.forEach(function (localization) {
        if (localization.culture.id == "__") {
          createTextArea(member, localization).appendTo(field);
        }
      });
    }

    return field;
  }

  function createTextArea(member, localization) {
    return platformus.controls.textArea.create(
      {
        identity: platformus.memberEditors.base.getIdentity(member, localization),
        value: localization.value,
        isMultilingual: localization.culture.id != "__",
        validation: {
          isRequired: platformus.memberEditors.base.getIsRequiredDataTypeParameterValue(member),
          maxLength: platformus.memberEditors.base.getMaxLengthDataTypeParameterValue(member)
        }
      }
    ).attr("lang", localization.culture.id);
  }
})(window.platformus = window.platformus || {});