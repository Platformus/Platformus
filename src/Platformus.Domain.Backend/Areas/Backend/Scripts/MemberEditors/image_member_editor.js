// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.image = {};
  platformus.memberEditors.image.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);

    if (member.isPropertyLocalizable) {
    }

    else {
      for (var i = 0; i < member.property.stringValue.localizations.length; i++) {
        var localization = member.property.stringValue.localizations[i];

        if (localization.culture.code == "__") {
          createImageUploader(member, localization).appendTo(field);
        }
      }
    }

    return field;
  }

  function createImageUploader(member, localization) {
    var imageUploader = $("<div>").addClass("image-uploader");
    var width = platformus.memberEditors.base.getDataTypeParameterValue(member, "Width", null);
    var height = platformus.memberEditors.base.getDataTypeParameterValue(member, "Height", null);

    if (width != null && height != null) {
      imageUploader.attr("data-width", width).attr("data-height", height);
    }

    createInput(member, localization).appendTo(imageUploader);
    createImage(member, localization).appendTo(imageUploader);
    createButtons(member, localization).appendTo(imageUploader);
    return imageUploader;
  }

  function createInput(member, localization) {
    var identity = platformus.memberEditors.base.getIdentity(member, localization);

    return $("<input>").attr("name", identity).attr("type", "hidden").attr("value", localization.value);
  }

  function createImage(member, localization) {
    var identity = platformus.memberEditors.base.getIdentity(member, localization);
    var img = $("<img>").addClass("image-uploader__image").attr("src", localization.value);

    if (localization.value == null) {
      img.hide();
    }

    return img;
  }

  function createButtons(member, localization) {
    var buttons = $("<div>").addClass("form__buttons").addClass("form__buttons--minor").addClass("buttons");

    createUploadButton(member, localization).appendTo(buttons);
    createRemoveButton(member, localization).appendTo(buttons);
    return buttons;
  }

  function createUploadButton(member, localization) {
    return $("<button>")
      .addClass("image-uploader__upload-button")
      .addClass("buttons__button")
      .addClass("buttons__button--minor")
      .addClass("button")
      .addClass("button--positive")
      .addClass("button--minor")
      .attr("type", "button")
      .html("Upload…");
  }

  function createRemoveButton(member, localization) {
    return $("<button>")
      .addClass("image-uploader__remove-button")
      .addClass("buttons__button")
      .addClass("buttons__button--minor")
      .addClass("button")
      .addClass("button--negative")
      .addClass("button--minor")
      .attr("type", "button")
      .html("Remove");
  }
})(window.platformus = window.platformus || {});