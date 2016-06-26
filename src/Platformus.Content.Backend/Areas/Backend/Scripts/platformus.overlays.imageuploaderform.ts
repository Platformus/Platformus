// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../../scripts/typings/platformus/platformus.d.ts" />
/// <reference path="../../../scripts/typings/platformus/platformus.overlays.d.ts" />

module Platformus.Overlays {
  export class ImageUploaderForm extends FormBase {
    private callback: (imageUrl: string) => void;
    private isCropping: boolean;
    private isMoving: boolean;
    private isResizing: boolean;
    private move: boolean;
    private resize: boolean;
    private x: number;
    private y: number;
    private width: number;
    private height: number;
    private frameX: number;
    private frameY: number;
    private frameWidth: number;
    private frameHeight: number;

    public constructor(callback: (imageUrl: string) => void) {
      super("/backend/content/imageuploaderform");
      this.callback = callback;
    }

    public uploadingStarted(checkIsFinished: boolean): void {
      $("#imageUploader").hide();
      $("#imageUploadingIndicator").show();

      if (checkIsFinished) {
        setTimeout(function () { (<ImageUploaderForm>Platformus.Overlays.form).isFinished(); }, 100);
      }
    }

    public uploadingFinished(filename): void {
      $("#imageUploadingIndicator").hide();

      var imageCropper = $("#imageCropper").show();
      var image = $("<img>")
        .attr("src", "/images/temp/" + filename)
        .on(
          "load",
          function () {
            setTimeout(
              function () {
                var shader = $("<div>").addClass("shader").appendTo(imageCropper);

                shader.css(
                  {
                    width: image.width(),
                    height: image.height()
                  }
                );

                var frame = $("<div>").addClass("frame").attr("id", "imageCropperFrame").appendTo(imageCropper);
                var width = 100;
                var height = 100;

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
                  .addClass("image-fragment")
                  .attr("src", image.attr("src"))
                  .css({ width: image.width(), height: image.height() })
                  .appendTo(frame);

                var grip = $("<div>")
                  .addClass("grip")
                  .attr("id", "imageCropperGrip")
                  .css({ left: frame.width() - 5, top: frame.height() - 5 })
                  .appendTo(imageCropper);

                (<ImageUploaderForm>Platformus.Overlays.form).isCropping = true;
              },
              250
            );
          }
        ).appendTo(imageCropper);
    }

    public isFinished() {
      var value = (<any>document.getElementById("imageUploader")).contentWindow.document.body.innerHTML;

      if (value.indexOf("filename=") != -1) {
        var filename = value.replace("filename=", String.empty).replace("<pre>", String.empty).replace("</pre>", String.empty);

        (<ImageUploaderForm>Platformus.Overlays.form).uploadingFinished(filename);
      }

      else if (value.indexOf("error=") != -1) {
        var error = value.replace("error=", String.empty);

        (<ImageUploaderForm>Platformus.Overlays.form).uploadingErrorOccurred(error);
      }

      else {
        setTimeout(function () { (<ImageUploaderForm>Platformus.Overlays.form).isFinished(); }, 100);
      }
    }

    public uploadingErrorOccurred(textStatus): void {
      alert(textStatus);
    }

    public done() {
      var image = $("#imageCropper img");
      var frame = $("#imageCropperFrame");
      var factor = (<any>image[0]).naturalWidth / image.width();
      var x = Math.round((frame.position().left + 1) * factor);
      var y = Math.round((frame.position().top + 1) * factor);
      var width = Math.round(frame.width() * factor);
      var height = Math.round(frame.height() * factor);

      $.get(
        "/backend/imageuploader/getcroppedimageurl",
        { imageUrl: image.attr("src"), x: x, y: y, width: width, height: height, sourceWidth: this.width == null ? -1 : this.width, sourceHeight: this.height == null ? -1 : this.height },
        function (result) {
          (<ImageUploaderForm>Platformus.Overlays.form).hideAndRemove();
          (<ImageUploaderForm>Platformus.Overlays.form).callback(result);
        }
      );

      return false;
    }

