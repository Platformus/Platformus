// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/platformus/platformus.ui.d.ts" />
/// <reference path="platformus.overlays.fileselectorform.ts" />

module Platformus.Ui {
  export function showFileSelectorForm(identity: string, url: string, code: string, wnd: Window) {
    new Platformus.Overlays.FileSelectorForm(
      function (filename: string) {
        (<HTMLInputElement>wnd.document.getElementById(identity)).value = filename;
      }
    ).show();
  }
}

Platformus.Ui.tinymceFileBrowserCallback = Platformus.Ui.showFileSelectorForm;