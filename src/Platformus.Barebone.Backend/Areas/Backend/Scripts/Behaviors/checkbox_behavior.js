// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
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
        if (this.hasClass("checkbox")) {
          var value = this.find("input").val();

          if (this.data("useIntegerNumber")) {
            return value == "1";
          }

          return value == "true";
        }

        return $val.call(this);
      }

      if (this.hasClass("checkbox")) {
        var result = null;
        var input = this.find("input");

        value = value == true || value == "true" || value == 1 || value == "1";

        if (this.data("useIntegerNumber")) {
          input.val(value ? 1 : 0);
        }

        else {
          input.val(value ? true : false);
        }

        this.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers() {
    $(document.body).on("click", ".checkbox", checkboxClickHandler);
  }

  function checkboxClickHandler() {
    $(this).find(".checkbox__indicator").toggleClass("checkbox__indicator--checked");

    var value = $(this).val();

    if ($(this).data("useIntegerNumber")) {
      $(this).val(value ? "0" : "1");
    }

    else {
      $(this).val(value ? "false" : "true");
    }

    return false;
  }
})(window.platformus = window.platformus || {});