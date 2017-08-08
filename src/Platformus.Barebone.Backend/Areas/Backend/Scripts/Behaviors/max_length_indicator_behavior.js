// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    $(document.body).on("focus", "input[maxlength], textarea[maxlength]", controlFocusHandler);
    $(document.body).on("blur", "input[maxlength], textarea[maxlength]", controlBlurHandler);
  }

  function controlFocusHandler() {
    var control = $(this);
    var maxLengthIndicator = createMaxLengthIndicator(control);

    positionMaxLengthIndicator(maxLengthIndicator, control);
    showMaxLengthIndicator(maxLengthIndicator);
    bindMaxLengthIndicatorToControl(maxLengthIndicator, control);
  }

  function controlBlurHandler() {
    var control = $(this);
    var maxLengthIndicator = getMaxLengthIndicator();

    hideAndRemoveMaxLengthIndicator(maxLengthIndicator);
    unbindMaxLengthIndicatorFromControl(control);
  }

  function createMaxLengthIndicator(control) {
    var maxLengthIndicator = $("<div>").addClass("max-length-indicator").appendTo($(document.body));

    $("<div>").addClass("max-length-indicator__arrow").appendTo(maxLengthIndicator);
    $("<div>").addClass("max-length-indicator__remaining-length").html(getRemainingLength(control).toString()).appendTo(maxLengthIndicator);
    return maxLengthIndicator;
  }

  function positionMaxLengthIndicator(maxLengthIndicator, control) {
    maxLengthIndicator.css(
      {
        left: control.offset().left + control.outerWidth() + 20,
        top: control.offset().top
      }
    );
  }

  function showMaxLengthIndicator(maxLengthIndicator) {
    maxLengthIndicator.fadeIn("fast");
  }

  function bindMaxLengthIndicatorToControl(maxLengthIndicator, control) {
    var remainingLength = maxLengthIndicator.find(".max-length-indicator__remaining-length");

    control.bind(
      "input keyup paste propertychange",
      function () {
        remainingLength.html(getRemainingLength(control).toString());
      }
    );
  }

  function getRemainingLength(control) {
    return parseInt(control.attr("maxlength")) - control.val().length;
  }

  function getMaxLengthIndicator() {
    return $(".max-length-indicator");
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
})(window.platformus = window.platformus || {});