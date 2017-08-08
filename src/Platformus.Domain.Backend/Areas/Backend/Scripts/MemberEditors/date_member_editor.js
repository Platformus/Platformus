// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.date = {};
  platformus.memberEditors.date.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("date-editor").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);
    createTextBox(member).appendTo(field);
    return field;
  }

  function createTextBox(member) {
    return platformus.controls.textBox.create(
      {
        identity: platformus.memberEditors.base.getIdentity(member),
        value: member.property.dateTimeValue,
        validation: {
          isRequired: platformus.memberEditors.base.getIsRequiredDataTypeParameterValue(member),
          maxLength: platformus.memberEditors.base.getMaxLengthDataTypeParameterValue(member)
        }
      }
    ).attr("autocomplete", "off")
      .attr("placeholder", moment().locale(platformus.culture.server()).localeData().longDateFormat("L"))
      .attr("data-type", "date");
  }
})(window.platformus = window.platformus || {});