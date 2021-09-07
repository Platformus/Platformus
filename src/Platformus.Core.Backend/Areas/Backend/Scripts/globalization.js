// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.globalization = platformus.globalization || {};
  platformus.globalization.getCultureCode = function () {
    return $("html").attr("lang");
  };

  platformus.globalization.getDateFormat = function () {
    return moment().localeData().longDateFormat("L");
  }

  platformus.globalization.getDateTimeFormat = function (isVisible) {
    var format = moment().localeData().longDateFormat("L");

    if (moment().localeData().longDateFormat("LT").includes("A")) {
      format += isVisible ? " HH:MM TT" : " hh:mm A";
    }

    else {
      format += isVisible ? " HH:MM" : " HH:mm";
    }

    return format;
  }
})(window.platformus = window.platformus || {});