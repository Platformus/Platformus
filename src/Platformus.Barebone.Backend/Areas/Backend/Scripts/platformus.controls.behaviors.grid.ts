/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.ts" />
module Platformus.Controls.Behaviors.Grid {
  import String = Platformus.String;
  import Url = Platformus.Url;
  import Descriptor = Platformus.Url.Descriptor;

  export function apply(): void {
    defineHandlers();
  }

  function defineHandlers(): void {
    $(document.body).on("change", ".grid .header .take-selector .drop-down-list", takeSelectorChangeHandler);
    $(document.body).on("keyup", ".grid .header .filter input", filterKeyUpHandler);
  }

  function takeSelectorChangeHandler(): void {
    location.href = Url.combine(
      [
        new Descriptor({ name: "filter", takeFromUrl: true }),
        new Descriptor({ name: "orderby", takeFromUrl: true }),
        new Descriptor({ name: "direction", takeFromUrl: true }),
        new Descriptor({ name: "skip", skip: true }),
        new Descriptor({ name: "take", value: $(this).val() })
      ]
    );
  }

  function filterKeyUpHandler(e: JQueryEventObject): void {
    if (e.keyCode == 13) {
      var value = $(this).val();

      location.href = Url.combine(
        [
          new Descriptor({ name: "filter", value: String.isNullOrEmpty(value) ? null : value, skip: String.isNullOrEmpty(value) }),
          new Descriptor({ name: "orderby", takeFromUrl: true }),
          new Descriptor({ name: "direction", takeFromUrl: true }),
          new Descriptor({ name: "skip", skip: true }),
          new Descriptor({ name: "take", skip: true })
        ]
      );
    }
  }
}