// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.culture = platformus.culture || {};
  platformus.culture.server = function () {
    return $("html").attr("lang");
  };
})(window.platformus = window.platformus || {});