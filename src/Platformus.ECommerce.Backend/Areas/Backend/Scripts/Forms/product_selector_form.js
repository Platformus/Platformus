// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.productSelectorForm = {};
  platformus.forms.productSelectorForm.show = function (productId, callback) {
    return platformus.forms.baseItemSelectorForm.show(
      "/backend/ecommerce/productselectorform?productid=" + productId, null, 1, callback
    );
  };

  platformus.forms.productSelectorForm.select = function () {
    return platformus.forms.baseItemSelectorForm.select();
  };

  platformus.forms.productSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseItemSelectorForm.hideAndRemove();
  };
})(window.platformus = window.platformus || {});