// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
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
        var radioButtonList = this, input = radioButtonList.find("input");
        var result = input.val(value);

        radioButtonList.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers() {
    $(document.body).on("click", ".radio-button", radioButtonClickHandler);
  }

  function radioButtonClickHandler() {
    var radioButton = $(this),
      radioButtonList = radioButton.parent();

    radioButtonList.find(".radio-button__indicator").removeClass("radio-button__indicator--checked");
    radioButton.find(".radio-button__indicator").addClass("radio-button__indicator--checked");
    radioButtonList.val($(this).data("value"));
    return false;
  }
})(window.platformus = window.platformus || {});