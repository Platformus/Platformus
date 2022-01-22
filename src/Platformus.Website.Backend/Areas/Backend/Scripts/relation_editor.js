// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.relationEditor = platformus.relationEditor || [];
  platformus.relationEditor.showObjectSelectorForm = function (memberId, classId, minRelatedObjectsNumber, maxRelatedObjectsNumber) {
    var valueElement = $("#relationMember" + memberId);

    return platformus.forms.objectSelectorForm.show(
      classId, valueElement.val(), minRelatedObjectsNumber, maxRelatedObjectsNumber,
      function (objectIds) {
        valueElement.val(objectIds).trigger("change");
        platformus.relationEditor.sync(memberId);
      }
    );
  };

  platformus.relationEditor.sync = function (memberId) {
    var identity = "relationMember" + memberId;
    var value = $("#" + identity).val();

    if (!value) return;

    $.get(
      "/backend/website/displayableobjects?ids=" + value,
      function (objects) {
        var displayableValues = $("#" + identity + "DisplayableValues");

        displayableValues.empty();
        objects.forEach(function (object) {
          $("<div>")
            .addClass("item-selector__key")
            .html(Object.values(object).join(" "))
            .appendTo(displayableValues);
        });
      }
    );
  };
})(window.platformus = window.platformus || {});