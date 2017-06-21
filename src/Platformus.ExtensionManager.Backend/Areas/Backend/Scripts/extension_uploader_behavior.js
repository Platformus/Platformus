// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.extensionUploader = {};
  platformus.extensionUploader.extensionSelected = function () {
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

    var extensionsDragAndDropArea = $("#extensionsDragAndDropArea");

    if (extensionsDragAndDropArea.length == 0) {
      return;
    }

    extensionsDragAndDropArea[0].ondragover = function () {
      extensionsDragAndDropArea.addClass("extensions-drag-and-drop-area--dragging");
      return false;
    };

    extensionsDragAndDropArea[0].ondragleave = function () {
      extensionsDragAndDropArea.removeClass("extensions-drag-and-drop-area--dragging");
      return false;
    };

    extensionsDragAndDropArea[0].ondrop = function (event) {
      extensionsDragAndDropArea.removeClass("extensions-drag-and-drop-area--dragging");

      var formData = new FormData();

      for (var i = 0; i != event.dataTransfer.files.length; i++) {
        formData.append("files", event.dataTransfer.files[i]);
      }

      $.ajax(
        {
          url: "/backend/extensionmanager/create",
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