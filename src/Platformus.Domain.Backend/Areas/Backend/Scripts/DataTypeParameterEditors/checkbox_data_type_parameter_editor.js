// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.checkbox = {};
  platformus.dataTypeParameterEditors.checkbox.create = function (container, dataTypeParameter) {
    createField(dataTypeParameter).appendTo(container);
  };

  function createField(dataTypeParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    createCheckbox(dataTypeParameter).appendTo(field);
    return field;
  }

  function createCheckbox(dataTypeParameter) {
    var identity = "dataTypeParameter" + dataTypeParameter.id;
    var checkbox = $("<a>")
      .addClass("checkbox")
      .attr("id", identity)
      .attr("href", "#")
      .change(platformus.dataTypeParameterEditors.base.dataTypeParameterChanged);

    createIndicator(dataTypeParameter).appendTo(checkbox);
    createLabel(dataTypeParameter).appendTo(checkbox);
    createInput(dataTypeParameter).appendTo(checkbox);
    return checkbox;
  }

  function createIndicator(dataTypeParameter) {
    var indicator = $("<div>").addClass("checkbox__indicator");

    if (dataTypeParameter.value == "true")
      indicator.addClass("checkbox__indicator--checked");

    return indicator;
  }

  function createLabel(dataTypeParameter) {
    return $("<div>").addClass("checkbox__label").html(dataTypeParameter.name);
  };

  function createInput(dataTypeParameter) {
    var identity = "dataTypeParameter" + dataTypeParameter.id;
    var value = dataTypeParameter.value;

    if (platformus.string.isNullOrEmpty(value))
      value = "false";

    return $("<input>")
      .attr("name", identity)
      .attr("type", "hidden")
      .attr("value", value);
  }
})(window.platformus = window.platformus || {});