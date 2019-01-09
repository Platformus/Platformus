// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.classSelectorForm = {};
  platformus.forms.classSelectorForm.show = function (classId, callback) {
    return platformus.forms.baseItemSelectorForm.show(
      "/backend/domain/classselectorform?classid=" + classId, null, 1, callback
    );
  };

  platformus.forms.classSelectorForm.select = function () {
    return platformus.forms.baseItemSelectorForm.select();
  };

  platformus.forms.classSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseItemSelectorForm.hideAndRemove();
  };
})(window.platformus = window.platformus || {});