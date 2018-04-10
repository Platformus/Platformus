// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.photoDragAndDropArea = {};
  platformus.photoDragAndDropArea.selected = function () {
    parent.platformus.ui.photoUploadingStarted(true);
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

    var photoDragAndDropArea = $("#photoDragAndDropArea");

    if (photoDragAndDropArea.length == 0) {
      return;
    }

    photoDragAndDropArea[0].ondragover = function () {
      photoDragAndDropArea.addClass("drag-and-drop-area--dragging");
      return false;
    };

    photoDragAndDropArea[0].ondragleave = function () {
      photoDragAndDropArea.removeClass("drag-and-drop-area--dragging");
      return false;
    };

    photoDragAndDropArea[0].ondrop = function (event) {
      photoDragAndDropArea.removeClass("drag-and-drop-area--dragging");

      var formData = new FormData();

      for (var i = 0; i != event.dataTransfer.files.length; i++) {
        formData.append("files", event.dataTransfer.files[i]);
      }

      $.ajax(
        {
          url: "/backend/photouploader",
          data: formData,
          processData: false,
          contentType: false,
          type: "POST",
          success: function (data) {
            parent.platformus.ui.photoUploadingFinished(data.replace("filenames=", platformus.string.empty));
          },
          error: function (jqXHR, textStatus) {
            parent.platformus.ui.photoUploadingErrorOccurred(textStatus);
          }
        }
      );

      parent.platformus.ui.photoUploadingStarted(false);
      return false;
    }
  }
})(window.platformus = window.platformus || {});