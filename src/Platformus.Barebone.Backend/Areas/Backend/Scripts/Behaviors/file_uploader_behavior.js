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
    $(document.body).on("change", ".file-uploader__browse-input", fileSelected);
  }

  function fileSelected() {
    var input = $(this);
    var filename = input.parent().parent().parent().find(".file-uploader__filename");
    var size = this.files[0].size;

    filename.removeClass("file-uploader__filename--not-selected");
    filename.html(input.val().replace(/^.*[\\\/]/, "") + " <span class=\"file-uploader__filename--size\">(" + size + " bytes)</span>");
  }
})(window.platformus = window.platformus || {});