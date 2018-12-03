// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  var _destinationBaseUrl = null;
  var _destinationWidth = null;
  var _destinationHeight = null;
  var _callback = null;
  var _isCropping = false;
  var _isMoving = false;
  var _isResizing = false;
  var _mouseOverFrame = false;
  var _mouseOverGrip = false;
  var _x = 0;
  var _y = 0;
  var _frameX = 0;
  var _frameY = 0;
  var _frameWidth = 0;
  var _frameHeight = 0;

  platformus.forms = platformus.forms || {};
  platformus.forms.imageUploaderForm = {};
  platformus.forms.imageUploaderForm.show = function (destinationBaseUrl, width, height, callback) {
    _destinationBaseUrl = destinationBaseUrl || "/images/";
    _destinationWidth = width;
    _destinationHeight = height;
    _callback = callback;
    return platformus.forms.baseForm.show("/backend/barebone/imageuploaderform", defineHandlers, "image-uploader-pop-up-form");
  };

  platformus.forms.imageUploaderForm.uploadingStarted = function (checkIsFinished) {
    $("#imageUploader").hide();
    $("#imageUploadingIndicator").show();

    if (checkIsFinished) {
      setTimeout(function () { platformus.forms.imageUploaderForm.isFinished(); }, 100);
    }
  };

  platformus.forms.imageUploaderForm.uploadingFinished = function (filename) {
    $("#imageUploadingIndicator").hide();

    var imageCropper = $("#imageCropper").show();
    var image = $("<img>")
      .addClass("image-uploader-pop-up-form__image-cropper-image")
      .attr("src", "/images/temp/" + filename)
      .on(
        "load",
        function () {
          setTimeout(
            function () {
              var shader = $("<div>").addClass("image-uploader-pop-up-form__image-cropper-shader").appendTo(imageCropper);

              shader.css(
                {
                  width: image.width(),
                  height: image.height()
                }
              );

              var frame = $("<div>").addClass("image-uploader-pop-up-form__image-cropper-frame").attr("id", "imageCropperFrame").appendTo(imageCropper);
              var width = _destinationWidth == null ? 100 : _destinationWidth;
              var height = _destinationHeight == null ? 100 : _destinationHeight;

              if (width > image.width()) {
                var factor = image.width() / width;

                width = image.width();
                height *= factor;
              }

              if (height > image.height()) {
                var factor = image.height() / height;

                width *= factor;
                height = image.height();
              }

              frame.css(
                {
                  width: width,
                  height: height
                }
              );

              var imageFragment = $("<img>")
                .addClass("image-uploader-pop-up-form__image-cropper-image-fragment")
                .attr("src", image.attr("src"))
                .css({ width: image.width(), height: image.height() })
                .appendTo(frame);

              var grip = $("<div>")
                .addClass("image-uploader-pop-up-form__image-cropper-grip")
                .attr("id", "imageCropperGrip")
                .css({ left: frame.width() - 5, top: frame.height() - 5 })
                .appendTo(imageCropper);

              _isCropping = true;
            },
            250
          );
        }
      ).appendTo(imageCropper);
  };

  platformus.forms.imageUploaderForm.isFinished = function () {
    var value = document.getElementById("imageUploader").contentWindow.document.body.innerHTML;

    value = value.replace(/<(?:.|\n)*?>/gm, "");

    if (value.indexOf("filename=") != -1) {
      var filename = value.replace("filename=", platformus.string.empty);

      platformus.forms.imageUploaderForm.uploadingFinished(filename);
    }

    else if (value.indexOf("error=") != -1) {
      var error = value.replace("error=", platformus.string.empty);

      platformus.forms.imageUploaderForm.uploadingErrorOccurred(error);
    }

    else {
      setTimeout(function () { platformus.forms.imageUploaderForm.isFinished(); }, 100);
    }
  };

  platformus.forms.imageUploaderForm.uploadingErrorOccurred = function (textStatus) {
    alert(textStatus);
  };

  platformus.forms.imageUploaderForm.done = function () {
    var image = $("#imageCropper img");
    var frame = $("#imageCropperFrame");
    var factor = image[0].naturalWidth / image.width();
    var x = Math.round((frame.position().left + 1) * factor);
    var y = Math.round((frame.position().top + 1) * factor);
    var width = Math.round(frame.width() * factor);
    var height = Math.round(frame.height() * factor);

    $.get(
      "/backend/imageuploader/getcroppedimageurl",
      {
        sourceImageUrl: image.attr("src"),
        sourceX: x,
        sourceY: y,
        sourceWidth: width,
        sourceHeight: height,
        destinationBaseUrl: _destinationBaseUrl,
        destinationWidth: _destinationWidth == null ? width : _destinationWidth,
        destinationHeight: _destinationHeight == null ? height : _destinationHeight
      },
      function (result) {
        if (_callback != null) {
          _callback(result);
        }

        platformus.forms.imageUploaderForm.hideAndRemove();
      }
    );

    return false;
  };

  platformus.forms.imageUploaderForm.hideAndRemove = function () {
    return platformus.forms.baseForm.hideAndRemove();
  };

  function defineHandlers() {
    $(document).on(
        "mouseenter",
        "#imageCropperFrame",
        function () {
          _mouseOverFrame = true;
        }
      );

    $(document).on(
      "mouseleave",
      "#imageCropperFrame",
      function () {
        _mouseOverFrame = false;
      }
    );

    $(document).on(
      "mouseenter",
      "#imageCropperGrip",
      function () {
        _mouseOverGrip = true;
      }
    );

    $(document).on(
      "mouseleave",
      "#imageCropperGrip",
      function () {
        _mouseOverGrip = null;
      }
    );

    $(document).on(
      "mousedown",
      function (e) {
        if (!_isCropping) {
          return true;
        }

        if (!_mouseOverFrame && !_mouseOverGrip) {
          return true;
        }

        _x = e.pageX;
        _y = e.pageY;

        if (_mouseOverFrame) {
          _isMoving = true;
          _frameX = $("#imageCropperFrame").position().left;
          _frameY = $("#imageCropperFrame").position().top;
        }

        else if (_mouseOverGrip) {
          _isResizing = true;
          _frameWidth = $("#imageCropperFrame").width();
          _frameHeight = $("#imageCropperFrame").height();
        }

        return false;
      }
    );

    $(document).on(
      "mousemove",
      function (e) {
        if (_isMoving) {
          var frame = $("#imageCropperFrame");
          var image = frame.find("img");
          var grip = $("#imageCropperGrip");
          var xOffset = e.pageX - _x;
          var yOffset = e.pageY - _y;
          var x = _frameX + xOffset;
          var y = _frameY + yOffset

          if (x < -1) {
            x = -1;
          }

          if (x > image.width() - frame.width() - 1) {
            x = image.width() - frame.width() - 1;
          }

          if (y < -1) {
            y = -1;
          }

          if (y > image.height() - frame.height() - 1) {
            y = image.height() - frame.height() - 1;
          }

          frame.css({ left: x, top: y });
          image.css({ left: x * -1 - 1, top: y * -1 - 1 });
          grip.css(
            {
              left: frame.position().left + frame.width() - 4,
              top: frame.position().top + frame.height() - 4
            }
          );
        }

        else if (_isResizing) {
          var frame = $("#imageCropperFrame");
          var image = frame.find("img");
          var grip = $("#imageCropperGrip");
          var xOffset = e.pageX - _x;
          var yOffset = e.pageY - _y;

          if (_destinationWidth == null && _destinationHeight == null) {
            var width = _frameWidth + xOffset;

            if (frame.position().left + width > image.width() - 1) {
              width = image.width() - frame.position().left - 1;
            }

            var height = _frameHeight + yOffset;

            if (frame.position().top + height > image.height() - 1) {
              height = image.height() - frame.position().top - 1;
            }
          }

          else {
            var offset = (xOffset + yOffset) / 2;
            var width = _frameWidth + offset;

            if (frame.position().left + width > image.width() - 1) {
              width = image.width() - frame.position().left - 1;
            }

            var delta = _frameWidth / width;
            var height = _frameHeight / delta;

            if (frame.position().top + height > image.height() - 1) {
              var delta = height / (image.height() - frame.position().top - 1);

              width = width / delta;
              height = image.height() - frame.position().top - 1;
            }
          }

          frame.css({ width: width, height: height });
          grip.css(
            {
              left: frame.position().left + frame.width() - 4,
              top: frame.position().top + frame.height() - 4
            }
          );
        }
      }
    );

    $(document).on(
      "mouseup",
      function (e) {
        if (!_isCropping) {
          return true;
        }

        _isMoving = null;
        _isResizing = null;
        _x = null;
        _y = null;
        return false;
      }
    );
  }
})(window.platformus = window.platformus || {});