/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Editors {
  export function apply(membersByTabs: any): void {
    membersByTabs.forEach(
      (tab: any) => {
        var tabs = $("#tabs");

        createTab(tab).appendTo(tabs);

        var members = $("#members");
        var tabPage = createTabPage(tab).appendTo(members);

        tab.members.forEach(
          (member: any) => {
            if (member.relationClass != null) {
              Platformus.Editors.Relation.create(tabPage, member);
            }

            else if (member.propertyDataType != null) {
              var f = Platformus.Editors[member.propertyDataType.javaScriptEditorClassName]["create"];

              f.call(this, tabPage, member);
            }
          }
        );
      }
    );

    $("<div>").addClass("clear").appendTo($("#tabs"));
  }

  function createTab(tab: any): JQuery {
    return $("<div>").addClass("tab").attr("data-tab-page-id", tab.id).html(tab.name);
  }

  function createTabPage(tab: any): JQuery {
    var tabPage = $("#tabPage" + tab.id);

    if (tabPage.length != 0) {
      return tabPage;
    }

    return $("<div>").addClass("tab-page").attr("id", "tabPage" + tab.id);
  }
}