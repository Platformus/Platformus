// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.dropDownList = {};
  platformus.endpointParameterEditors.dropDownList.create = function (container, endpointParameter) {
    createField(endpointParameter).appendTo(container);
  };

  function createField(endpointParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.endpointParameterEditors.base.createLabel(endpointParameter).appendTo(field);
    createDropDownList(endpointParameter).appendTo(field);
    return field;
  }

  function createDropDownList(endpointParameter) {
    var identity = "endpointParameter" + endpointParameter.code;
    var dropDownList = $("<div>").addClass("drop-down-list field__drop-down-list").attr("id", identity).change(platformus.endpointParameterEditors.base.endpointParameterChanged);

    if (endpointParameter.isRequired) {
      dropDownList.addClass("drop-down-list--required").attr("data-val", true).attr("data-val-required", true);
    }

    createSelectedDropDownListItem(endpointParameter).appendTo(dropDownList);
    createDropDownListItems(endpointParameter).appendTo(dropDownList);
    createInput(endpointParameter).appendTo(dropDownList);
    return dropDownList;
  }

  function createSelectedDropDownListItem(endpointParameter) {
    var selectedOption = getSelectedOption(endpointParameter);

    return $("<a>").addClass("drop-down-list__item drop-down-list__item--selected").attr("href", "#").html(selectedOption == null ? "Not selected" : selectedOption.text);
  }

  function createDropDownListItems(endpointParameter) {
    var dropDownListItems = $("<div>").addClass("drop-down-list__items");

    for (var i = 0; i != endpointParameter.options.length; i++) {
      createDropDownListItem(endpointParameter.options[i]).appendTo(dropDownListItems);
    }

    return dropDownListItems;
  }

  function createDropDownListItem(option) {
    return $("<a>").addClass("drop-down-list__item").attr("href", "#").attr("data-value", option.value).html(option.text);
  }

  function getSelectedOption(endpointParameter) {
    var value = platformus.endpointParameterEditors.base.endpointParameterValue(endpointParameter);

    for (var i = 0; i != endpointParameter.options.length; i++) {
      if (endpointParameter.options[i].value == value) {
        return endpointParameter.options[i];
      }
    }

    return null;
  }

  function createInput(endpointParameter) {
    return $("<input>")
      .attr("type", "hidden")
      .attr("value", platformus.endpointParameterEditors.base.endpointParameterValue(endpointParameter))
      .attr("data-datasource-parameter-code", endpointParameter.code)
  }
})(window.platformus = window.platformus || {});