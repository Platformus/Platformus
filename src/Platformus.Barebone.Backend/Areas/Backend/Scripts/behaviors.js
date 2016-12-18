// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.registerBehavior = function (behaviour) {
    platformus.behaviors.push(behaviour);
  };

  platformus.applyBehaviors = function () {
    for (var i = 0; i < platformus.behaviors.length; i++) {
      platformus.behaviors[i]();
    }
  };
})(window.platformus = window.platformus || {});