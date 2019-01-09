// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.relation = {};
  platformus.memberEditors.relation.create = function (container, member) {
    if (member.isRelationSingleParent) {
      return;
    }

    createField(member).appendTo(container);
    syncObjectDisplayValues(member);
  };

  function createField(member) {
    var field = $("<div>").addClass("item-selector").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);
    createInput(member).appendTo(field);
    createDisplayValues(member).appendTo(field);
    createButtons(member).appendTo(field);
    return field;
  }

  function createInput(member) {
    var identity = "relationMember" + member.id;
    var primaryIds = getPrimaryIds(member);
    var input = $("<input>").attr("id", identity).attr("name", identity).attr("type", "hidden").attr("value", primaryIds).change(
      function () {
        syncObjectDisplayValues(member);
      }
    );

    return input;
  }

  function createDisplayValues(member) {
    var identity = "relationMember" + member.id;

    return $("<div>").addClass("item-selector__keys").attr("id", identity + "DisplayValues");
  }

  function createButtons(member) {
    var buttons = $("<div>").addClass("form__buttons").addClass("form__buttons--minor").addClass("buttons");

    createButton(member).appendTo(buttons);
    return buttons;
  }

  function createButton(member) {
    var identity = "relationMember" + member.id;

    return $("<button>").addClass("buttons__button").addClass("buttons__button--minor").addClass("button").addClass("button--positive").addClass("button--minor").attr("type", "button").html("Select…").click(
      function () {
        platformus.forms.objectSelectorForm.show(
          member.relationClass.id,
          $("#" + identity).val(),
          member.minRelatedObjectsNumber,
          member.maxRelatedObjectsNumber,
          function (objectIds) {
            $("#" + identity).val(objectIds);
            $("#" + identity).trigger("change");
          }
        );
      }
    );
  }

  function getPrimaryIds(member) {
    var primaryIds = platformus.string.empty;

    for (var i = 0; i < member.relations.length; i++) {
      var relation = member.relations[i];

      if (!platformus.string.isNullOrEmpty(primaryIds)) {
        primaryIds += ",";
      }

      primaryIds += relation.primaryId;
    }

    return primaryIds;
  }

  function syncObjectDisplayValues(member) {
    var identity = "relationMember" + member.id;

    $.get(
      "/backend/domain/getobjectdisplayvalues?objectids=" + $("#" + identity).val(),
      function (result) {
        $("#" + identity + "DisplayValues").html(result);
      }
    );
  }
})(window.platformus = window.platformus || {});