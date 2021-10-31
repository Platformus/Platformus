// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
      performInitialActions();
    }
  );

  function defineHandlers() {
    $(document.body).on("click", ".field__numeric-button--up", numericButtonUpClickHandler);
    $(document.body).on("click", ".field__numeric-button--down", numericButtonDownClickHandler);
    $(document.body).on("keypress", "[data-type='number']", keyPressHandler);
  }

  function performInitialActions() {
    $("[data-type='number']").each(function (index, element) {
      createNumericButtons().appendTo($(element).parent());
    });
  }

  function numericButtonUpClickHandler() {
    var textBox = $(this).parent().parent().find("input");
    var value = textBox.val();

    if (value) {
      value = parseInt(value);
    }

    else {
      value = 0;
    }

    textBox.val(value + 1);
    textBox.change();
    return false;
  }

  function numericButtonDownClickHandler() {
    var textBox = $(this).parent().parent().find("input");
    var value = textBox.val();

    if (value) {
      value = parseInt(value);
    }

    else {
      value = 0;
    }

    textBox.val(value - 1);
    textBox.change();
    return false;
  }

  function keyPressHandler(e) {
    return (e.keyCode >= 47 && e.keyCode <= 57) || e.keyCode == 44 || e.keyCode == 46;
  }

  function createNumericButtons() {
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