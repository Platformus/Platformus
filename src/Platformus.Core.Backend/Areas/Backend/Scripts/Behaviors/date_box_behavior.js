// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
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
    $(document.body).on("focus click", "input[data-type='date']", onDatePickerFocus);
    $(document.body).on("blur", "input[data-type='date']", onDatePickerBlur);
  }

  function performInitialActions() {
    var format = platformus.globalization.getDateFormat(true);
    
    $("input[data-type='date']")
      .addClass("date-picker")
      .attr("autocomplete", "off")
      .attr("placeholder", format)
      .mask(format);
  }

  function onDatePickerFocus() {
    var datePicker = $(this);
    var calendar = getCalendar();

    if (calendar.length && calendar.data("datePickerId") == datePicker.attr("id")) {
      return;
    }

    datePicker.addClass("date-picker--expanded");
    calendar = createCalendar(datePicker);
    positionCalendar(calendar, datePicker);
    showCalendar(calendar);
  }

  function onDatePickerBlur() {
    var datePicker = $(this);
    var calendar = getCalendar();

    if (calendar.is(":hover")) {
      return;
    }

    datePicker.removeClass("date-picker--expanded");
    hideAndRemoveCalendar(calendar);
  }

  function createCalendar(datePicker) {
    var today = datePicker.val() ? moment(datePicker.val(), platformus.globalization.getDateFormat()) : moment();

    if (!today.isValid()) {
      today = moment();
    }

    var month = moment(new Date(today.year(), today.month(), 1));
    var calendar = platformus.calendar.create(
      {
        month: month,
        onMonthChange: function (month) {
          datePicker.focus();
        },
        onDateSelect: function (date) {
          setDate(datePicker, date);
        }
      }
    ).addClass("date-picker__calendar")
      .attr("data-date-picker-id", datePicker.attr("id"))
      .appendTo($(document.body));
    return calendar;
  }

  function positionCalendar(calendar, datePicker) {
    calendar.css(
      { left: datePicker.offset().left, top: datePicker.offset().top + datePicker.outerHeight() - 1 }
    )
  }

  function showCalendar(calendar) {
    calendar.fadeIn("fast");
  }

  function hideAndRemoveCalendar(calendar) {
    calendar.fadeOut(
      "fast",
      function () {
        calendar.remove();
      }
    );
  }

  function getCalendar() {
    return $(".calendar");
  }

  function setDate(datePicker, date) {
    datePicker.val(date.format(platformus.globalization.getDateFormat()));
    datePicker.focus();
    hideAndRemoveCalendar(getCalendar());
    datePicker.change();
  }
})(window.platformus = window.platformus || {});