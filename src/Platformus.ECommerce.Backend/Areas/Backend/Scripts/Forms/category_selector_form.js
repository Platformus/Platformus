// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  var _callback = null;

  platformus.forms = platformus.forms || {};
  platformus.forms.categorySelectorForm = {};
  platformus.forms.categorySelectorForm.show = function (categoryId, callback) {
    _callback = callback;
    return platformus.forms.baseForm.show(
      "/backend/ecommerce/categoryselectorform?categoryid=" + categoryId,
      defineHandlers,
      "category-selector-pop-up-form"
    );
  };

  platformus.forms.categorySelectorForm.select = function () {
    if (_callback != null) {
      _callback(getSelectedCategoryId());
    }

    return platformus.forms.baseForm.hideAndRemove();
  };

  platformus.forms.categorySelectorForm.hideAndRemove = function () {
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

  function getSelectedCategoryId() {
    var selectedRow = platformus.forms.activeForm.find(".table__row--selected");

    if(selectedRow.length == 0) {
      return null;
    }

    return selectedRow.data("categoryId");
  }
})(window.platformus = window.platformus || {});