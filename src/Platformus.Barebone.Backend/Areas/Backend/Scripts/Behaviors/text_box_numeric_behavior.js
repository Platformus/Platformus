// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
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

    if (platformus.string.isNullOrEmpty(value)) {
      value = 0;
    }

    else {
      value = parseInt(value);
    }

    textBox.val(value + 1);
    return false;
  }

  function numericButtonDownClickHandler() {
    var textBox = $(this).parent().parent().find("input");
    var value = textBox.val();

    if (platformus.string.isNullOrEmpty(value)) {
      value = 0;
    }

    else {
      value = parseInt(value);
    }

    textBox.val(value - 1);
    return false;
  }
})(window.platformus = window.platformus || {});