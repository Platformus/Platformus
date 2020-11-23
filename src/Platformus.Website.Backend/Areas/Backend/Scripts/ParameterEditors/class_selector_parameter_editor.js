// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.classSelector = {};
  platformus.parameterEditors.classSelector.create = function (container, parameter) {
    platformus.parameterEditors.baseItemSelector.create(
      container,
      parameter,
      platformus.forms.classSelectorForm,
      "/backend/website/class/",
      function ($class) {
        return $("<div>").addClass("item-selector__key").html($class.name);
      }
    );
  };
})(window.platformus = window.platformus || {});