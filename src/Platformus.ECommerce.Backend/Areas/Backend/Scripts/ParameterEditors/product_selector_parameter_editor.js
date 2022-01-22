// Copyright © 2022 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditor = platformus.parameterEditor || [];
  platformus.parameterEditor.productSelector = {};
  platformus.parameterEditor.productSelector.showCategorySelectorForm = function (code) {
    var valueElement = $("#parameter" + code);

    return platformus.forms.productSelectorForm.show(
      valueElement.val(),
      function (classId) {
        valueElement.val(classId).trigger("change");
        platformus.parameterEditor.productSelector.sync(code);
      }
    );
  };

  platformus.parameterEditor.productSelector.sync = function (code) {
    var identity = "parameter" + code;
    var value = $("#" + identity).val();

    if (!value) return;

    $.get(
      "/backend/ecommerce/product/" + value,
      function (product) {
        $("#" + identity + "Keys").empty().append($("<div>").addClass("item-selector__key").html(product.name));
      }
    );
  };
})(window.platformus = window.platformus || {});