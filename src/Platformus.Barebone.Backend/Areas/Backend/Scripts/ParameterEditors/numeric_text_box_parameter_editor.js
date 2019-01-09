// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.numericTextBox = {};
  platformus.parameterEditors.numericTextBox.create = function (container, parameter) {
    createField(parameter).appendTo(container);
  };

  function createField(parameter) {
    var identity = platformus.parameterEditors.base.getIdentity(parameter);
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ identity: identity, text: parameter.name }).appendTo(field);
    platformus.controls.numericTextBox.create(
      {
        identity: identity,
        value: platformus.parameterEditors.base.getValue(parameter),
        validation: { isRequired: parameter.isRequired }
      }
    ).attr("data-parameter-code", parameter.code)
      .change(platformus.parameterEditors.base.changed)
      .appendTo(field);

    platformus.controls.numericTextBox.createNumericButtons().appendTo(field);
    return field;
  }
})(window.platformus = window.platformus || {});