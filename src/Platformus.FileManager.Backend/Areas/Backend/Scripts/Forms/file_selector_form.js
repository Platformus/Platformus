// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  var _callback = null;

  platformus.forms = platformus.forms || {};
  platformus.forms.fileSelectorForm = {};
  platformus.forms.fileSelectorForm.show = function (callback) {
    _callback = callback;

    return platformus.forms.baseForm.show("/backend/filemanager/fileselectorform", defineHandlers, "file-selector-pop-up-form");
  };

  platformus.forms.fileSelectorForm.select = function () {
    var filename = getSelectedFilename();

    if (!platformus.string.isNullOrEmpty(filename) && _callback != null) {
      _callback(filename);
    }

    return platformus.forms.baseForm.hideAndRemove();
  };

  platformus.forms.fileSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseForm.hideAndRemove();
  };

  function defineHandlers() {
    platformus.forms.activeForm.find(".table__row").bind("click", rowClickHandler);
  }

  function rowClickHandler() {
    if ($(this).find(".table__cell--header").length != 0) {
      return;
    }

    platformus.forms.activeForm.find(".table__row").removeClass("table__row--selected");
    $(this).addClass("table__row--selected");
  }

  function getSelectedFilename() {
    var selectedRow = platformus.forms.activeForm.find(".table__row--selected");

    if(selectedRow.length == 0) {
      return null;
    }

    return selectedRow.data("fileName");
  }
})(window.platformus = window.platformus || {});