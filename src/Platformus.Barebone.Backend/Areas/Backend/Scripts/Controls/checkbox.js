// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.checkbox = {};
  platformus.controls.checkbox.create = function (descriptor) {
    var checkbox = $("<a>")
      .addClass("checkbox")
      .attr("id", descriptor.identity)
      .attr("href", "#");

    createIndicator(descriptor).appendTo(checkbox);
    createLabel(descriptor).appendTo(checkbox);
    createInput(descriptor).appendTo(checkbox);
    return checkbox;
  };

  function createIndicator(descriptor) {
    var indicator = $("<div>").addClass("checkbox__indicator");

    if (descriptor.value == "true")
      indicator.addClass("checkbox__indicator--checked");

    return indicator;
  }

  function createLabel(descriptor) {
    return $("<div>").addClass("checkbox__label").html(descriptor.text);
  };

  function createInput(descriptor) {
    return $("<input>")
      .attr("name", descriptor.identity)
      .attr("type", "hidden")
      .attr("value", platformus.string.isNullOrEmpty(descriptor.value) ? "false" : descriptor.value);
  }
})(window.platformus = window.platformus || {});