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
        if (this.hasClass("drop-down-list")) {
          return this.find("input").val();
        }

        return $val.call(this);
      }

      if (this.hasClass("drop-down-list")) {
        var dropDownList = this,
          selectedDropDownListItem = dropDownList.find(".drop-down-list__item--selected"),
          dropDownListItem = dropDownList.find(".drop-down-list__item[data-value='" + value + "']"),
          input = dropDownList.find("input");

        selectedDropDownListItem.html(dropDownListItem.html());

        var result = input.val(value);

        input.trigger("change");
        dropDownList.trigger("change");
        return result;
      }

      return $val.call(this, value);
    };
  }

  function defineHandlers() {
    $(document.body).on("click", onGlobalClick);
    $(document.body).on("click", ".drop-down-list__item--selected", onSelectedDropDownListItemClick);
    $(document.body).on("click", ".drop-down-list__item:not(.drop-down-list__item--selected)", onDropDownListItemClick);
  }

  function onGlobalClick() {
    $(".drop-down-list").removeClass("drop-down-list--expanded");
    return true;
  }

  function onSelectedDropDownListItemClick() {
    $(this).closest(".drop-down-list").toggleClass("drop-down-list--expanded");
    return false;
  }

  function onDropDownListItemClick() {
    var dropDownListItem = $(this),
      dropDownList = dropDownListItem.closest(".drop-down-list");

    dropDownList.removeClass("drop-down-list--expanded").val(dropDownListItem.data("value"));
    return false;
  }
})(window.platformus = window.platformus || {});