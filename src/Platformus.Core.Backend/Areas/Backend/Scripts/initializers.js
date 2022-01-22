// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

$(document).ready(
  function () {
    platformus.initializers.sort(
      function (a, b) { return (a.priority > b.priority) ? 1 : ((a.priority < b.priority) ? -1 : 0); }
    );

    platformus.initializers.forEach(i => i.action());
  }
);

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        window.oncontextmenu = function (event) {
          event.preventDefault();
          event.stopPropagation();
          return false;
        };
      },
      priority: 0
    }
  );
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        moment.locale(platformus.globalization.getCultureCode());
        moment.fn.toISOStringWithoutTimezone = function () {
          return this.format("YYYY-MM-DD[T]HH:mm:ss");
        };
      },
      priority: 0
    }
  );
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        $.mask.definitions["D"] = "[0-9]";
        $.mask.definitions["M"] = "[0-9]";
        $.mask.definitions["Y"] = "[0-9]";
        $.mask.definitions["H"] = "[0-9]";
        $.mask.definitions["T"] = "[ampAMP]";
      },
      priority: 0
    }
  );
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        if (!$("form").length) return;

        $(window).on("beforeunload", function () {
          if (platformus.needsSaveConfirmation) {
            return confirm("Your changes might be lost!");
          }
        });

        $(document.body).on("change", "input, textarea", function () {
          if ($(this).attr("type") != "file" && !$(this).data("propertyPath")) {
            platformus.needsSaveConfirmation = true;
          }
        });

        $(document.body).on("click", "button[type='submit']", function () {
          platformus.needsSaveConfirmation = null;
        });
      },
      priority: 0
    }
  );
})(window.platformus = window.platformus || {});