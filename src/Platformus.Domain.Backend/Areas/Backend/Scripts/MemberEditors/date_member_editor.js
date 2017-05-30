// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.date = {};
  platformus.memberEditors.date.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("date-editor").addClass("form__field").addClass("field");

    platformus.memberEditors.base.createLabel(member).appendTo(field);
    createTextBox(member).appendTo(field);
    return field;
  }

  function createTextBox(member) {
    var identity = platformus.memberEditors.base.getIdentity(member);
    var textBox = $("<input>").addClass("field__text-box");

    return textBox
      .addClass("text-box")
      .attr("id", identity)
      .attr("name", identity)
      .attr("type", "text")
      .attr("autocomplete", "off")
      .attr("placeholder", moment().locale(platformus.culture.server()).localeData().longDateFormat("L"))
      .attr("value", member.property.dateTimeValue)
      .attr("data-type", "date");
  }
})(window.platformus = window.platformus || {});