    protected create(): void {
      super.create();
      this.overlay.addClass("image-uploader-form");

      $(document).on(
        "mouseenter",
        "#imageCropperFrame",
        function () {
          (<ImageUploaderForm>Platformus.Overlays.form).move = true;
        }
      );

      $(document).on(
        "mouseleave",
        "#imageCropperFrame",
        function () {
          (<ImageUploaderForm>Platformus.Overlays.form).move = false;
        }
      );

      $(document).on(
        "mouseenter",
        "#imageCropperGrip",
        function () {
          (<ImageUploaderForm>Platformus.Overlays.form).resize = true;
        }
      );

      $(document).on(
        "mouseleave",
        "#imageCropperGrip",
        function () {
          (<ImageUploaderForm>Platformus.Overlays.form).resize = null;
        }
      );

      $(document).on(
        "mousedown",
        function (e) {
          if (!(<ImageUploaderForm>Platformus.Overlays.form).isCropping) {
            return true;
          }

          if (!(<ImageUploaderForm>Platformus.Overlays.form).move && !(<ImageUploaderForm>Platformus.Overlays.form).resize) {
            return true;
          }

          (<ImageUploaderForm>Platformus.Overlays.form).x = e.pageX;
          (<ImageUploaderForm>Platformus.Overlays.form).y = e.pageY;

          if ((<ImageUploaderForm>Platformus.Overlays.form).move) {
            (<ImageUploaderForm>Platformus.Overlays.form).isMoving = true;
            (<ImageUploaderForm>Platformus.Overlays.form).frameX = $("#imageCropperFrame").position().left;
            (<ImageUploaderForm>Platformus.Overlays.form).frameY = $("#imageCropperFrame").position().top;
          }

          else if ((<ImageUploaderForm>Platformus.Overlays.form).resize) {
            (<ImageUploaderForm>Platformus.Overlays.form).isResizing = true;
            (<ImageUploaderForm>Platformus.Overlays.form).frameWidth = $("#imageCropperFrame").width();
            (<ImageUploaderForm>Platformus.Overlays.form).frameHeight = $("#imageCropperFrame").height();
          }

          return false;
        }
      );

      $(document).on(
        "mousemove",
        function (e) {
          if ((<ImageUploaderForm>Platformus.Overlays.form).isMoving) {
            var frame = $("#imageCropperFrame");
            var image = frame.find("img");
            var grip = $("#imageCropperGrip");
            var xOffset = e.pageX - (<ImageUploaderForm>Platformus.Overlays.form).x;
            var yOffset = e.pageY - (<ImageUploaderForm>Platformus.Overlays.form).y;
            var x = (<ImageUploaderForm>Platformus.Overlays.form).frameX + xOffset;
            var y = (<ImageUploaderForm>Platformus.Overlays.form).frameY + yOffset

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

          else if ((<ImageUploaderForm>Platformus.Overlays.form).isResizing) {
            var frame = $("#imageCropperFrame");
            var image = frame.find("img");
            var grip = $("#imageCropperGrip");
            var xOffset = e.pageX - (<ImageUploaderForm>Platformus.Overlays.form).x;
            var yOffset = e.pageY - (<ImageUploaderForm>Platformus.Overlays.form).y;

            if ((<ImageUploaderForm>Platformus.Overlays.form).width == null && (<ImageUploaderForm>Platformus.Overlays.form).height == null) {
              var width = (<ImageUploaderForm>Platformus.Overlays.form).frameWidth + xOffset;

              if (frame.position().left + width > image.width() - 1) {
                width = image.width() - frame.position().left - 1;
              }

              var height = (<ImageUploaderForm>Platformus.Overlays.form).frameHeight + yOffset;

              if (frame.position().top + height > image.height() - 1) {
                height = image.height() - frame.position().top - 1;
              }
            }

            else {
              var offset = (xOffset + yOffset) / 2;
              var width = (<ImageUploaderForm>Platformus.Overlays.form).frameWidth + offset;

              if (frame.position().left + width > image.width() - 1) {
                width = image.width() - frame.position().left - 1;
              }

              var delta = (<ImageUploaderForm>Platformus.Overlays.form).frameWidth / width;
              var height = (<ImageUploaderForm>Platformus.Overlays.form).frameHeight / delta;

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
          if (!(<ImageUploaderForm>Platformus.Overlays.form).isCropping) {
            return true;
          }

          (<ImageUploaderForm>Platformus.Overlays.form).isMoving = null;
          (<ImageUploaderForm>Platformus.Overlays.form).isResizing = null;
          (<ImageUploaderForm>Platformus.Overlays.form).x = null;
          (<ImageUploaderForm>Platformus.Overlays.form).y = null;
          return false;
        }
      );
    }

    protected bind(): void {
      Platformus.Overlays.form.getOverlay().find(".neutral").bind("click", $.proxy(this.hideAndRemove, this));
    }
  }
}