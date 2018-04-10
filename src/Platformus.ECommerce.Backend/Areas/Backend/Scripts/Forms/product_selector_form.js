// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  var _callback = null;

  platformus.forms = platformus.forms || {};
  platformus.forms.productSelectorForm = {};
  platformus.forms.productSelectorForm.show = function (productId, callback) {
    _callback = callback;
    return platformus.forms.baseForm.show(
      "/backend/ecommerce/productselectorform?productid=" + productId,
      defineHandlers,
      "product-selector-pop-up-form"
    );
  };

  platformus.forms.productSelectorForm.select = function () {
    if (_callback != null) {
      _callback(getSelectedProductId());
    }

    return platformus.forms.baseForm.hideAndRemove();
  };

  platformus.forms.productSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseForm.hideAndRemove();
  };

  function defineHandlers() {
    platformus.forms.activeForm.find(".table__row").bind("click", rowClickHandler);
  }

  function rowClickHandler() {
    if ($(this).find(".table__cell--header").length != 0) {
      return;
    }

    platformus.forms.activeForm.find(".table__row").removeClass("table__row--selected");
    $(this).addClass("table__row--selected");
  }

  function getSelectedProductId() {
    var selectedRow = platformus.forms.activeForm.find(".table__row--selected");

    if(selectedRow.length == 0) {
      return null;
    }

    return selectedRow.data("productId");
  }
})(window.platformus = window.platformus || {});