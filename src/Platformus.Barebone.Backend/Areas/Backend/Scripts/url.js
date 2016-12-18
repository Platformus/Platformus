// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
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
    var result = platformus.string.empty;
    var parameters = platformus.url.getParameters();

    for (var i = 0; i < descriptors.length; i++) {
      var descriptor = descriptors[i];

      if (!descriptor.skip) {
        var value = descriptor.takeFromUrl ? parameters[descriptor.name] : descriptor.value;

        if (value != null) {
          result += (platformus.string.isNullOrEmpty(result) ? "?" : "&") + descriptor.name + "=" + value;
        }
      }
    }

    for (var parameter in parameters) {
      if (!containsDescriptor(descriptors, parameter)) {
        var value = parameters[parameter];

        if (!platformus.string.isNullOrEmpty(value)) {
          result += (platformus.string.isNullOrEmpty(result) ? "?" : "&") + parameter + "=" + value;
        }
      }
    }

    return location.pathname + result;
  }

  function containsDescriptor(descriptors, parameter) {
    for (var i = 0; i < descriptors.length; i++) {
      if (descriptors[i].name == parameter) {
        return true;
      }
    }

    return false;
  }
})(window.platformus = window.platformus || {});