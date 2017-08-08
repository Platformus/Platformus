// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    $(document.body).on("focus click", "input[data-type='date']", textBoxFocusHandler);
    $(document.body).on("blur", "input[data-type='date']", textBoxBlurHandler);
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
    var picker = $("<div>").addClass("date-picker").addClass("picker").attr("data-text-box-id", textBox.attr("id")).appendTo($(document.body));

    $("<div>").addClass("picker__arrow").appendTo(picker);

    var today = moment();
    var thisMonth = moment(new Date(today.year(), today.month(), 1)).locale(platformus.culture.server());

    createCalendar(thisMonth, textBox).appendTo(picker);
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

  function createCalendar(thisMonth, textBox) {
    var calendar = $("<div>").addClass("date-picker__calendar").addClass("calendar");

    createHeader(thisMonth, textBox).appendTo(calendar);
    createMonth(thisMonth, textBox).appendTo(calendar);
    return calendar;
  }

  function createHeader(thisMonth, textBox) {
    var header = $("<div>").addClass("calendar__header").html(thisMonth.format("MMMM YYYY"));
    var previousMonth = $("<div>").addClass("calendar__previous-month").appendTo(header).click(
      function () {
        getPicker().find(".calendar").replaceWith(createCalendar(thisMonth.subtract(1, "months"), textBox));
        textBox.focus();
        return false;
      }
    );

    var nextMonth = $("<div>").addClass("calendar__next-month").appendTo(header).click(
      function () {
        getPicker().find(".calendar").replaceWith(createCalendar(thisMonth.add(1, "months"), textBox));
        textBox.focus();
        return false;
      }
    );

    return header;
  }

  function createMonth(thisMonth, textBox) {
    var month = $("<div>").addClass("calendar__month");
    var week = $("<div>").addClass("calendar__week").appendTo(month);

    for (var i = 0; i != 7; i++) {
      $("<div>").addClass("calendar__day").html(moment().day(i + 1).format("dd")).appendTo(week);
    }

    var offset = thisMonth.isoWeekday() - 1;
    var date = thisMonth.clone().subtract(offset, "days");

    for (var i = 0; i != 6; i++) {
      week = $("<div>").addClass("calendar__week").appendTo(month);

      for (var j = 0; j != 7; j++) {
        var day = $("<a>").addClass("calendar__day").attr("href", "#").html(date.format("DD")).appendTo(week).click(
          function (formattedDate) {
            return function () {
              textBox.val(formattedDate);
              textBox.focus();
              hideAndRemovePicker(getPicker());
              return false;
            };
          }(date.locale(platformus.culture.server()).format("L"))
        );

        if (date.month() != thisMonth.month()) {
          day.addClass("calendar__day--outer");
        }

        else if (date.month() == moment().month() && date.date() == moment().date()) {
          day.addClass("calendar__day--today");
        }

        date.add(1, "days");
      }
    }

    return month;
  }

  function getPicker() {
    return $(".picker");
  }

  function hideAndRemovePicker(picker) {
    picker.fadeOut(
      "fast",
      function () {
        picker.remove();
      }
    );
  }
})(window.platformus = window.platformus || {});