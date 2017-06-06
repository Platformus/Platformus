// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.temp = {};
  platformus.dataTypeParameterEditors.temp.create = function (container, dataTypeParameter) {
    createField(dataTypeParameter).appendTo(container);
  };

  function createField(dataTypeParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.dataTypeParameterEditors.base.createLabel(dataTypeParameter).appendTo(field);
    createTextBox(dataTypeParameter).appendTo(field);
    return field;
  }

  function createTextBox(dataTypeParameter) {
    var identity = "dataTypeParameter" + dataTypeParameter.id;
    var textBox = $("<input>").addClass("field__text-box");

    return textBox
      .addClass("text-box")
      .attr("id", identity)
      .attr("name", identity)
      .attr("type", "text")
      .attr("value", dataTypeParameter.value);
  }
})(window.platformus = window.platformus || {});