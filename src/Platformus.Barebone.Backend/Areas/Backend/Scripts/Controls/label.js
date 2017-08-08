// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.label = {};
  platformus.controls.label.create = function (descriptor) {
    return $("<label>")
      .addClass("field__label")
      .addClass("label")
      .attr("for", descriptor.identity)
      .html(descriptor.text);
  };
})(window.platformus = window.platformus || {});