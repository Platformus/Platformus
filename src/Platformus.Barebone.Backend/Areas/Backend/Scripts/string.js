// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.string = platformus.string || {};
  platformus.string.empty = "";
  platformus.string.isNullOrEmpty = function (value) {
    return value == null || value.length == 0;
  };
})(window.platformus = window.platformus || {});