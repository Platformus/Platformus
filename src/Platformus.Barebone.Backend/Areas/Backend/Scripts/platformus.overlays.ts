/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Overlays {
  export class OverlayBase {
    protected overlay: JQuery;

    public constructor() {
      this.create();
    }

    public show(): boolean {
      this.overlay.animate({ opacity: 1 }, "fast");
      return false;
    }

    public hideAndRemove(): boolean {
      this.overlay.animate({ opacity: 0 }, "fast", function () { $(this).remove(); });
      return false;
    }

    protected create(): void {
      this.overlay = $("<div>").addClass("overlay").appendTo($(document.body));
    }
  }

  class Modal extends OverlayBase {
    protected create(): void {
      super.create();
      this.overlay.addClass("modal");
    }
  }

  export var form: FormBase;

  export class FormBase extends OverlayBase {
    private formUrl: string;
    private modal: Modal;

    public constructor(formUrl: string) {
      super();
      this.formUrl = formUrl;
      this.modal = new Modal();
      Platformus.Overlays.form = this;
    }

    public show(): boolean {
      this.modal.show();
      super.show();
      this.position();
      this.load();
      return false;
    }

    public hideAndRemove(): boolean {
      this.modal.hideAndRemove();
      super.hideAndRemove();
      return false;
    }

    public getOverlay(): JQuery {
      return this.overlay;
    }

    protected create(): void {
      super.create();
      this.overlay.addClass("form");
    }

    protected position(): void {
      this.overlay.css(
        {
          left: $(window).width() / 2 - this.overlay.outerWidth() / 2,
          top: $(window).height() / 2 - this.overlay.outerHeight() / 2
        }
      );
    }

    protected load(): void {
      var $this = this;

      $.ajax(
        {
          url: this.formUrl,
          type: "GET",
          cache: false,
          success: function (result) {
            $this.overlay.html(result);
            $this.position();
            $this.bind();
          }
        }
      );
    }

    protected bind(): void { }
  }
}