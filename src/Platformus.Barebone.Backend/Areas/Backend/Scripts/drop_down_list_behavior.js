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
        if (this.hasClass("drop-down-list")) {
          return this.find("input").val();
        }

        return $val.call(this);
      }

      if (this.hasClass("drop-down-list")) {
        var dropDownList = this,
          selectedDropDownListItem = dropDownList.find(".drop-down-list__item--selected"),
          dropDownListItems = dropDownList.find(".drop-down-list__items"),
          dropDownListItem = dropDownListItems.find(".drop-down-list__item[data-value='" + value + "']"),
          input = dropDownList.find("input");

        selectedDropDownListItem.html(dropDownListItem.html());

        var result = input.val(value);

        dropDownList.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers() {
    $(document.body).on("click", globalClickHandler);
    $(document.body).on("click", ".drop-down-list__item--selected", selectedDropDownListItemClickHandler);
    $(document.body).on("click", ".drop-down-list__item:not(.drop-down-list__item--selected)", dropDownListItemClickHandler);
  }

  function globalClickHandler() {
    $(".drop-down-list__items").slideUp("fast");
    return true;
  }

  function selectedDropDownListItemClickHandler() {
    $(this).parent().find(".drop-down-list__items").slideDown("fast");
    return false;
  }

  function dropDownListItemClickHandler() {
    var dropDownListItem = $(this),
      dropDownListItems = dropDownListItem.parent(),
      dropDownList = dropDownListItems.parent();

    dropDownList.val(dropDownListItem.data("value"));
    dropDownListItems.fadeOut("fast");
    return false;
  }
})(window.platformus = window.platformus || {});