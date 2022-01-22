// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    $(document.body).on("click", ".image-uploader__upload-button", onUploadButtonClick);
    $(document.body).on("click", ".image-uploader__remove-button", onRemoveButtonClick);
  }

  function onUploadButtonClick() {
    var imageUploader = $(this).closest(".image-uploader");
    var input = imageUploader.find("input");
    var image = imageUploader.find("img");

    new platformus.forms.imageUploaderForm.show(
      imageUploader.data("destinationBaseUrl"),
      imageUploader.data("width"),
      imageUploader.data("height"),
      function (imageUrl) {
        input.val(imageUrl);
        image.attr("src", imageUrl).show();
      }
    );
  }

  function onRemoveButtonClick() {
    var imageUploader = $(this).closest(".image-uploader");
    var input = imageUploader.find("input");
    var image = imageUploader.find("img");

    input.val("");
    image.removeAttr("src").hide();
  }
})(window.platformus = window.platformus || {});