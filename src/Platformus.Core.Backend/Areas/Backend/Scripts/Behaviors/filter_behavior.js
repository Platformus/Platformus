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
    $(document.body).on("change", "[data-property-path]", onChange);
  }

  function onChange() {
    var criterion = $(this);
    var value = criterion.val();

    if (criterion.data("type") == "date") {
      value = moment(value, platformus.globalization.getDateFormat());
      value = value.isValid() ? value.toISOStringWithoutTimezone() : null;
    }

    else if (criterion.data("type") == "date-time") {
      value = moment(value, platformus.globalization.getDateTimeFormat());
      value = value.isValid() ? value.toISOStringWithoutTimezone() : null;
    }

    location.href = platformus.url.combine(
      [
        { name: $(this).data("propertyPath"), value: value ? value : null, skip: !value },
        { name: "sorting", takeFromUrl: true },
        { name: "offset", skip: true },
        { name: "limit", skip: true }
      ]
    );
  }
})(window.platformus = window.platformus || {});