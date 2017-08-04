// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.dropDownList = {};
  platformus.dataSourceParameterEditors.dropDownList.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.dataSourceParameterEditors.base.createLabel(dataSourceParameter).appendTo(field);
    createDropDownList(dataSourceParameter).appendTo(field);
    return field;
  }

  function createDropDownList(dataSourceParameter) {
    var identity = "dataSourceParameter" + dataSourceParameter.code;
    var dropDownList = $("<div>").addClass("drop-down-list field__drop-down-list").attr("id", identity).change(platformus.dataSourceParameterEditors.base.dataSourceParameterChanged);

    if (dataSourceParameter.isRequired) {
      dropDownList.addClass("drop-down-list--required").attr("data-val", true).attr("data-val-required", true);
    }

    createSelectedDropDownListItem(dataSourceParameter).appendTo(dropDownList);
    createDropDownListItems(dataSourceParameter).appendTo(dropDownList);
    createInput(dataSourceParameter).appendTo(dropDownList);
    return dropDownList;
  }

  function createSelectedDropDownListItem(dataSourceParameter) {
    var selectedOption = getSelectedOption(dataSourceParameter);

    return $("<a>").addClass("drop-down-list__item drop-down-list__item--selected").attr("href", "#").html(selectedOption == null ? "Not selected" : selectedOption.text);
  }

  function createDropDownListItems(dataSourceParameter) {
    var dropDownListItems = $("<div>").addClass("drop-down-list__items");

    for (var i = 0; i != dataSourceParameter.options.length; i++) {
      createDropDownListItem(dataSourceParameter.options[i]).appendTo(dropDownListItems);
    }

    return dropDownListItems;
  }

  function createDropDownListItem(option) {
    return $("<a>").addClass("drop-down-list__item").attr("href", "#").attr("data-value", option.value).html(option.text);
  }

  function getSelectedOption(dataSourceParameter) {
    var value = platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter);

    for (var i = 0; i != dataSourceParameter.options.length; i++) {
      if (dataSourceParameter.options[i].value == value) {
        return dataSourceParameter.options[i];
      }
    }

    return null;
  }

  function createInput(dataSourceParameter) {
    return $("<input>")
      .attr("type", "hidden")
      .attr("value", platformus.dataSourceParameterEditors.base.dataSourceParameterValue(dataSourceParameter))
      .attr("data-datasource-parameter-code", dataSourceParameter.code)
  }
})(window.platformus = window.platformus || {});