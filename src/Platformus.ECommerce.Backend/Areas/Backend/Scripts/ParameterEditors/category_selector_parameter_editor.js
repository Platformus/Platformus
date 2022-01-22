// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditor = platformus.parameterEditor || [];
  platformus.parameterEditor.categorySelector = {};
  platformus.parameterEditor.categorySelector.showCategorySelectorForm = function (code) {
    var valueElement = $("#parameter" + code);

    return platformus.forms.categorySelectorForm.show(
      valueElement.val(),
      function (classId) {
        valueElement.val(classId).trigger("change");
        platformus.parameterEditor.categorySelector.sync(code);
      }
    );
  };

  platformus.parameterEditor.categorySelector.sync = function (code) {
    var identity = "parameter" + code;
    var value = $("#" + identity).val();

    if (!value) return;

    $.get(
      "/backend/ecommerce/category/" + value,
      function (category) {
        $("#" + identity + "Keys").empty().append($("<div>").addClass("item-selector__key").html(category.name));
      }
    );
  };
})(window.platformus = window.platformus || {});