/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Controls.Behaviors.Checkbox {
  export function apply(): void {
    extendJQuery();
    defineHandlers();
  }

  function extendJQuery(): void {
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

  function defineHandlers(): void {
    $(document.body).on("click", ".checkbox", checkboxClickHandler);
  }

  function checkboxClickHandler(): boolean {
    $(this).find(".indicator").toggleClass("checked");
    $(this).val((!$(this).val()).toString());
    return false;
  }
}