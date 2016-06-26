// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />

module Platformus.Controls.Behaviors.Tabs {
  export function apply(): void {
    defineHandlers();
    performInitialActions();
  }

  function defineHandlers(): void {
    $(document.body).on("click", ".tabs .tab", checkboxClickHandler);
  }

  function performInitialActions(): void {
    $(".tabs .tab").first().click();
  }

  function checkboxClickHandler(): boolean {
    $(".tabs .tab").removeClass("active");
    $(".tab-page").hide();
    $(this).addClass("active");
    $("#tabPage" + $(this).data("tabPageId")).show();
    return false;
  }
}