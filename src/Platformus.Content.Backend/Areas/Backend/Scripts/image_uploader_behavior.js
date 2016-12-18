// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.imageUploader = {};
  platformus.imageUploader.imageSelected = function () {
    parent.platformus.forms.imageUploaderForm.uploadingStarted(true);
    document["form"].submit();
  };

  platformus.registerBehavior(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    if (typeof(window["FileReader"]) == "undefined") {
      return;
    }

    var imageDragAndDropArea = $("#imageDragAndDropArea");

    if (imageDragAndDropArea.length == 0) {
      return;
    }

    imageDragAndDropArea[0].ondragover = function () {
      imageDragAndDropArea.addClass("image-drag-and-drop-area--dragging");
      return false;
    };

    imageDragAndDropArea[0].ondragleave = function () {
      imageDragAndDropArea.removeClass("image-drag-and-drop-area--dragging");
      return false;
    };

    imageDragAndDropArea[0].ondrop = function (event) {
      imageDragAndDropArea.removeClass("image-drag-and-drop-area--dragging");

      var formData = new FormData();

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
            parent.platformus.forms.imageUploaderForm.uploadingFinished(data.replace("filename=", String.empty));
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