// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.microcontrollerParameterEditors = platformus.microcontrollerParameterEditors || [];
  platformus.microcontrollerParameterEditors.text = {};
  platformus.microcontrollerParameterEditors.text.create = function (container, microcontrollerParameter) {
    createField(microcontrollerParameter).appendTo(container);
  };

  function createField(microcontrollerParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.microcontrollerParameterEditors.base.createLabel(microcontrollerParameter).appendTo(field);
    createTextBox(microcontrollerParameter).appendTo(field);
    return field;
  }

  function createTextBox(microcontrollerParameter) {
    var textBox = $("<input>")
      .addClass("field__text-box")
      .addClass("text-box")
      .attr("type", "text")
      .attr("value", platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter))
      .attr("data-microcontroller-parameter-code", microcontrollerParameter.code)
      .change(platformus.microcontrollerParameterEditors.base.microcontrollerParameterChanged);

    if (microcontrollerParameter.isRequired) {
      textBox.addClass("text-box--required").attr("data-val", true).attr("data-val-required", true);
    }

    return textBox;
  }
})(window.platformus = window.platformus || {});