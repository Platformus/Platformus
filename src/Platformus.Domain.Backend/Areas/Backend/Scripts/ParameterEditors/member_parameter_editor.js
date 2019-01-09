// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.member = {};
  platformus.parameterEditors.member.create = function (container, parameter) {
    createField(parameter).appendTo(container);
    syncMemberNames(parameter);
  };

  function createField(parameter) {
    var field = $("<div>").addClass("member-parameter-editor").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: parameter.name }).appendTo(field);
    createInput(parameter).appendTo(field);
    createNames(parameter).appendTo(field);
    createButtons(parameter).appendTo(field);
    return field;
  }

  function createInput(parameter) {
    var identity = "parameter" + parameter.code;
    var input = $("<input>")
      .attr("id", identity)
      .attr("type", "hidden")
      .attr("value", platformus.parameterEditors.base.getValue(parameter))
      .attr("data-parameter-code", parameter.code)
      .change(
        function () {
          syncMemberNames(parameter);
          platformus.parameterEditors.base.changed();
        }
      );

    if (parameter.isRequired) {
      input.attr("data-val", true).attr("data-val-required", true);
    }

    return input;
  }

  function createNames(parameter) {
    var identity = "parameter" + parameter.code;
    var names = $("<div>").addClass("member-parameter-editor__names").attr("id", identity + "Names");

    if (parameter.isRequired) {
      names.addClass("member-parameter-editor__names--required");
    }

    return names;
  }

  function createButtons(parameter) {
    var buttons = $("<div>").addClass("form__buttons").addClass("form__buttons--minor").addClass("buttons");

    createButton(parameter).appendTo(buttons);
    return buttons;
  }

  function createButton(parameter) {
    var identity = "parameter" + parameter.code;

    return $("<button>").addClass("buttons__button").addClass("buttons__button--minor").addClass("button").addClass("button--positive").addClass("button--minor").attr("type", "button").html("Select…").click(
      function () {
        platformus.forms.memberSelectorForm.show(
          $("#" + identity).val(),
          function (memberId) {
            $("#" + identity).val(memberId);
            $("#" + identity).trigger("change");
          }
        );
      }
    );
  }

  function syncMemberNames(parameter) {
    var identity = "parameter" + parameter.code;

    $.get(
      "/backend/domain/getmembername?memberid=" + $("#" + identity).val(),
      function (result) {
        $("#" + identity + "Names").html(result);
      }
    );
  }
})(window.platformus = window.platformus || {});