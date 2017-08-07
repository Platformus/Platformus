// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.checkbox = {};
  platformus.endpointParameterEditors.checkbox.create = function (container, endpointParameter) {
    createField(endpointParameter).appendTo(container);
  };

  function createField(endpointParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    createCheckbox(endpointParameter).appendTo(field);
    return field;
  }

  function createCheckbox(endpointParameter) {
    var checkbox = $("<a>")
      .addClass("checkbox")
      .attr("id", endpointParameter.code)
      .attr("href", "#")
      .change(platformus.endpointParameterEditors.base.endpointParameterChanged);

    createIndicator(endpointParameter).appendTo(checkbox);
    createLabel(endpointParameter).appendTo(checkbox);
    createInput(endpointParameter).appendTo(checkbox);
    return checkbox;
  }

  function createIndicator(endpointParameter) {
    var indicator = $("<div>").addClass("checkbox__indicator");

    if (platformus.endpointParameterEditors.base.endpointParameterValue(endpointParameter) == "true")
      indicator.addClass("checkbox__indicator--checked");

    return indicator;
  }

  function createLabel(endpointParameter) {
    return $("<div>").addClass("checkbox__label").html(endpointParameter.name);
  };

  function createInput(endpointParameter) {
    var value = platformus.endpointParameterEditors.base.endpointParameterValue(endpointParameter);

    if (platformus.string.isNullOrEmpty(value))
      value = "false";

    return $("<input>")
      .attr("type", "hidden")
      .attr("value", value)
      .attr("data-endpoint-parameter-code", endpointParameter.code);
  }
})(window.platformus = window.platformus || {});