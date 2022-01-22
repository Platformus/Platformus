// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    $(document.body).on("change", "#limit", onLimitChange);
  }

  function onLimitChange() {
    location.href = platformus.url.combine(
      [
        { name: "sorting", takeFromUrl: true },
        { name: "offset", skip: true },
        { name: "limit", value: $(this).val() }
      ]
    );
  }
})(window.platformus = window.platformus || {});