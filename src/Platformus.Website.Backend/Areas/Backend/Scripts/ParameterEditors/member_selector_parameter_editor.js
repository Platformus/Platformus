// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditor = platformus.parameterEditor || [];
  platformus.parameterEditor.memberSelector = {};
  platformus.parameterEditor.memberSelector.showMemberSelectorForm = function (code) {
    var valueElement = $("#parameter" + code);

    return platformus.forms.memberSelectorForm.show(
      valueElement.val(),
      function (memberId) {
        valueElement.val(memberId).trigger("change");
        platformus.parameterEditor.memberSelector.sync(code);
      }
    );
  };

  platformus.parameterEditor.memberSelector.sync = function (code) {
    var identity = "parameter" + code;
    var value = $("#" + identity).val();

    if (!value) return;

    $.get(
      "/backend/website/member/" + value,
      function (member) {
        $("#" + identity + "Keys").empty().append($("<div>").addClass("item-selector__key").html(member.class.name + "." + member.name));
      }
    );
  };
})(window.platformus = window.platformus || {});