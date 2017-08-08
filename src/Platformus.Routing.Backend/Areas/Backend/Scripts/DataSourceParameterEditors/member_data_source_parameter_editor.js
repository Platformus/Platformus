// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.member = {};
  platformus.dataSourceParameterEditors.member.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
    syncMemberNames(dataSourceParameter);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("member-data-source-parameter-editor").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: dataSourceParameter.name }).appendTo(field);
    createInput(dataSourceParameter).appendTo(field);
    createNames(dataSourceParameter).appendTo(field);
    createButtons(dataSourceParameter).appendTo(field);
    return field;
  }

  function createInput(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;
    var input = $("<input>")
      .attr("id", identity)
      .attr("type", "hidden")
      .attr("value", platformus.dataSourceParameterEditors.base.getValue(dataSourceParameter))
      .attr("data-data-source-parameter-code", dataSourceParameter.code)
      .change(
        function () {
          syncMemberNames(dataSourceParameter);
          platformus.dataSourceParameterEditors.base.changed();
        }
      );

    if (dataSourceParameter.isRequired) {
      input.attr("data-val", true).attr("data-val-required", true);
    }

    return input;
  }

  function createNames(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;
    var names = $("<div>").addClass("member-data-source-parameter-editor__names").attr("id", identity + "Names");

    if (dataSourceParameter.isRequired) {
      names.addClass("member-data-source-parameter-editor__names--required");
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

  function syncMemberNames(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;

    $.get(
      "/backend/domain/getmembername?memberid=" + $("#" + identity).val(),
      function (result) {
        $("#" + identity + "Names").html(result);
      }
    );
  }
})(window.platformus = window.platformus || {});