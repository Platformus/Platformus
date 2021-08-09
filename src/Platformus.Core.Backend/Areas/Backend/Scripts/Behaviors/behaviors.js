// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        platformus.behaviors.forEach(b => b());
      },
      priority: 0
    }
  );
})(window.platformus = window.platformus || {});