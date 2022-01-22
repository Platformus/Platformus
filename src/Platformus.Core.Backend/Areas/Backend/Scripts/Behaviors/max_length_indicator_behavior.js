// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    $(document.body).on("focus", "input[maxlength], textarea[maxlength]", onControlFocus);
    $(document.body).on("blur", "input[maxlength], textarea[maxlength]", onControlBlur);
  }

  function onControlFocus() {
    var control = $(this);
    var maxLengthIndicator = createMaxLengthIndicator(control);

    positionMaxLengthIndicator(maxLengthIndicator, control);
    showMaxLengthIndicator(maxLengthIndicator);
    bindMaxLengthIndicatorToControl(maxLengthIndicator, control);
  }

  function onControlBlur() {
    var control = $(this);
    var maxLengthIndicator = getMaxLengthIndicator();

    hideAndRemoveMaxLengthIndicator(maxLengthIndicator);
    unbindMaxLengthIndicatorFromControl(control);
  }

  function createMaxLengthIndicator(control) {
    var maxLengthIndicator = $("<div>")
      .addClass("max-length-indicator")
      .html(getRemainingLength(control).toString())
      .appendTo($(document.body));

    return maxLengthIndicator;
  }

  function positionMaxLengthIndicator(maxLengthIndicator, control) {
    maxLengthIndicator.css(
      {
        left: control.offset().left + control.outerWidth() - maxLengthIndicator.outerWidth() - 1,
        top: control.offset().top + 1
      }
    );
  }

  function showMaxLengthIndicator(maxLengthIndicator) {
    maxLengthIndicator.fadeIn("fast");
  }

  function bindMaxLengthIndicatorToControl(maxLengthIndicator, control) {
    control.bind(
      "input keyup paste propertychange",
      function () {
        var remainingLength = getRemainingLength(control);

        if (remainingLength <= 10) {
          maxLengthIndicator.addClass("max-length-indicator--highlighted");
        }

        else {
          maxLengthIndicator.removeClass("max-length-indicator--highlighted");
        }

        maxLengthIndicator.html(remainingLength.toString());
        positionMaxLengthIndicator(maxLengthIndicator, control);
      }
    );
  }

  function hideAndRemoveMaxLengthIndicator(maxLengthIndicator) {
    maxLengthIndicator.fadeOut(
      "fast",
      function () {
        maxLengthIndicator.remove();
      }
    );
  }

  function unbindMaxLengthIndicatorFromControl(control) {
    control.unbind("input keyup paste propertychange");
  }

  function getRemainingLength(control) {
    return parseInt(control.attr("maxlength")) - control.val().length;
  }

  function getMaxLengthIndicator() {
    return $(".max-length-indicator");
  }
})(window.platformus = window.platformus || {});