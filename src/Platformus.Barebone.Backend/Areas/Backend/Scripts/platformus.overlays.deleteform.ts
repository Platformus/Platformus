/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.overlays.ts" />
module Platformus.Overlays {
  export class DeleteForm extends FormBase {
    public constructor(targetUrl: string) {
      super("/backend/barebone/deleteform?targeturl=" + encodeURIComponent(targetUrl));
    }

    protected bind(): void {
      Platformus.Overlays.form.getOverlay().find(".neutral").bind("click", $.proxy(this.hideAndRemove, this));
    }
  }
}