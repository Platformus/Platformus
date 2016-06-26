// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../../scripts/typings/platformus/platformus.d.ts" />
/// <reference path="../../../scripts/typings/platformus/platformus.overlays.d.ts" />

module Platformus.Overlays {
  export class ObjectSelectorForm extends FormBase {
    private callback: (objectIds: string) => void;

    public constructor(relationClassId: number, objectIds: string, callback: (objectIds: string) => void) {
      super("/backend/content/objectselectorform?classid=" + relationClassId + "&objectids=" + objectIds);
      this.callback = callback;
    }

    public select() {
      this.hideAndRemove();
      this.callback(this.getSelectedObjectIds());
      return false;
    }

    protected create(): void {
      super.create();
      this.overlay.addClass("object-selector-form");
    }

    protected bind(): void {
      Platformus.Overlays.form.getOverlay().find(".grid table tr").bind("click", this.toggleRowSelection);
      Platformus.Overlays.form.getOverlay().find(".neutral").bind("click", $.proxy(this.hideAndRemove, this));
    }

    private toggleRowSelection(): boolean {
      $(this).toggleClass("selected");
      return false;
    }

    private getSelectedObjectIds(): string {
      var objectIds = String.empty;

      $(".object-selector-form .grid table .selected").each(
        function (index, element) {
          if (!String.isNullOrEmpty(objectIds)) {
            objectIds += ",";
          }

          objectIds += $(element).data("objectId");
        }
      );

      return objectIds;
    }
  }
}