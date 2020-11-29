// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.categorySelector = {};
  platformus.parameterEditors.categorySelector.create = function (container, parameter) {
    platformus.parameterEditors.baseItemSelector.create(
      container,
      parameter,
      platformus.forms.categorySelectorForm,
      "/backend/ecommerce/category/",
      function (category) {
        return $("<div>").addClass("item-selector__key").html(category.name);
      }
    );
  };
})(window.platformus = window.platformus || {});