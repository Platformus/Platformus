// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  var _callback = null;

  platformus.forms = platformus.forms || {};
  platformus.forms.objectSelectorForm = {};
  platformus.forms.objectSelectorForm.show = function (relationClassId, objectIds, callback) {
    _callback = callback;

    return platformus.forms.baseForm.show(
      "/backend/content/objectselectorform?classid=" + relationClassId + "&objectids=" + objectIds,
      defineHandlers,
      "object-selector-pop-up-form"
    );
  };

  platformus.forms.objectSelectorForm.select = function () {
    if (_callback != null) {
      _callback(getSelectedObjectIds());
    }

    return platformus.forms.baseForm.hideAndRemove();
  };

  platformus.forms.objectSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseForm.hideAndRemove();
  };

  function defineHandlers() {
    platformus.forms.activeForm.find(".table__row").bind("click", rowClickHandler);
  }

  function rowClickHandler() {
    if ($(this).find(".table__cell--header").length != 0) {
      return;
    }

    $(this).toggleClass("table__row--selected");
  }

  function getSelectedObjectIds() {
    var objectIds = platformus.string.empty;

    platformus.forms.activeForm.find(".table__row--selected").each(
      function (index, element) {
        if (!platformus.string.isNullOrEmpty(objectIds)) {
          objectIds += ",";
        }

        objectIds += $(element).data("objectId");
      }
    );

    return objectIds;
  }
})(window.platformus = window.platformus || {});