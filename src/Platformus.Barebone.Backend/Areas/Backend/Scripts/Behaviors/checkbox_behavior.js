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
          return this.find("input").val() == true.toString();
        }

        return $val.call(this);
      }

      if (this.hasClass("checkbox")) {
        var result = this.find("input").val(value);

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
    $(this).val((!$(this).val()).toString());
    return false;
  }
})(window.platformus = window.platformus || {});