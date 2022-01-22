// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      extendJQuery();
      defineHandlers();
    }
  );

  function extendJQuery() {
    var $val = $.fn.val;

    $.fn.val = function (value) {
      if (arguments.length == 0) {
        if (this.hasClass("numeric-box")) {
          return getValue(this.find(".numeric-box__text-box"), null);
        }

        return $val.call(this);
      }

      if (this.hasClass("numeric-box")) {
        var numericBox = this, numericBox = numericBox.find(".numeric-box__text-box");
        var result = numericBox.val(value);

        numericBox.trigger("change");
        numericBox.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers() {
    $(document.body).on("click", ".numeric-box__button--minus", onMinusButtonClick);
    $(document.body).on("click", ".numeric-box__button--plus", onPlusButtonClick);
    $(document.body).on("keypress", "[data-type='integer'],[data-type='decimal']", onKeyPress);
  }

  function onMinusButtonClick() {
    var numericBox = $(this).closest(".numeric-box").find(".numeric-box__text-box");
    var value = getValue(numericBox, 0);

    numericBox.val(value - 1);
    numericBox.change();
    return false;
  }

  function onPlusButtonClick() {
    var numericBox = $(this).closest(".numeric-box").find(".numeric-box__text-box");
    var value = getValue(numericBox, 0);

    numericBox.val(value + 1);
    numericBox.change();
    return false;
  }

  function onKeyPress(e) {
    var numericBox = $(this);

    if (isInteger(numericBox)) {
      return e.keyCode >= 47 && e.keyCode <= 57;
    }

    return (e.keyCode >= 47 && e.keyCode <= 57) || e.keyCode == 44 || e.keyCode == 46;
  }

  function isInteger(numericBox) {
    return numericBox.data("type") == "integer";
  }

  function getValue(numericBox, defaultValue) {
    var value = numericBox.val();

    if (value) {
      return numericBox.data("type") == "integer" ? parseInt(value) : parseFloat(value);
    }

    return defaultValue;
  }
})(window.platformus = window.platformus || {});