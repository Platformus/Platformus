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
        if (this.hasClass("checkbox")) {
          return JSON.parse(this.find("input").val());
        }

        return $val.call(this);
      }

      if (this.hasClass("checkbox")) {
        var checkbox = this, input = checkbox.find("input");
        var result = input.val(JSON.parse(value));

        updateIndicator(checkbox);
        checkbox.trigger("change");
        input.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers() {
    $(document.body).on("click", ".checkbox", onClick);
  }

  function onClick() {
    $(this).val(!$(this).val());
    updateIndicator($(this));
    return false;
  }

  function updateIndicator(checkbox) {
    if (checkbox.val()) {
      checkbox.find(".checkbox__indicator").addClass("checkbox__indicator--checked");
    }

    else {
      checkbox.find(".checkbox__indicator").removeClass("checkbox__indicator--checked");
    }
  }
})(window.platformus = window.platformus || {});