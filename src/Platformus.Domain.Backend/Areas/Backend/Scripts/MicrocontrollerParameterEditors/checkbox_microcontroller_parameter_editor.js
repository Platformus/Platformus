// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.microcontrollerParameterEditors = platformus.microcontrollerParameterEditors || [];
  platformus.microcontrollerParameterEditors.checkbox = {};
  platformus.microcontrollerParameterEditors.checkbox.create = function (container, microcontrollerParameter) {
    createField(microcontrollerParameter).appendTo(container);
  };

  function createField(microcontrollerParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    createCheckbox(microcontrollerParameter).appendTo(field);
    return field;
  }

  function createCheckbox(microcontrollerParameter) {
    var checkbox = $("<a>")
      .addClass("checkbox")
      .attr("id", microcontrollerParameter.code)
      .attr("href", "#")
      .change(platformus.microcontrollerParameterEditors.base.microcontrollerParameterChanged);

    createIndicator(microcontrollerParameter).appendTo(checkbox);
    createLabel(microcontrollerParameter).appendTo(checkbox);
    createInput(microcontrollerParameter).appendTo(checkbox);
    return checkbox;
  }

  function createIndicator(microcontrollerParameter) {
    var indicator = $("<div>").addClass("checkbox__indicator");

    if (platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter) == "true")
      indicator.addClass("checkbox__indicator--checked");

    return indicator;
  }

  function createLabel(microcontrollerParameter) {
    return $("<div>").addClass("checkbox__label").html(microcontrollerParameter.name);
  };

  function createInput(microcontrollerParameter) {
    var value = platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter);

    if (platformus.string.isNullOrEmpty(value))
      value = "false";

    return $("<input>")
      .attr("type", "hidden")
      .attr("value", value)
      .attr("data-microcontroller-parameter-code", microcontrollerParameter.code);
  }
})(window.platformus = window.platformus || {});