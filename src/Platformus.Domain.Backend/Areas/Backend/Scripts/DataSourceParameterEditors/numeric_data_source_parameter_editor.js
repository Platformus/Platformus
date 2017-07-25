// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.numeric = {};
  platformus.dataSourceParameterEditors.numeric.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.dataSourceParameterEditors.base.createLabel(dataSourceParameter).appendTo(field);
    createTextBox(dataSourceParameter).appendTo(field);
    createNumericButtons().appendTo(field);
    return field;
  }

  function createTextBox(dataSourceParameter) {
    return $("<input>")
      .addClass("field__text-box")
      .addClass("field__text-box--numeric")
      .addClass("text-box")
      .attr("type", "text")
      .attr("value", platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter.code))
      .attr("data-datasource-parameter-code", dataSourceParameter.code)
      .change(platformus.dataSourceParameterEditors.base.dataSourceParameterChanged);
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