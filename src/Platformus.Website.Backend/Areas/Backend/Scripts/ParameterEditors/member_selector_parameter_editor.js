// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.memberSelector = {};
  platformus.parameterEditors.memberSelector.create = function (container, parameter) {
    platformus.parameterEditors.baseItemSelector.create(
      container,
      parameter,
      platformus.forms.memberSelectorForm,
      "/backend/website/member/",
      function (member) {
        return $("<div>").addClass("item-selector__key").html(member.class.name + "." + member.name);
      }
    );
  };
})(window.platformus = window.platformus || {});