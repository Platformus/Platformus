// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.create = function () {
    var tabs = $("#tabs");
    var tabPages = $("#tabPages");

    for (var i = 0; i < membersByTabs.length; i++) {
      var tab = membersByTabs[i];

      createTab(tab).appendTo(tabs);

      var tabPage = createTabPage(tab).appendTo(tabPages);

      for (var j = 0; j < tab.members.length; j++) {
        var member = tab.members[j];

        if (member.relationClass != null) {
          platformus.memberEditors.relation.create(tabPage, member);
        }

        else if (member.propertyDataType != null) {
          var memberEditor = platformus.memberEditors[member.propertyDataType.javaScriptEditorClassName];

          if (memberEditor != null) {
            var f = memberEditor["create"];

            if (f != null) {
              f.call(this, tabPage, member);
            }
          }
        }
      }
    }

    platformus.ui.initializeJQueryValidation();
  };

  function createTab(tab) {
    return $("<div>").addClass("tabs__tab").attr("data-tab-page-id", tab.id).html(tab.name);
  }

  function createTabPage(tab) {
    var tabPage = $("#tabPage" + tab.id);

    if (tabPage.length != 0) {
      return tabPage;
    }

    return $("<div>").addClass("tab-pages__tab-page").attr("id", "tabPage" + tab.id);
  }
})(window.platformus = window.platformus || {});