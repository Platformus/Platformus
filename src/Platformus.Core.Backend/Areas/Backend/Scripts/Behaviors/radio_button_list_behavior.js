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
        if (this.hasClass("radio-button-list")) {
          return this.find("input").val();
        }

        return $val.call(this);
      }

      if (this.hasClass("radio-button-list")) {
        var radioButtonList = this,
          radioButton = radioButtonList.find(".radio-button-list__radio-button[data-value='" + value + "']"),
          input = radioButtonList.find("input");

        radioButtonList.find(".radio-button__indicator").removeClass("radio-button__indicator--checked");
        radioButton.find(".radio-button__indicator").addClass("radio-button__indicator--checked");

        var result = input.val(value);

        input.trigger("change");
        radioButtonList.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers() {
    $(document.body).on("click", ".radio-button", onRadioButtonClick);
  }

  function onRadioButtonClick() {
    var radioButton = $(this),
      radioButtonList = radioButton.closest(".radio-button-list");

    radioButtonList.find(".radio-button__indicator").removeClass("radio-button__indicator--checked");
    radioButton.find(".radio-button__indicator").addClass("radio-button__indicator--checked");
    radioButtonList.val($(this).data("value"));
    return false;
  }
})(window.platformus = window.platformus || {});