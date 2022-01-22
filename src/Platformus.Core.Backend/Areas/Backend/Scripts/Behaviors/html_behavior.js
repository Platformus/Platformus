// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      performInitialActions();
    }
  );

  function performInitialActions() {
    $("[data-type='html']").each(function (index, element) {
      platformus.tinyMce.initialize($(element).attr("id"));
    });
  }
})(window.platformus = window.platformus || {});