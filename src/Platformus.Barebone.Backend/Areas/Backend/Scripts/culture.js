// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.culture = platformus.culture || {};
  platformus.culture.client = function () {
    var culture = window.navigator.userLanguage || window.navigator.language;

    if (platformus.string.isNullOrEmpty(culture)) {
      culture = "en";
    }

    if (culture.length > 2) {
      culture = culture.substring(0, 2);
    }

    return culture;
  };

  platformus.culture.server = function () {
    return $("html").data("culture");
  };
})(window.platformus = window.platformus || {});