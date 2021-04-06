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
    $(document.body).on("change", "#take", takeChangeHandler);
    $(document.body).on("change", "[data-filtering-property]", filterChangeHandler);
  }

  function takeChangeHandler() {
    location.href = platformus.url.combine(
      [
        { name: "filter", takeFromUrl: true },
        { name: "orderby", takeFromUrl: true },
        { name: "direction", takeFromUrl: true },
        { name: "skip", skip: true },
        { name: "take", value: $(this).val() }
      ]
    );
  }

  function filterChangeHandler(e) {
    var value = $(this).val();

    location.href = platformus.url.combine(
      [
        { name: $(this).data("filteringProperty"), value: platformus.string.isNullOrEmpty(value) ? null : value, skip: platformus.string.isNullOrEmpty(value) },
        { name: "orderby", takeFromUrl: true },
        { name: "direction", takeFromUrl: true },
        { name: "skip", skip: true },
        { name: "take", skip: true }
      ]
    );
  }
})(window.platformus = window.platformus || {});