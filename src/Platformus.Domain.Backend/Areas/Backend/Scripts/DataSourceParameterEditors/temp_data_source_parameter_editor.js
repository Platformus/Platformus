// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.temp = {};
  platformus.dataSourceParameterEditors.temp.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.dataSourceParameterEditors.base.createLabel(dataSourceParameter).appendTo(field);
    createTextBox(dataSourceParameter).appendTo(field);
    return field;
  }

  function createTextBox(dataSourceParameter) {
    var identity = dataSourceParameter.code;
    var textBox = $("<input>").addClass("field__text-box");

    return textBox
      .addClass("text-box")
      .attr("type", "text")
      .attr("value", platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter.code))
      .attr("data-datasource-parameter-code", dataSourceParameter.code)
      .change(platformus.dataSourceParameterEditors.base.dataSourceParameterChanged);
  }
})(window.platformus = window.platformus || {});