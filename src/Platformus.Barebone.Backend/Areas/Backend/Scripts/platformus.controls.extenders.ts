// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.controls.extenders.maxlengthindicator.ts" />
/// <reference path="platformus.controls.extenders.tabstop.ts" />

module Platformus.Controls.Extenders {
  export function apply(): void {
    MaxLengthIndicator.apply();
    TabStop.apply();
  }
}