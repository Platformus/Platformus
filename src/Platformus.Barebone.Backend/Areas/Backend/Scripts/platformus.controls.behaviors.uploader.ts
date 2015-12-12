/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
module Platformus.Controls.Behaviors.Uploader {
  export function apply(): void {
    defineHandlers();
  }

  function defineHandlers(): void {
    if (typeof (window["FileReader"]) == "undefined") {
      return;
    }

    var filesDragAndDropArea = $("#filesDragAndDropArea");

    if (filesDragAndDropArea.length == 0) {
      return;
    }

    filesDragAndDropArea[0].ondragover = function () {
      filesDragAndDropArea.addClass("files-drag-and-drop-area-dragging");
      return false;
    };

    filesDragAndDropArea[0].ondragleave = function () {
      filesDragAndDropArea.removeClass("files-drag-and-drop-area-dragging");
      return false;
    };

    filesDragAndDropArea[0].ondrop = function (event) {
      filesDragAndDropArea.removeClass("files-drag-and-drop-area-dragging");

      var formData : any = new FormData();

      for (var i = 0; i != event.dataTransfer.files.length; i++) {
        formData.append("files", event.dataTransfer.files[i]);
      }

      $.ajax(
        {
          url: "/backend/files/create",
          data: formData,
          processData: false,
          contentType: false,
          type: "POST",
          success: function (data) {
            location.reload();
          }
        }
      );

      return false;
    }
  }
}