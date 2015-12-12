/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.controls.behaviors.checkbox.ts" />
/// <reference path="platformus.controls.behaviors.dropdownlist.ts" />
/// <reference path="platformus.controls.behaviors.grid.ts" />
/// <reference path="platformus.controls.behaviors.tabs.ts" />
/// <reference path="platformus.controls.behaviors.uploader.ts" />
module Platformus.Controls.Behaviors {
  export function apply(): void {
    Checkbox.apply();
    DropDownList.apply();
    Grid.apply();
    Tabs.apply();
    Uploader.apply();
  }
}