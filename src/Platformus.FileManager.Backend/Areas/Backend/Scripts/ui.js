// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.tinyMceFileBrowserCallback = function (fieldId, url, type, wnd) {
    platformus.forms.fileSelectorForm.show(
      function (filename) {
        wnd.document.getElementById(fieldId).value = "/files/" + filename;
      }
    );
  };
})(window.platformus = window.platformus || {});