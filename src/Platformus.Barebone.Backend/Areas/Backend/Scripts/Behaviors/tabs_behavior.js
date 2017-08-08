// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
      performInitialActions();
    }
  );

  function defineHandlers() {
    $(document.body).on("click", ".tabs__tab", tabClickHandler);
  }

  function performInitialActions() {
    $(".tabs__tab").first().click();
  }

  function tabClickHandler() {
    $(".tabs__tab").removeClass("tabs__tab--active");
    $(".tab-pages__tab-page").hide();
    $(this).addClass("tabs__tab--active");
    $("#tabPage" + $(this).data("tabPageId")).show();
    return false;
  }
})(window.platformus = window.platformus || {});