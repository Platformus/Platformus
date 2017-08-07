// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.numeric = {};
  platformus.endpointParameterEditors.numeric.create = function (container, endpointParameter) {
    createField(endpointParameter).appendTo(container);
  };

  function createField(endpointParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.endpointParameterEditors.base.createLabel(endpointParameter).appendTo(field);
    createTextBox(endpointParameter).appendTo(field);
    createNumericButtons().appendTo(field);
    return field;
  }

  function createTextBox(endpointParameter) {
    var textBox = $("<input>")
      .addClass("field__text-box")
      .addClass("field__text-box--numeric")
      .addClass("text-box")
      .attr("type", "text")
      .attr("value", platformus.endpointParameterEditors.base.endpointParameterValue(endpointParameter))
      .attr("data-endpoint-parameter-code", endpointParameter.code)
      .change(platformus.endpointParameterEditors.base.endpointParameterChanged);

    if (endpointParameter.isRequired) {
      textBox.addClass("text-box--required").attr("data-val", true).attr("data-val-required", true);
    }

    return textBox;
  }

  function createNumericButtons() {
    var buttons = $("<div>").addClass("field__numeric-buttons");

    createNumericUpButton().appendTo(buttons);
    createNumericDownButton().appendTo(buttons);
    return buttons;
  }

  function createNumericUpButton() {
    return $("<a>").addClass("field__numeric-button").addClass("field__numeric-button--up").attr("href", "#");
  }

  function createNumericDownButton() {
    return $("<a>").addClass("field__numeric-button").addClass("field__numeric-button--down").attr("href", "#");
  }
})(window.platformus = window.platformus || {});