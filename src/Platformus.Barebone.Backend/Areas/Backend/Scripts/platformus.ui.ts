// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

module Platformus.Ui {
  declare var tinymce: any;

  export function initializeTinyMce(identity: string): void {
    tinymce.init(
      {
        selector: "#" + identity,
        plugins: [
          "advlist anchor autolink charmap code contextmenu fullscreen image insertdatetime link lists media paste preview print searchreplace table visualblocks",
        ],
        menubar: "edit insert view format table tools",
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist | outdent indent | link image",
        statusbar: false,
        convert_urls: false,
        file_browser_callback: getTinymceFileBrowserCallback()
      }
    );
  }

  export var tinymceFileBrowserCallback: any;

  function getTinymceFileBrowserCallback(): any {
    return tinymceFileBrowserCallback;
  }
}