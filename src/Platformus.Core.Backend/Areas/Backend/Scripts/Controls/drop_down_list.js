// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.dropDownList = {};
  platformus.controls.dropDownList.create = function (descriptor) {
    var dropDownList = $("<div>").addClass("field__drop-down-list drop-down-list").attr("id", descriptor.identity);

    if (descriptor.validation && descriptor.validation.isRequired) {
      dropDownList.addClass("drop-down-list--required").attr("data-val", true).attr("data-val-required", true);
    }

    createSelectedDropDownListItem(descriptor).appendTo(dropDownList);
    createDropDownListItems(descriptor).appendTo(dropDownList);
    createInput(descriptor);
    return dropDownList;
  };

  function createSelectedDropDownListItem(descriptor) {
    var selectedDropDownListItem = getSelectedDropDownListItem(descriptor);

    return createDropDownListItem(selectedDropDownListItem, true);
  }

  function createDropDownListItems(descriptor) {
    var dropDownListItems = $("<div>").addClass("drop-down-list__items");

    descriptor.options.forEach(o => createDropDownListItem(o).appendTo(dropDownListItems));
    return dropDownListItems;
  }

  function createDropDownListItem(option, isSelected) {
    var dropDownListItem = $("<a>").addClass("drop-down-list__item").attr("href", "#");

    if (isSelected) {
      dropDownListItem.addClass("drop-down-list__item--selected");
    }

    if (option) {
      dropDownListItem.attr("data-value", option.value).html(option.text);
    }

    else {
      dropDownListItem.html("&nbsp;");
    }

    return dropDownListItem;
  }

  function createInput(descriptor) {
    return $("<input>")
      .attr("name", descriptor.identity)
      .attr("type", "hidden")
      .attr("value", descriptor.value);
  }

  function getSelectedDropDownListItem(descriptor) {
    return descriptor.options.find(o => o.value == descriptor.value);
  }
})(window.platformus = window.platformus || {});