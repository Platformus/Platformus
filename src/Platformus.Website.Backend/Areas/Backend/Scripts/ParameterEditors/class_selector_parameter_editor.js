// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditor = platformus.parameterEditor || [];
  platformus.parameterEditor.classSelector = {};
  platformus.parameterEditor.classSelector.showClassSelectorForm = function (code) {
    var valueElement = $("#parameter" + code);

    return platformus.forms.classSelectorForm.show(
      valueElement.val(),
      function (classId) {
        valueElement.val(classId).trigger("change");
        platformus.parameterEditor.classSelector.sync(code);
      }
    );
  };

  platformus.parameterEditor.classSelector.sync = function (code) {
    var identity = "parameter" + code;
    var value = $("#" + identity).val();

    if (!value) return;

    $.get(
      "/backend/website/class/" + value,
      function ($class) {
        $("#" + identity + "Keys").empty().append($("<div>").addClass("item-selector__key").html($class.name));
      }
    );
  };
})(window.platformus = window.platformus || {});