// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.microcontrollerParameterEditors = platformus.microcontrollerParameterEditors || [];
  platformus.microcontrollerParameterEditors.dropDownList = {};
  platformus.microcontrollerParameterEditors.dropDownList.create = function (container, microcontrollerParameter) {
    createField(microcontrollerParameter).appendTo(container);
  };

  function createField(microcontrollerParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.microcontrollerParameterEditors.base.createLabel(microcontrollerParameter).appendTo(field);
    createDropDownList(microcontrollerParameter).appendTo(field);
    return field;
  }

  function createDropDownList(microcontrollerParameter) {
    var identity = "microcontrollerParameter" + microcontrollerParameter.code;
    var dropDownList = $("<div>").addClass("drop-down-list field__drop-down-list").attr("id", identity).change(platformus.microcontrollerParameterEditors.base.microcontrollerParameterChanged);

    if (microcontrollerParameter.isRequired) {
      dropDownList.addClass("drop-down-list--required").attr("data-val", true).attr("data-val-required", true);
    }

    createSelectedDropDownListItem(microcontrollerParameter).appendTo(dropDownList);
    createDropDownListItems(microcontrollerParameter).appendTo(dropDownList);
    createInput(microcontrollerParameter).appendTo(dropDownList);
    return dropDownList;
  }

  function createSelectedDropDownListItem(microcontrollerParameter) {
    var selectedOption = getSelectedOption(microcontrollerParameter);

    return $("<a>").addClass("drop-down-list__item drop-down-list__item--selected").attr("href", "#").html(selectedOption == null ? "Not selected" : selectedOption.text);
  }

  function createDropDownListItems(microcontrollerParameter) {
    var dropDownListItems = $("<div>").addClass("drop-down-list__items");

    for (var i = 0; i != microcontrollerParameter.options.length; i++) {
      createDropDownListItem(microcontrollerParameter.options[i]).appendTo(dropDownListItems);
    }

    return dropDownListItems;
  }

  function createDropDownListItem(option) {
    return $("<a>").addClass("drop-down-list__item").attr("href", "#").attr("data-value", option.value).html(option.text);
  }

  function getSelectedOption(microcontrollerParameter) {
    var value = platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter);

    for (var i = 0; i != microcontrollerParameter.options.length; i++) {
      if (microcontrollerParameter.options[i].value == value) {
        return microcontrollerParameter.options[i];
      }
    }

    return null;
  }

  function createInput(microcontrollerParameter) {
    return $("<input>")
      .attr("type", "hidden")
      .attr("value", platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue(microcontrollerParameter))
      .attr("data-datasource-parameter-code", microcontrollerParameter.code)
  }
})(window.platformus = window.platformus || {});