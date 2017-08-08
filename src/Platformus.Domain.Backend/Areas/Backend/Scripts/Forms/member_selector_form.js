// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  var _callback = null;

  platformus.forms = platformus.forms || {};
  platformus.forms.memberSelectorForm = {};
  platformus.forms.memberSelectorForm.show = function (memberId, callback) {
    _callback = callback;
    return platformus.forms.baseForm.show(
      "/backend/domain/memberselectorform?memberid=" + memberId,
      defineHandlers,
      "member-selector-pop-up-form"
    );
  };

  platformus.forms.memberSelectorForm.select = function () {
    if (_callback != null) {
      _callback(getSelectedMemberId());
    }

    return platformus.forms.baseForm.hideAndRemove();
  };

  platformus.forms.memberSelectorForm.hideAndRemove = function () {
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

  function getSelectedMemberId() {
    var selectedRow = platformus.forms.activeForm.find(".table__row--selected");

    if(selectedRow.length == 0) {
      return null;
    }

    return selectedRow.data("memberId");
  }
})(window.platformus = window.platformus || {});