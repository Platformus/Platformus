// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.deleteForm = {};
  platformus.forms.deleteForm.show = function (url) {
    return platformus.forms.baseForm.show("/backend/barebone/deleteform?targeturl=" + encodeURIComponent(url));
  };

  platformus.forms.deleteForm.hideAndRemove = function () {
    return platformus.forms.baseForm.hideAndRemove();
  };
})(window.platformus = window.platformus || {});