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

  platformus.url.combine = function (descriptors) {
    var result = "";
    var parameters = platformus.url.getParameters();

    descriptors.forEach(function (descriptor) {
      if (!descriptor.skip) {
        var value = descriptor.takeFromUrl ? parameters[descriptor.name] : descriptor.value;

        if (value != null) {
          value = encodeURIComponent(value);
          result += (result ? "&" : "?") + descriptor.name + "=" + value;
        }
      }
    });

    for (var parameter in parameters) {
      if (!containsDescriptor(descriptors, parameter)) {
        var value = parameters[parameter];

        if (value) {
          value = encodeURIComponent(value);
          result += (result ? "&" : "?") + parameter + "=" + value;
        }
      }
    }

    return location.pathname + result;
  }

  function containsDescriptor(descriptors, parameter) {
    return descriptors.some(d => d.name == parameter);
  }
})(window.platformus = window.platformus || {});