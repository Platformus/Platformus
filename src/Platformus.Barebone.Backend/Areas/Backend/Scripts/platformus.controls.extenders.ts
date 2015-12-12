/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.controls.extenders.maxlengthindicator.ts" />
/// <reference path="platformus.controls.extenders.tabstop.ts" />
module Platformus.Controls.Extenders {
  export function apply(): void {
    MaxLengthIndicator.apply();
    TabStop.apply();
  }
}