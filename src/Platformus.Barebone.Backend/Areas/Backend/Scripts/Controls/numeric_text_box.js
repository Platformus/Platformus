// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.numericTextBox = {};
  platformus.controls.numericTextBox.create = function (descriptor) {
    return platformus.controls.textBox.create(descriptor).addClass("field__text-box--numeric");
  };

  platformus.controls.numericTextBox.createNumericButtons = function () {
    var buttons = $("<div>").addClass("field__numeric-buttons");

    createNumericUpButton().appendTo(buttons);
    createNumericDownButton().appendTo(buttons);
    return buttons;
  };

  function createNumericUpButton() {
    return $("<a>").addClass("field__numeric-button").addClass("field__numeric-button--up").attr("href", "#");
  }

  function createNumericDownButton() {
    return $("<a>").addClass("field__numeric-button").addClass("field__numeric-button--down").attr("href", "#");
  }
})(window.platformus = window.platformus || {});