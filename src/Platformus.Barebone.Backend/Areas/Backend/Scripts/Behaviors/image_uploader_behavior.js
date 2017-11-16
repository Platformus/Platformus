// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.behaviors = platformus.behaviors || [];
  platformus.behaviors.push(
    function () {
      defineHandlers();
    }
  );

  function defineHandlers() {
    $(document.body).on("click", ".image-uploader__upload-button", uploadButtonClicked);
    $(document.body).on("click", ".image-uploader__remove-button", removeButtonClicked);
  }

  function uploadButtonClicked() {
    var button = $(this);
    var imageUploader = button.parent().parent();
    var input = imageUploader.find("input");
    var image = imageUploader.find("img");

    new platformus.forms.imageUploaderForm.show(
      "/images/objects/",
      imageUploader.data("width"),
      imageUploader.data("height"),
      function (imageUrl) {
        input.val(imageUrl);
        image.attr("src", imageUrl).show();
      }
    );
  }

  function removeButtonClicked() {
    var button = $(this);
    var imageUploader = button.parent().parent();
    var input = imageUploader.find("input");
    var image = imageUploader.find("img");

    input.val(platformus.string.empty);
    image.removeAttr("src").hide();
  }
})(window.platformus = window.platformus || {});