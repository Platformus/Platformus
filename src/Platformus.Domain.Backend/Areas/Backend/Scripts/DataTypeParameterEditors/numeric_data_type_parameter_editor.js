// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.numeric = {};
  platformus.dataTypeParameterEditors.numeric.create = function (container, dataTypeParameter) {
    createField(dataTypeParameter).appendTo(container);
  };

  function createField(dataTypeParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.dataTypeParameterEditors.base.createLabel(dataTypeParameter).appendTo(field);
    createTextBox(dataTypeParameter).appendTo(field);
    createNumericButtons().appendTo(field);
    return field;
  }

  function createTextBox(dataTypeParameter) {
    var identity = "dataTypeParameter" + dataTypeParameter.id;

    return $("<input>")
      .addClass("field__text-box")
      .addClass("field__text-box--numeric")
      .addClass("text-box")
      .attr("id", identity)
      .attr("name", identity)
      .attr("type", "text")
      .attr("value", dataTypeParameter.value);
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