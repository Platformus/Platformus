// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    $(document.body).on("click", ".field__numeric-button--up", numericButtonUpClickHandler);
    $(document.body).on("click", ".field__numeric-button--down", numericButtonDownClickHandler);
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
})(window.platformus = window.platformus || {});