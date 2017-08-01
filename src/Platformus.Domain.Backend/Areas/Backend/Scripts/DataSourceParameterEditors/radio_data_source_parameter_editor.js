// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.radio = {};
  platformus.dataSourceParameterEditors.radio.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.dataSourceParameterEditors.base.createLabel(dataSourceParameter).appendTo(field);
    createRadioButtonList(dataSourceParameter).appendTo(field);
    return field;
  }

  function createRadioButtonList(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;
    var radioButtonList = $("<div>").addClass("radio-button-list field__radio-button-list").attr("id", identity).change(platformus.dataSourceParameterEditors.base.dataSourceParameterChanged);

    for (var i = 0; i != dataSourceParameter.options.length; i++) {
      createRadioButton(dataSourceParameter, dataSourceParameter.options[i]).appendTo(radioButtonList);
    }

    createInput(dataSourceParameter).appendTo(radioButtonList);
    return radioButtonList;
  }

  function createRadioButton(dataSourceParameter, option) {
    var radioButton = $("<a>").addClass("radio-button-list__radio-button radio-button").attr("href", "#").attr("data-value", option.value);

    createRadioButtonIndicator(dataSourceParameter, option).appendTo(radioButton);
    createRadioButtonLabel(dataSourceParameter, option).appendTo(radioButton);
    return radioButton;
  }

  function createRadioButtonIndicator(dataSourceParameter, option) {
    var radioButtonIndicator = $("<div>").addClass("radio-button__indicator");

    if (option.value == platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter.code)) {
      radioButtonIndicator.addClass("radio-button__indicator--checked");
    }

    return radioButtonIndicator;
  }

  function createRadioButtonLabel(dataSourceParameter, option) {
    return $("<div>").addClass("radio-button__label").html(option.text);
  }

  function createInput(dataSourceParameter) {
    return $("<input>")
      .attr("type", "hidden")
      .attr("value", platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter.code))
      .attr("data-datasource-parameter-code", dataSourceParameter.code)
  }
})(window.platformus = window.platformus || {});