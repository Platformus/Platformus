// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.singleLinePlainText = {};
  platformus.memberEditors.singleLinePlainText.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.memberEditors.base.createLabel(member).appendTo(field);

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
    var identity = platformus.memberEditors.base.getIdentity(member, localization);
    var textBox = $("<input>").addClass("field__text-box");

    if (localization.culture.code != "__") {
      textBox.addClass("field__text-box--multilingual");
    }

    textBox
      .addClass("text-box")
      .attr("id", identity)
      .attr("name", identity)
      .attr("type", "text")
      .attr("value", localization.value)
      .attr("data-culture", localization.culture.code);

    var isRequired = platformus.memberEditors.base.getIsRequiredDataTypeParameterValue(member);
    var maxLength = platformus.memberEditors.base.getMaxLengthDataTypeParameterValue(member);

    if (isRequired != null || maxLength != null) {
      textBox.attr("data-val", true);
    }

    if (isRequired != null) {
      textBox.addClass("text-box--required").attr("data-val-required", isRequired);
    }

    if (maxLength != null) {
      textBox.attr("maxlength", maxLength).attr("data-val-maxlength-max", maxLength);
    }

    return textBox;
  }
})(window.platformus = window.platformus || {});