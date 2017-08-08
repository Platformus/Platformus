// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.radioButtonList = {};
  platformus.controls.radioButtonList.create = function (descriptor) {
    var radioButtonList = $("<div>").addClass("field__radio-button-list radio-button-list").attr("id", descriptor.identity);

    for (var i = 0; i != descriptor.options.length; i++) {
      createRadioButton(descriptor, descriptor.options[i]).appendTo(radioButtonList);
    }

    createInput(descriptor).appendTo(radioButtonList);
    return radioButtonList;
  };

  function createRadioButton(descriptor, option) {
    var radioButton = $("<a>").addClass("radio-button-list__radio-button radio-button").attr("href", "#").attr("data-value", option.value);

    createRadioButtonIndicator(descriptor, option).appendTo(radioButton);
    createRadioButtonLabel(descriptor, option).appendTo(radioButton);
    return radioButton;
  }

  function createRadioButtonIndicator(descriptor, option) {
    var radioButtonIndicator = $("<div>").addClass("radio-button__indicator");

    if (option.value == descriptor.value) {
      radioButtonIndicator.addClass("radio-button__indicator--checked");
    }

    return radioButtonIndicator;
  }

  function createRadioButtonLabel(descriptor, option) {
    return $("<div>").addClass("radio-button__label").html(option.text);
  }

  function createInput(descriptor) {
    return $("<input>")
      .attr("name", descriptor.identity)
      .attr("type", "hidden")
      .attr("value", descriptor.value);
  }
})(window.platformus = window.platformus || {});