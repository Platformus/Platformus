// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
      performInitialActions();
    }
  );

  function defineHandlers() {
    $(document.body).on("focus click", "input[data-type='date-time']", textBoxFocusHandler);
    $(document.body).on("blur", "input[data-type='date-time']", textBoxBlurHandler);
  }

  function performInitialActions() {
    var format = platformus.globalization.getDateTimeFormat(true);

    $("input[data-type='date-time']")
      .attr("autocomplete", "off")
      .attr("placeholder", format)
      .mask(format);
  }

  function textBoxFocusHandler() {
    var picker = getPicker();
    var textBox = $(this);

    if (picker.length != 0 && picker.data("textBoxId") == textBox.attr("id")) {
      return;
    }

    picker = createPicker(textBox);
    positionPicker(picker, textBox);
    showPicker(picker);
  }

  function textBoxBlurHandler() {
    var picker = getPicker();

    try {
      if (picker.is(":hover")) {
        return;
      }
    }

    catch (e) {}

    hideAndRemovePicker(picker);
  }

  function createPicker(textBox) {
    var picker = $("<div>").addClass("date-time-picker").addClass("picker").attr("data-text-box-id", textBox.attr("id")).appendTo($(document.body));

    $("<div>").addClass("picker__arrow").appendTo(picker);

    var today = textBox.val() ? moment(textBox.val(), platformus.globalization.getDateTimeFormat()) : moment();

    if (!today.isValid()) {
      today = moment();
    }

    var month = moment(new Date(today.year(), today.month(), 1));

    platformus.controls.calendar.create(
      {
        month: month,
        onMonthChange: function (month) {
          textBox.focus();
        },
        onDateSelect: function (date) {
          setDateTime(textBox, date);
        }
      }
    ).addClass("date-time-picker__calendar").appendTo(picker);
    return picker;
  }

  function positionPicker(picker, textBox) {
    picker.css(
      { left: textBox.offset().left + textBox.outerWidth() + 20, top: textBox.offset().top }
    )
  }

  function showPicker(picker) {
    picker.fadeIn("fast");
  }

  function hideAndRemovePicker(picker) {
    picker.fadeOut(
      "fast",
      function () {
        picker.remove();
      }
    );
  }

  function setDateTime(textBox, date) {
    textBox.val(date.format(platformus.globalization.getDateTimeFormat()));
    textBox.focus();
    hideAndRemovePicker(getPicker());
    textBox.change();
  }

  function getPicker() {
    return $(".picker");
  }
})(window.platformus = window.platformus || {});