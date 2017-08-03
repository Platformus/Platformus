// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.class = {};
  platformus.dataSourceParameterEditors.class.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
    syncClassNames(dataSourceParameter);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("class-data-source-parameter-editor").addClass("form__field").addClass("field");

    platformus.dataSourceParameterEditors.base.createLabel(dataSourceParameter).appendTo(field);
    createInput(dataSourceParameter).appendTo(field);
    createNames(dataSourceParameter).appendTo(field);
    createButtons(dataSourceParameter).appendTo(field);
    return field;
  }

  function createInput(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;

    return $("<input>")
      .attr("id", identity)
      .attr("type", "hidden")
      .attr("value", platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter))
      .attr("data-datasource-parameter-code", dataSourceParameter.code)
      .change(
        function () {
          syncClassNames(dataSourceParameter);
          platformus.dataSourceParameterEditors.base.dataSourceParameterChanged();
        }
      );
  }

  function createNames(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;
    var names = $("<div>").addClass("class-data-source-parameter-editor__names").attr("id", identity + "Names");

    if (dataSourceParameter.isRequired) {
      names.addClass("class-data-source-parameter-editor__names--required");
    }

    return names;
  }

  function createButtons(dataSourceParameter) {
    var buttons = $("<div>").addClass("form__buttons").addClass("form__buttons--minor").addClass("buttons");

    createButton(dataSourceParameter).appendTo(buttons);
    return buttons;
  }

  function createButton(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;

    return $("<button>").addClass("buttons__button").addClass("buttons__button--minor").addClass("button").addClass("button--positive").addClass("button--minor").attr("type", "button").html("Select…").click(
      function () {
        platformus.forms.classSelectorForm.show(
          $("#" + identity).val(),
          function (classId) {
            $("#" + identity).val(classId);
            $("#" + identity).trigger("change");
          }
        );
      }
    );
  }

  function syncClassNames(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;

    $.get(
      "/backend/domain/getclassname?classid=" + $("#" + identity).val(),
      function (result) {
        $("#" + identity + "Names").html(result);
      }
    );
  }
})(window.platformus = window.platformus || {});