// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.integerNumber = {};
  platformus.memberEditors.integerNumber.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);
    platformus.controls.numericTextBox.create(
      {
        identity: platformus.memberEditors.base.getIdentity(member),
        value: member.property.integerValue,
        validation: {
          isRequired: platformus.memberEditors.base.getIsRequiredDataTypeParameterValue(member),
          maxLength: platformus.memberEditors.base.getMaxLengthDataTypeParameterValue(member)
        }
      }
    ).appendTo(field);

    platformus.controls.numericTextBox.createNumericButtons().appendTo(field);
    return field;
  }
})(window.platformus = window.platformus || {});