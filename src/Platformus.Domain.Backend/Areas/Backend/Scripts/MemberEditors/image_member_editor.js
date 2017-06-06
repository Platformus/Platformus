// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.image = {};
  platformus.memberEditors.image.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("image-member-editor").addClass("form__field").addClass("field");

    platformus.memberEditors.base.createLabel(member).appendTo(field);

    if (member.isPropertyLocalizable) {
    }

    else {
      for (var i = 0; i < member.property.stringValue.localizations.length; i++) {
        var localization = member.property.stringValue.localizations[i];

        if (localization.culture.code == "__") {
          createInput(member, localization).appendTo(field);
          createImage(member, localization).appendTo(field);
          createButtons(member, localization).appendTo(field);
        }
      }
    }

    return field;
  }

  function createInput(member, localization) {
    var identity = platformus.memberEditors.base.getIdentity(member, localization);

    return $("<input>").attr("id", identity).attr("name", identity).attr("type", "hidden").attr("value", localization.value);
  }

  function createImage(member, localization) {
    var identity = platformus.memberEditors.base.getIdentity(member, localization);
    var img = $("<img>").addClass("image-member-editor__image").attr("id", identity + "Image").attr("src", localization.value);

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
    var identity = platformus.memberEditors.base.getIdentity(member, localization);

    return $("<button>").addClass("buttons__button").addClass("buttons__button--minor").addClass("button").addClass("button--positive").addClass("button--minor").attr("type", "button").html("Upload…").click(
      function () {
        new platformus.forms.imageUploaderForm.show(
          "/images/objects/",
          platformus.memberEditors.base.getDataTypeParameterValue(member, "Width", null),
          platformus.memberEditors.base.getDataTypeParameterValue(member, "Height", null),
          function (imageUrl) {
            $("#" + identity).val(imageUrl);
            $("#" + identity + "Image").attr("src", imageUrl).show();
          }
        );
      }
    );
  }

  function createRemoveButton(member, localization) {
    var identity = platformus.memberEditors.base.getIdentity(member, localization);

    return $("<button>").addClass("buttons__button").addClass("buttons__button--minor").addClass("button").addClass("button--negative").addClass("button--minor").attr("type", "button").html("Remove").click(
      function () {
        $("#" + identity).val(platformus.string.empty);
        $("#" + identity + "Image").removeAttr("src").hide();
      }
    );
  }
})(window.platformus = window.platformus || {});