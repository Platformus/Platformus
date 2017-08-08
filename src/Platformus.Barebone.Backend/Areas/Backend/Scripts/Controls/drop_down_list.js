// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.dropDownList = {};
  platformus.controls.dropDownList.create = function (descriptor) {
    var dropDownList = $("<div>").addClass("field__drop-down-list drop-down-list").attr("id", descriptor.identity);

    if (descriptor.validation != null && descriptor.validation.isRequired) {
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

    for (var i = 0; i != descriptor.options.length; i++) {
      createDropDownListItem(descriptor.options[i]).appendTo(dropDownListItems);
    }

    return dropDownListItems;
  }

  function createDropDownListItem(option, isSelected) {
    var dropDownListItem = $("<a>").addClass("drop-down-list__item").attr("href", "#");

    if (isSelected) {
      dropDownListItem.addClass("drop-down-list__item--selected");
    }

    if (option == null) {
      dropDownListItem.html("Not selected");
    }

    else {
      dropDownListItem.attr("data-value", option.value).html(option.text);
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
    for (var i = 0; i != descriptor.options.length; i++) {
      if (descriptor.options[i].value == descriptor.value) {
        return descriptor.options[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});