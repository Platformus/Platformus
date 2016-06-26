// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
/// <reference path="platformus.overlays.imageuploaderform.ts" />

module Platformus.Editors.Image {
  export function create(container: JQuery, member: any): void {
    createField(container, member);
  }

  function createField(container: JQuery, member: any): JQuery {
    var field = $("<div>").addClass("field").appendTo(container);

    createLabel(member).appendTo(field);

    if (member.isPropertyLocalizable) {
    }

    else {
      member.property.localizations.forEach(
        (localization, index) => {
          if (localization.culture.code == "__") {
            createInput(member, localization).appendTo(field);
            createImage(member, localization).appendTo(field);
            createButtons(member, localization).appendTo(field);
          }
        }
      );
    }

    return field;
  }

  function createLabel(member: any): JQuery {
    return $("<label>").html(member.name);
  }

  function createInput(member: any, localization: any): JQuery {
    var identity = "propertyMember" + member.id + localization.culture.code;

    return $("<input>").attr("id", identity).attr("name", identity).attr("type", "hidden").attr("value", localization.value);
  }

  function createImage(member: any, localization: any): JQuery {
    var identity = "propertyMember" + member.id + localization.culture.code + "Image";
    var img = $("<img>").attr("id", identity).attr("src", localization.value);

    if (String.isNullOrEmpty(localization.value)) {
      img.hide();
    }

    return img;
  }

  function createButtons(member: any, localization: any): JQuery {
    var buttons = $("<div>").addClass("buttons").addClass("tool");

    createUploadButton(member, localization).appendTo(buttons);
    createRemoveButton(member, localization).appendTo(buttons);
    createClear().appendTo(buttons);
    return buttons;
  }

  function createUploadButton(member: any, localization: any): JQuery {
    var identity = "propertyMember" + member.id + localization.culture.code;

    return $("<button>").addClass("positive").attr("type", "button").html("Upload…").click(
      function () {
        new Platformus.Overlays.ImageUploaderForm(
          function (imageUrl: string) {
            $("#" + identity).val(imageUrl);
            $("#" + identity + "Image").attr("src", imageUrl).show();
          }
        ).show();
      }
    );
  }

  function createRemoveButton(member: any, localization: any): JQuery {
    var identity = "propertyMember" + member.id + localization.culture.code;

    return $("<button>").addClass("negative").attr("type", "button").html("Remove").click(
      function () {
        $("#" + identity).val(String.empty);
        $("#" + identity + "Image").removeAttr("src").hide();
      }
    );
  }

  function createClear(): JQuery {
    return $("<div>").addClass("clear");
  }
}