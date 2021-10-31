// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.decimalNumber = {};
  platformus.memberEditors.decimalNumber.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);
    platformus.controls.textBox.create(
      {
        identity: platformus.memberEditors.base.getIdentity(member),
        value: member.property.decimalValue,
        validation: {
          isRequired: platformus.memberEditors.base.getIsRequiredDataTypeParameterValue(member),
          minValue: platformus.memberEditors.base.getMinValueDataTypeParameterValue(member),
          maxValue: platformus.memberEditors.base.getMaxValueDataTypeParameterValue(member)
        }
      }
    ).attr("data-type", "number")
      .appendTo(field);

    return field;
  }
})(window.platformus = window.platformus || {});