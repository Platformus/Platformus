/// <reference path="platformus.controls.behaviors.ts"/>
/// <reference path="platformus.controls.extenders.ts" />
module Platformus.Areas.Backend.Application {
  export class Instance {
    public constructor() {
      this.initialize();
    }

    private initialize(): void {
      Platformus.Controls.Behaviors.apply();
      Platformus.Controls.Extenders.apply();
    }
  }
}

var p;

window.onload = function () {
  p = new Platformus.Areas.Backend.Application.Instance();
}