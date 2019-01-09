// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.categorySelectorForm = {};
  platformus.forms.categorySelectorForm.show = function (categoryId, callback) {
    return platformus.forms.baseItemSelectorForm.show(
      "/backend/ecommerce/categoryselectorform?categoryid=" + categoryId, null, 1, callback
    );
  };

  platformus.forms.categorySelectorForm.select = function () {
    return platformus.forms.baseItemSelectorForm.select();
  };

  platformus.forms.categorySelectorForm.hideAndRemove = function () {
    return platformus.forms.baseItemSelectorForm.hideAndRemove();
  };
})(window.platformus = window.platformus || {});