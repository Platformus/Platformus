// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  var _minItemNumber = null;
  var _maxItemNumber = null;
  var _callback = null;

  platformus.forms = platformus.forms || {};
  platformus.forms.baseItemSelectorForm = {};
  platformus.forms.baseItemSelectorForm.show = function (url, minItemNumber, maxItemNumber, callback) {
    _minItemNumber = minItemNumber;
    _maxItemNumber = maxItemNumber;
    _callback = callback;
    return platformus.forms.baseForm.show(url, defineHandlers, "item-selector-pop-up-form");
  };

  platformus.forms.baseItemSelectorForm.select = function () {
    var selectedItemNumber = getSelectedItemNumber();

    if (_minItemNumber != null && _minItemNumber > selectedItemNumber) {
      return;
    }

    if (_maxItemNumber != null && _maxItemNumber < selectedItemNumber) {
      return;
    }

    if (_callback != null) {
      _callback(getSelectedItemValues());
    }

    return platformus.forms.baseForm.hideAndRemove();
  };

  platformus.forms.baseItemSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseForm.hideAndRemove();
  };

  function defineHandlers() {
    platformus.forms.activeForm.find(".table__row").bind("click", rowClickHandler);
  }

  function rowClickHandler() {
    if ($(this).find(".table__cell--header").length != 0) {
      return;
    }

    var selectedItemNumber = getSelectedItemNumber();
    var row = $(this);

    if (row.hasClass("table__row--selected")) {
      row.removeClass("table__row--selected");
    }

    else {
      if (_maxItemNumber != null && _maxItemNumber <= selectedItemNumber) {
        if (_maxItemNumber == 1) {
          platformus.forms.activeForm.find(".table__row--selected").removeClass("table__row--selected");
        }

        else {
          return;
        }
      }

      row.addClass("table__row--selected");
    }
  }

  function getSelectedItemNumber() {
    return platformus.forms.activeForm.find(".table__row--selected").length;
  }

  function getSelectedItemValues() {
    var selectedItemValues = platformus.string.empty;

    platformus.forms.activeForm.find(".table__row--selected").each(
      function (index, element) {
        if (!platformus.string.isNullOrEmpty(selectedItemValues)) {
          selectedItemValues += ",";
        }

        selectedItemValues += $(element).data("itemValue");
      }
    );

    return selectedItemValues;
  }
})(window.platformus = window.platformus || {});