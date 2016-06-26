// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />

module Platformus.Controls.Extenders.TabStop {
  export function apply(): void {
    $("a").attr("tabindex", 0);
  }
}