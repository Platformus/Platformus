/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.ui.ts" />
module Platformus.Editors.Relation {
  export function create(container: JQuery, member: any): void {
    createField(container, member);

    if (!member.isRelationSingleParent) {
      createButtons(container, member);
    }

    syncObjectDisplayValues(member);
  }

  function createField(container: JQuery, member: any): void {
    var field = $("<div>").addClass("field").appendTo(container);

    if (member.isRelationSingleParent) {
      field.hide();
    }

    createLabel(member).appendTo(field);
    createInput(member).appendTo(field);
    createDisplayValues(member).appendTo(field);
  }

  function createLabel(member: any): JQuery {
    return $("<label>").html(member.name);
  }

  function createInput(member: any): JQuery {
    var identity = "relationMember" + member.id;
    var primartyIds = getPrimaryIds(member);
    var input = $("<input>").attr("id", identity).attr("name", identity).attr("type", "hidden").attr("value", primartyIds).change(
      function () {
        syncObjectDisplayValues(member);
      }
    );

    return input;
  }

  function createDisplayValues(member): JQuery {
    var identity = "relationMember" + member.id;

    return $("<div>").addClass("display-values").attr("id", identity + "DisplayValues");
  }

  function createButtons(container: JQuery, member: any): void {
    var buttons = $("<div>").addClass("buttons").addClass("tool").appendTo(container);

    createButton(member).appendTo(buttons);
    createClear().appendTo(buttons);
  }

  function createButton(member: any): JQuery {
    var identity = "relationMember" + member.id;

    return $("<button>").addClass("positive").attr("type", "button").html("Select…").click(
      function () {
        Platformus.Ui.showObjectSelectorForm(member.relationClass.id, identity);
      }
    );
  }

  function createClear(): JQuery {
    return $("<div>").addClass("clear");
  }

  function getPrimaryIds(member: any): string {
    var primaryIds = String.empty;

    member.relations.forEach(
      (relation, index) => {
        if (!String.isNullOrEmpty(primaryIds)) {
          primaryIds += ",";
        }

        primaryIds += relation.primaryId;
      }
    );

    return primaryIds;
  }

  function syncObjectDisplayValues(member: any): void {
    var identity = "relationMember" + member.id;

    $.get(
      "/backend/content/getobjectdisplayvalues?objectids=" + $("#" + identity).val(),
      function (result) {
        $("#" + identity + "DisplayValues").html(result);
      }
    );
  }
}