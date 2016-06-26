// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.controls.behaviors.checkbox.ts" />
/// <reference path="platformus.controls.behaviors.dropdownlist.ts" />
/// <reference path="platformus.controls.behaviors.grid.ts" />
/// <reference path="platformus.controls.behaviors.tabs.ts" />
/// <reference path="platformus.controls.behaviors.uploader.ts" />
/// <reference path="platformus.controls.behaviors.imageuploader.ts" />

module Platformus.Controls.Behaviors {
  export function apply(): void {
    Checkbox.apply();
    DropDownList.apply();
    Grid.apply();
    Tabs.apply();
    Uploader.apply();
    ImageUploader.apply();
  }
}