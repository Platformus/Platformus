// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.editors = platformus.editors || [];
  platformus.editors.singleLinePlainText = {};
  platformus.editors.singleLinePlainText.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.editors.base.createLabel(member).appendTo(field);

    if (member.isPropertyLocalizable) {
      field.addClass("field--multilingual")

      for (var i = 0; i < member.property.localizations.length; i++) {
        var localization = member.property.localizations[i];

        if (localization.culture.code != "__") {
          platformus.editors.base.createCulture(localization).appendTo(field);
          createTextBox(member, localization).appendTo(field);

          if (i != member.property.localizations.length - 1) {
            platformus.editors.base.createMultilingualSeparator().appendTo(field);
          }
        }
      }
    }

    else {
      for (var i = 0; i < member.property.localizations.length; i++) {
        var localization = member.property.localizations[i];

        if (localization.culture.code == "__") {
          createTextBox(member, localization).appendTo(field);
        }
      }
    }

    return field;
  }

  function createTextBox(member, localization) {
    var identity = platformus.editors.base.getIdentity(member, localization);
    var textBox = $("<input>").addClass("field__text-box");

    if (localization.culture.code != "__") {
      textBox.addClass("field__text-box--multilingual");
    }

    return textBox.addClass("text-box").attr("id", identity).attr("name", identity).attr("type", "text").attr("value", localization.value);
  }
})(window.platformus = window.platformus || {});