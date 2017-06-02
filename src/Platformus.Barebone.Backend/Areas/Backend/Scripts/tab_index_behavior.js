// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      performInitialActions();
    }
  );

  function performInitialActions() {
    $("a").attr("tabindex", 0);
  }
})(window.platformus = window.platformus || {});