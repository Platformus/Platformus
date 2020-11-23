﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.singleLinePlainText = {};
  platformus.memberEditors.singleLinePlainText.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);

    if (member.isPropertyLocalizable) {
      field.addClass("field--multilingual")

      for (var i = 0; i < member.property.stringValue.localizations.length; i++) {
        var localization = member.property.stringValue.localizations[i];

        if (localization.culture.code != "__") {
          platformus.memberEditors.base.createCulture(localization).appendTo(field);
          createTextBox(member, localization).appendTo(field);

          if (i != member.property.stringValue.localizations.length - 1) {
            platformus.memberEditors.base.createMultilingualSeparator().appendTo(field);
          }
        }
      }
    }

    else {
      for (var i = 0; i < member.property.stringValue.localizations.length; i++) {
        var localization = member.property.stringValue.localizations[i];

        if (localization.culture.code == "__") {
          createTextBox(member, localization).appendTo(field);
        }
      }
    }

    return field;
  }

  function createTextBox(member, localization) {
    return platformus.controls.textBox.create(
      {
        identity: platformus.memberEditors.base.getIdentity(member, localization),
        value: localization.value,
        isMultilingual: localization.culture.code != "__",
        validation: {
          isRequired: platformus.memberEditors.base.getIsRequiredDataTypeParameterValue(member),
          maxLength: platformus.memberEditors.base.getMaxLengthDataTypeParameterValue(member)
        }
      }
    ).attr("data-culture", localization.culture.code);
  }
})(window.platformus = window.platformus || {});