/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Controls.Behaviors.DropDownList {
  export function apply(): void {
    extendJQuery();
    defineHandlers();
  }

  function extendJQuery(): void {
    var $val = $.fn.val;

    $.fn.val = function (value) {
      if (arguments.length == 0) {
        if (this.hasClass("drop-down-list")) {
          return this.find("input").val();
        }

        return $val.call(this);
      }

      if (this.hasClass("drop-down-list")) {
        var dropDownList = this,
          text = dropDownList.find(".text"),
          input = dropDownList.find("input"),
          dropDownListItems = dropDownList.find(".drop-down-list-items"),
          dropDownListItem = dropDownListItems.find(".drop-down-list-item[data-value='" + value + "']");

        text.html(dropDownListItem.html());

        var result = input.val(value);

        dropDownList.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers(): void {
    $(document.body).on("click", globalClickHandler);
    $(document.body).on("click", ".drop-down-list .text", textClickHandler);
    $(document.body).on("click", ".drop-down-list .drop-down-list-items .drop-down-list-item", dropDownListItemClickHandler);
  }

  function globalClickHandler(): boolean {
    $(".drop-down-list .drop-down-list-items").slideUp("fast");
    return true;
  }

  function textClickHandler(): boolean {
    $(this).parent().find(".drop-down-list-items").slideDown("fast");
    return false;
  }

  function dropDownListItemClickHandler(): boolean {
    var dropDownListItem = $(this),
      dropDownListItems = dropDownListItem.parent(),
      dropDownList = dropDownListItems.parent();

    dropDownList.val(<any>dropDownListItem.data("value"));
    dropDownListItems.fadeOut("fast");
    return false;
  }
}