// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.microcontrollerParameterEditors = platformus.microcontrollerParameterEditors || [];
  platformus.microcontrollerParameterEditors.numeric = {};
  platformus.microcontrollerParameterEditors.numeric.create = function (container, microcontrollerParameter) {
    createField(microcontrollerParameter).appendTo(container);
  };

  function createField(microcontrollerParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.microcontrollerParameterEditors.base.createLabel(microcontrollerParameter).appendTo(field);
    createTextBox(microcontrollerParameter).appendTo(field);
    createNumericButtons().appendTo(field);
    return field;
  }

  function createTextBox(microcontrollerParameter) {
    return $("<input>")
      .addClass("field__text-box")
      .addClass("field__text-box--numeric")
      .addClass("text-box")
      .attr("type", "text")
      .attr("value", platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter.code))
      .attr("data-microcontroller-parameter-code", microcontrollerParameter.code)
      .change(platformus.microcontrollerParameterEditors.base.microcontrollerParameterChanged);
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