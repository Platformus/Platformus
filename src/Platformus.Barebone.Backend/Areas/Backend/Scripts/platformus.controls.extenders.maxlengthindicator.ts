/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Controls.Extenders.MaxLengthIndicator {
  export function apply(): void {
    $(document.body).on("focus", "input[maxlength], textarea[maxlength]", controlFocusHandler);
    $(document.body).on("blur", "input[maxlength], textarea[maxlength]", controlBlurHandler);
  }

  function controlFocusHandler(): void {
    var input = $(this);
    var maxLengthIndicator = createMaxLengthIndicator(input);

    positionMaxLengthIndicator(maxLengthIndicator, input);
    showMaxLengthIndicator(maxLengthIndicator);
    bindMaxLengthIndicatorToInput(maxLengthIndicator, input);
  }

  function controlBlurHandler(): void {
    var input = $(this);
    var maxLengthIndicator = getMaxLengthIndicator();

    hideAndRemoveMaxLengthIndicator(maxLengthIndicator);
    unbindMaxLengthIndicatorFromInput(input);
  }

  function createMaxLengthIndicator(input: JQuery): JQuery {
    var maxLengthIndicator = $("<div>").addClass("max-length-indicator").appendTo($(document.body));

    $("<div>").addClass("arrow").appendTo(maxLengthIndicator);
    $("<div>").addClass("remaining-length").html(getRemainingLength(input).toString()).appendTo(maxLengthIndicator);
    return maxLengthIndicator;
  }

  function positionMaxLengthIndicator(maxLengthIndicator: JQuery, input: JQuery): void {
    maxLengthIndicator.css(
      {
        left: input.offset().left + input.outerWidth() + 20,
        top: input.offset().top
      }
    );
  }

  function showMaxLengthIndicator(maxLengthIndicator: JQuery): void {
    maxLengthIndicator.animate({ opacity: 1 }, "fast");
  }

  function bindMaxLengthIndicatorToInput(maxLengthIndicator: JQuery, input: JQuery): void {
    var remainingLength = maxLengthIndicator.find(".remaining-length");

    input.bind(
      "input keyup paste propertychange",
      function () {
        remainingLength.html(getRemainingLength(input).toString());
      }
      );
  }

  function getMaxLengthIndicator(): JQuery {
    return $(".max-length-indicator");
  }

  function hideAndRemoveMaxLengthIndicator(maxLengthIndicator: JQuery): void {
    maxLengthIndicator.animate(
      { opacity: 0 },
      "fast",
      function () {
        maxLengthIndicator.remove();
      }
      );
  }

  function unbindMaxLengthIndicatorFromInput(input: JQuery): void {
    input.unbind("input keyup paste propertychange");
  }

  function getRemainingLength(input: JQuery): number {
    return parseInt(input.attr("maxlength")) - input.val().length;
  }
}