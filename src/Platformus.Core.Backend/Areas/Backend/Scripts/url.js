// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.url = platformus.url || {};
  platformus.url.getParameters = function () {
    var result = {};
    var match,
      search = /([^&=]+)=?([^&]*)/g,
      query = window.location.search.substring(1),
      plus = /\+/g,
      decode = function (s) { return decodeURIComponent(s.replace(plus, " ")); };

    while (match = search.exec(query)) {
      result[decode(match[1])] = decode(match[2]);
    }

    return result;
  };

  platformus.url.combine = function (parameters) {
    var result = "";
    var currentParameters = platformus.url.getParameters();

    parameters.forEach(function (parameter) {
      if (!parameter.skip) {
        var value = parameter.takeFromUrl ? currentParameters[parameter.name] : parameter.value;

        if (value != null) {
          value = encodeURIComponent(value);
          result += (result ? "&" : "?") + parameter.name + "=" + value;
        }
      }
    });

    for (var parameter in currentParameters) {
      if (!containsParameter(parameters, parameter)) {
        var value = currentParameters[parameter];

        if (value) {
          value = encodeURIComponent(value);
          result += (result ? "&" : "?") + parameter + "=" + value;
        }
      }
    }

    return location.pathname + result;
  }

  function containsParameter(parameters, parameter) {
    return parameters.some(p => p.name == parameter);
  }
})(window.platformus = window.platformus || {});