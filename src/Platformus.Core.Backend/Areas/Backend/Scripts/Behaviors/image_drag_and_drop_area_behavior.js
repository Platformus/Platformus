// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.imageDragAndDropArea = {};
  platformus.imageDragAndDropArea.selected = function () {
    parent.platformus.forms.imageUploaderForm.uploadingStarted(true);
    document["form"].submit();
  };

  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    var imageDragAndDropArea = $("#imageDragAndDropArea");

    if (!imageDragAndDropArea.length) return;

    imageDragAndDropArea[0].ondragover = function () {
      imageDragAndDropArea.addClass("drag-and-drop-area--dragging");
      return false;
    };

    imageDragAndDropArea[0].ondragleave = function () {
      imageDragAndDropArea.removeClass("drag-and-drop-area--dragging");
      return false;
    };

    imageDragAndDropArea[0].ondrop = function (event) {
      imageDragAndDropArea.removeClass("drag-and-drop-area--dragging");

      var formData = new FormData();

      // forEach doesn't work here (event.dataTransfer.files is not array)
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
            parent.platformus.forms.imageUploaderForm.uploadingFinished(data.replace("filename=", ""));
          },
          error: function (jqXHR, textStatus) {
            parent.platformus.forms.imageUploaderForm.uploadingErrorOccurred(textStatus);
          }
        }
      );

      parent.platformus.forms.imageUploaderForm.uploadingStarted(false);
      return false;
    }
  }
})(window.platformus = window.platformus || {});