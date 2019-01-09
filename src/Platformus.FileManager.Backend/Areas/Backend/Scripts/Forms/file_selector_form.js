// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.fileSelectorForm = {};
  platformus.forms.fileSelectorForm.show = function (callback) {
    return platformus.forms.baseItemSelectorForm.show(
      "/backend/filemanager/fileselectorform", null, 1, callback
    );
  };

  platformus.forms.fileSelectorForm.select = function () {
    return platformus.forms.baseItemSelectorForm.select();
  };

  platformus.forms.fileSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseItemSelectorForm.hideAndRemove();
  };
})(window.platformus = window.platformus || {});