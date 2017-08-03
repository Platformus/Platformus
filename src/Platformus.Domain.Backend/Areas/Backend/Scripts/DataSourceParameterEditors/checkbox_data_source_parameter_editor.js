// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.checkbox = {};
  platformus.dataSourceParameterEditors.checkbox.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    createCheckbox(dataSourceParameter).appendTo(field);
    return field;
  }

  function createCheckbox(dataSourceParameter) {
    var checkbox = $("<a>")
      .addClass("checkbox")
      .attr("id", dataSourceParameter.code)
      .attr("href", "#")
      .change(platformus.dataSourceParameterEditors.base.dataSourceParameterChanged);

    createIndicator(dataSourceParameter).appendTo(checkbox);
    createLabel(dataSourceParameter).appendTo(checkbox);
    createInput(dataSourceParameter).appendTo(checkbox);
    return checkbox;
  }

  function createIndicator(dataSourceParameter) {
    var indicator = $("<div>").addClass("checkbox__indicator");

    if (platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter) == "true")
      indicator.addClass("checkbox__indicator--checked");

    return indicator;
  }

  function createLabel(dataSourceParameter) {
    return $("<div>").addClass("checkbox__label").html(dataSourceParameter.name);
  };

  function createInput(dataSourceParameter) {
    var value = platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter);

    if (platformus.string.isNullOrEmpty(value))
      value = "false";

    return $("<input>")
      .attr("type", "hidden")
      .attr("value", value)
      .attr("data-datasource-parameter-code", dataSourceParameter.code);
  }
})(window.platformus = window.platformus || {});