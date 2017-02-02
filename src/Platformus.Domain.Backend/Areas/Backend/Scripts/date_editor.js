// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.editors = platformus.editors || [];
  platformus.editors.date = {};
  platformus.editors.date.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("date-editor").addClass("form__field").addClass("field");

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
    var culture = null;

    if (localization.culture.code != "__") {
      textBox.addClass("field__text-box--multilingual");
      culture = localization.culture.code;
    }

    else {
      culture = platformus.culture.server();
    }

    return textBox
      .addClass("text-box")
      .attr("id", identity)
      .attr("name", identity)
      .attr("type", "text")
      .attr("autocomplete", "off")
      .attr("placeholder", moment().locale(culture).localeData().longDateFormat("L"))
      .attr("value", localization.value)
      .attr("data-culture", localization.culture.code)
      .attr("data-type", "date");
  }
})(window.platformus = window.platformus || {});