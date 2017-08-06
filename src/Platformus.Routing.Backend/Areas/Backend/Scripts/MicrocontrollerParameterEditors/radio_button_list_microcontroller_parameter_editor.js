// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.microcontrollerParameterEditors = platformus.microcontrollerParameterEditors || [];
  platformus.microcontrollerParameterEditors.radioButtonList = {};
  platformus.microcontrollerParameterEditors.radioButtonList.create = function (container, microcontrollerParameter) {
    createField(microcontrollerParameter).appendTo(container);
  };

  function createField(microcontrollerParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.microcontrollerParameterEditors.base.createLabel(microcontrollerParameter).appendTo(field);
    createRadioButtonList(microcontrollerParameter).appendTo(field);
    return field;
  }

  function createRadioButtonList(microcontrollerParameter) {
    var identity = "microcontrollerParameter" + microcontrollerParameter.code;
    var radioButtonList = $("<div>").addClass("radio-button-list field__radio-button-list").attr("id", identity).change(platformus.microcontrollerParameterEditors.base.microcontrollerParameterChanged);

    for (var i = 0; i != microcontrollerParameter.options.length; i++) {
      createRadioButton(microcontrollerParameter, microcontrollerParameter.options[i]).appendTo(radioButtonList);
    }

    createInput(microcontrollerParameter).appendTo(radioButtonList);
    return radioButtonList;
  }

  function createRadioButton(microcontrollerParameter, option) {
    var radioButton = $("<a>").addClass("radio-button-list__radio-button radio-button").attr("href", "#").attr("data-value", option.value);

    createRadioButtonIndicator(microcontrollerParameter, option).appendTo(radioButton);
    createRadioButtonLabel(microcontrollerParameter, option).appendTo(radioButton);
    return radioButton;
  }

  function createRadioButtonIndicator(microcontrollerParameter, option) {
    var radioButtonIndicator = $("<div>").addClass("radio-button__indicator");

    if (option.value == platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter)) {
      radioButtonIndicator.addClass("radio-button__indicator--checked");
    }

    return radioButtonIndicator;
  }

  function createRadioButtonLabel(microcontrollerParameter, option) {
    return $("<div>").addClass("radio-button__label").html(option.text);
  }

  function createInput(microcontrollerParameter) {
    return $("<input>")
      .attr("type", "hidden")
      .attr("value", platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter))
      .attr("data-microcontroller-parameter-code", microcontrollerParameter.code)
  }
})(window.platformus = window.platformus || {});