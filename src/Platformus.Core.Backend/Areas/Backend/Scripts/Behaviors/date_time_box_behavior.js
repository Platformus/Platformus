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
    $(document.body).on("focus click", "input[data-type='date-time']", onDateTimePickerFocus);
    $(document.body).on("blur", "input[data-type='date-time']", onDateTimePickerBlur);
  }

  function performInitialActions() {
    var format = platformus.globalization.getDateTimeFormat(true);

    $("input[data-type='date-time']")
      .addClass("date-time-picker")
      .attr("autocomplete", "off")
      .attr("placeholder", format)
      .mask(format);
  }

  function onDateTimePickerFocus() {
    var dateTimePicker = $(this);
    var calendar = getCalendar();
    
    if (calendar.length && calendar.data("dateTimePickerId") == dateTimePicker.attr("id")) {
      return;
    }

    dateTimePicker.addClass("date-time-picker--expanded");
    calendar = createCalendar(dateTimePicker);
    positionCalendar(calendar, dateTimePicker);
    showCalendar(calendar);
  }

  function onDateTimePickerBlur() {
    var dateTimePicker = $(this);
    var calendar = getCalendar();

    if (calendar.is(":hover")) {
      return;
    }

    dateTimePicker.removeClass("date-time-picker--expanded");
    hideAndRemoveCalendar(calendar);
  }

  function createCalendar(dateTimePicker) {
    var today = dateTimePicker.val() ? moment(dateTimePicker.val(), platformus.globalization.getDateTimeFormat()) : moment();

    if (!today.isValid()) {
      today = moment();
    }

    var month = moment(new Date(today.year(), today.month(), 1));
    var calendar = platformus.calendar.create(
      {
        month: month,
        onMonthChange: function (month) {
          dateTimePicker.focus();
        },
        onDateSelect: function (date) {
          setDateTime(dateTimePicker, date);
        }
      }
    ).addClass("date-time-picker__calendar")
      .attr("data-date-time-picker-id", dateTimePicker.attr("id"))
      .appendTo($(document.body));
    return calendar;
  }

  function positionCalendar(calendar, dateTimePicker) {
    calendar.css(
      { left: dateTimePicker.offset().left, top: dateTimePicker.offset().top + dateTimePicker.outerHeight() - 1 }
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

  function setDateTime(dateTimePicker, date) {
    dateTimePicker.val(date.format(platformus.globalization.getDateTimeFormat()));
    dateTimePicker.focus();
    hideAndRemoveCalendar(getCalendar());
    dateTimePicker.change();
  }
})(window.platformus = window.platformus || {});