// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.numericTextBox = {};
  platformus.endpointParameterEditors.numericTextBox.create = function (container, endpointParameter) {
    createField(endpointParameter).appendTo(container);
  };

  function createField(endpointParameter) {
    var identity = platformus.endpointParameterEditors.base.getIdentity(endpointParameter);
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ identity: identity, text: endpointParameter.name }).appendTo(field);
    platformus.controls.numericTextBox.create(
      {
        identity: identity,
        value: platformus.endpointParameterEditors.base.getValue(endpointParameter),
        validation: { isRequired: endpointParameter.isRequired }
      }
    ).attr("data-endpoint-parameter-code", endpointParameter.code).appendTo(field)
      .change(platformus.endpointParameterEditors.base.changed)
      .appendTo(field);

    platformus.controls.numericTextBox.createNumericButtons().appendTo(field);
    return field;
  }
})(window.platformus = window.platformus || {});