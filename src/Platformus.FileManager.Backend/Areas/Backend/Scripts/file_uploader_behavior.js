// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.fileUploader = {};
  platformus.fileUploader.fileSelected = function () {
    document["form"].submit();
  };

  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    if (typeof(window["FileReader"]) == "undefined") {
      return;
    }

    var filesDragAndDropArea = $("#filesDragAndDropArea");

    if (filesDragAndDropArea.length == 0) {
      return;
    }

    filesDragAndDropArea[0].ondragover = function () {
      filesDragAndDropArea.addClass("files-drag-and-drop-area--dragging");
      return false;
    };

    filesDragAndDropArea[0].ondragleave = function () {
      filesDragAndDropArea.removeClass("files-drag-and-drop-area--dragging");
      return false;
    };

    filesDragAndDropArea[0].ondrop = function (event) {
      filesDragAndDropArea.removeClass("files-drag-and-drop-area--dragging");

      var formData = new FormData();

      for (var i = 0; i != event.dataTransfer.files.length; i++) {
        formData.append("files", event.dataTransfer.files[i]);
      }

      $.ajax(
        {
          url: "/backend/filemanager/create",
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
})(window.platformus = window.platformus || {});