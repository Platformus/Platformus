// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.calendar = {};
  platformus.calendar.create = function (descriptor) {
    return createCalendar(descriptor);
  };

  function createCalendar(descriptor) {
    var calendar = $("<div>").addClass("calendar");

    createHeader(descriptor).appendTo(calendar);
    createMonth(descriptor).appendTo(calendar);
    return calendar;
  }

  function createHeader(descriptor) {
    var header = $("<div>").addClass("calendar__header").html(descriptor.month.format("MMMM YYYY"));
    $("<div>").addClass("calendar__previous-month").appendTo(header).click(
      function () {
        onPreviousMonthClick($(this).closest(".calendar"), descriptor);
        return false;
      }
    );

    $("<div>").addClass("calendar__next-month").appendTo(header).click(
      function () {
        onNextMonthClick($(this).closest(".calendar"), descriptor);
        return false;
      }
    );

    return header;
  }

  function createMonth(descriptor) {
    var month = $("<div>").addClass("calendar__month");
    var week = $("<div>").addClass("calendar__week").appendTo(month);

    for (var i = 0; i != 7; i++) {
      $("<div>").addClass("calendar__day").html(moment().day(i + 1).format("dd")).appendTo(week);
    }

    var offset = descriptor.month.isoWeekday() - 1;
    var date = descriptor.month.clone().subtract(offset, "days");

    for (var i = 0; i != 6; i++) {
      week = $("<div>").addClass("calendar__week").appendTo(month);

      for (var j = 0; j != 7; j++) {
        var day = $("<a>").addClass("calendar__day").attr("href", "#").html(date.format("DD")).appendTo(week).click(
          function (selectedDate) {
            return function () {
              if (descriptor.onDateSelect) {
                descriptor.onDateSelect(selectedDate);
              }

              return false;
            };
          }(moment(date.toISOString()))
        );

        if (date.month() != descriptor.month.month()) {
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

  function onPreviousMonthClick(calendar, descriptor) {
    descriptor.month = moment(descriptor.month.subtract(1, "months").toISOString());
    changeMonth(calendar, descriptor);
  }

  function onNextMonthClick(calendar, descriptor) {
    descriptor.month = moment(descriptor.month.add(1, "months").toISOString());
    changeMonth(calendar, descriptor);
  }

  function changeMonth(calendar, descriptor) {
    calendar.empty().append(createCalendar(descriptor).children());

    if (descriptor.onMonthChange) {
      descriptor.onMonthChange(descriptor.month);
    }
  }
})(window.platformus = window.platformus || {});