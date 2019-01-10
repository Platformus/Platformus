// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.baseItemSelector = {};
  platformus.parameterEditors.baseItemSelector.create = function (container, parameter, form, keysUrl) {
    createField(parameter, form, keysUrl).appendTo(container);
    syncKeys(parameter, keysUrl);
  };

  function createField(parameter, form, keysUrl) {
    var field = $("<div>").addClass("item-selector").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: parameter.name }).appendTo(field);
    createInput(parameter, keysUrl).appendTo(field);
    createKeys(parameter).appendTo(field);
    createButtons(parameter, form).appendTo(field);
    return field;
  }

  function createInput(parameter, keysUrl) {
    var identity = "parameter" + parameter.code;
    var input = $("<input>")
      .attr("id", identity)
      .attr("type", "hidden")
      .attr("value", platformus.parameterEditors.base.getValue(parameter))
      .attr("data-parameter-code", parameter.code)
      .change(
      function () {
          syncKeys(parameter, keysUrl);
          platformus.parameterEditors.base.changed();
        }
      );

    if (parameter.isRequired) {
      input.attr("data-val", true).attr("data-val-required", true);
    }

    return input;
  }

  function createKeys(parameter) {
    var identity = "parameter" + parameter.code;
    var keys = $("<div>").addClass("item-selector__keys").attr("id", identity + "Keys");

    if (parameter.isRequired) {
      keys.addClass("item-selector__keys--required");
    }

    return keys;
  }

  function createButtons(parameter, form) {
    var buttons = $("<div>").addClass("form__buttons").addClass("form__buttons--minor").addClass("buttons");

    createButton(parameter, form).appendTo(buttons);
    return buttons;
  }

  function createButton(parameter, form) {
    var identity = "parameter" + parameter.code;

    return $("<button>").addClass("buttons__button").addClass("buttons__button--minor").addClass("button").addClass("button--positive").addClass("button--minor").attr("type", "button").html("Select…").click(
      function () {
        form.show(
          $("#" + identity).val(),
          function (key) {
            $("#" + identity).val(key);
            $("#" + identity).trigger("change");
          }
        );
      }
    );
  }

  function syncKeys(parameter, keysUrl) {
    var identity = "parameter" + parameter.code;

    $.get(
      keysUrl + $("#" + identity).val(),
      function (result) {
        $("#" + identity + "Keys").html(result);
      }
    );
  }
})(window.platformus = window.platformus || {});