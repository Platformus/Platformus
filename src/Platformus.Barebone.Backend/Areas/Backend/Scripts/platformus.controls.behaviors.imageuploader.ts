// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.ts" />

module Platformus.Controls.Behaviors.ImageUploader {
  export function apply(): void {
    defineHandlers();
  }

  export function imageSelected(): void {
    (<any>parent).Platformus.Overlays.form.uploadingStarted(true);
    document["form"].submit();
  }

  function defineHandlers(): void {
    if (typeof (window["FileReader"]) == "undefined") {
      return;
    }

    var imageDragAndDropArea = $("#imageDragAndDropArea");

    if (imageDragAndDropArea.length == 0) {
      return;
    }

    imageDragAndDropArea[0].ondragover = function () {
      imageDragAndDropArea.addClass("image-drag-and-drop-area-dragging");
      return false;
    };

    imageDragAndDropArea[0].ondragleave = function () {
      imageDragAndDropArea.removeClass("image-drag-and-drop-area-dragging");
      return false;
    };

    imageDragAndDropArea[0].ondrop = function (event) {
      imageDragAndDropArea.removeClass("image-drag-and-drop-area-dragging");

      var formData : any = new FormData();

      for (var i = 0; i != event.dataTransfer.files.length; i++) {
        formData.append("files", event.dataTransfer.files[i]);
      }

      $.ajax(
        {
          url: "/backend/imageuploader",
          data: formData,
          processData: false,
          contentType: false,
          type: "POST",
          success: function (data) {
            (<any>parent).Platformus.Overlays.form.uploadingFinished(data.replace("filename=", String.empty));
          },
          error: function (jqXHR, textStatus) {
            (<any>parent).Platformus.Overlays.form.uploadingErrorOccurred(textStatus);
          }
        }
      );

      (<any>parent).Platformus.Overlays.form.uploadingStarted(false);
      return false;
    }
  }
}