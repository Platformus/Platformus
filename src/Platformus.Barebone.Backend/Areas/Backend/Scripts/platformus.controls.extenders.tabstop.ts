/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Controls.Extenders.TabStop {
  export function apply(): void {
    $("a").attr("tabindex", 0);
  }
}