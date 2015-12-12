/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Editors.SingleLinePlainText {
  export function create(container: JQuery, member: any): void {
    createField(container, member);
  }

  function createField(container: JQuery, member: any): JQuery {
    var field = $("<div>").addClass("field").appendTo(container);

    createLabel(member).appendTo(field);
    member.property.localizations.forEach(
      (localization, index) => {
        createCulture(localization).appendTo(field);
        createInput(member, localization).appendTo(field);

        if (index != member.property.localizations.length - 1) {
          createMultilingualSeparator().appendTo(field);
        }
      }
    );
    
    return field;
  }

  function createLabel(member: any): JQuery {
    return $("<label>").html(member.name);
  }

  function createInput(member: any, localization: any): JQuery {
    var identity = "propertyMember" + member.id + localization.culture.code;

    return $("<input>").attr("id", identity).attr("name", identity).attr("type", "text").attr("value", localization.value);
  }

  function createCulture(localization: any): JQuery {
    var culture = $("<div>").addClass("culture");

    createFlag(localization).appendTo(culture);
    return culture;
  }

  function createFlag(localization: any): JQuery {
    return $("<div>").addClass("flag").html(localization.culture.code);
  }

  function createMultilingualSeparator(): JQuery {
    return $("<div>").addClass("multilingual-separator");
  }
}