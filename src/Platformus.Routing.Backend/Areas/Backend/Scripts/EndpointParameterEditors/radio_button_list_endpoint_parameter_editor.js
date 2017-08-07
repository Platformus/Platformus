// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.radioButtonList = {};
  platformus.endpointParameterEditors.radioButtonList.create = function (container, endpointParameter) {
    createField(endpointParameter).appendTo(container);
  };

  function createField(endpointParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.endpointParameterEditors.base.createLabel(endpointParameter).appendTo(field);
    createRadioButtonList(endpointParameter).appendTo(field);
    return field;
  }

  function createRadioButtonList(endpointParameter) {
    var identity = "endpointParameter" + endpointParameter.code;
    var radioButtonList = $("<div>").addClass("radio-button-list field__radio-button-list").attr("id", identity).change(platformus.endpointParameterEditors.base.endpointParameterChanged);

    for (var i = 0; i != endpointParameter.options.length; i++) {
      createRadioButton(endpointParameter, endpointParameter.options[i]).appendTo(radioButtonList);
    }

    createInput(endpointParameter).appendTo(radioButtonList);
    return radioButtonList;
  }

  function createRadioButton(endpointParameter, option) {
    var radioButton = $("<a>").addClass("radio-button-list__radio-button radio-button").attr("href", "#").attr("data-value", option.value);

    createRadioButtonIndicator(endpointParameter, option).appendTo(radioButton);
    createRadioButtonLabel(endpointParameter, option).appendTo(radioButton);
    return radioButton;
  }

  function createRadioButtonIndicator(endpointParameter, option) {
    var radioButtonIndicator = $("<div>").addClass("radio-button__indicator");

    if (option.value == platformus.endpointParameterEditors.base.endpointParameterValue(endpointParameter)) {
      radioButtonIndicator.addClass("radio-button__indicator--checked");
    }

    return radioButtonIndicator;
  }

  function createRadioButtonLabel(endpointParameter, option) {
    return $("<div>").addClass("radio-button__label").html(option.text);
  }

  function createInput(endpointParameter) {
    return $("<input>")
      .attr("type", "hidden")
      .attr("value", platformus.endpointParameterEditors.base.endpointParameterValue(endpointParameter))
      .attr("data-endpoint-parameter-code", endpointParameter.code)
  }
})(window.platformus = window.platformus || {});