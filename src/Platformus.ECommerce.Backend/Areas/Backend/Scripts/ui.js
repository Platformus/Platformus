// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.catalogCSharpClassNameChanged = function () {
    var cSharpClassName = getSelectedCatalogCSharpClassName();

    platformus.productProviderParameterEditors.sync(cSharpClassName);
  };

  platformus.ui.addAttribute = function () {
    platformus.forms.attributeSelectorForm.show(
      null,
      function (attributeId) {
        $.getJSON(
          "/backend/ecommerce/getattribute",
          { attributeId: attributeId },
          function (attribute) {
            var identity = "newAttribute" + ($(".table__row--new").length + 1);
            var row = $("<tr>").addClass("table__row").addClass("table__row--new").attr("id", identity).appendTo($("#attributes tbody"));
            var cell1 = $("<td>").addClass("table__cell").html(attribute.feature.name).appendTo(row);
            var cell2 = $("<td>").addClass("table__cell").html(attribute.value).appendTo(row);

            $("<input>").attr("name", identity).attr("type", "hidden").appendTo(cell1);
          }
        );
      }
    );
  };

  platformus.ui.removeAttribute = function (identity) {
    if (identity.indexOf("newAttribute") == -1) {
      var id = identity.replace("attribute", platformus.string.empty);
      var removedAttributeIds = $("#RemovedAttributeIds").val();

      removedAttributeIds += (removedAttributeIds.length == 0 ? platformus.string.empty : ",") + id;
      $("#RemovedAttributeIds").val(removedAttributeIds);
    }

    $("#" + identity).remove();
  };

  platformus.ui.photoUploadingStarted = function (checkIsFinished) {
    $("#photoUploader").hide();

    if (checkIsFinished) {
      setTimeout(function () { platformus.ui.isPhotoUploadingFinished(); }, 100);
    }
  };

  platformus.ui.photoUploadingFinished = function (filenames) {
    $("#photoUploader").attr("src", "/backend/photouploader").show();

    $(filenames.split(',')).each(
      function (index, filename) {
        createNewPhoto(filename).appendTo($("#photos"));
      }
    );
  };

  platformus.ui.isPhotoUploadingFinished = function () {
    var value = document.getElementById("photoUploader").contentWindow.document.body.innerHTML;

    value = value.replace(/<(?:.|\n)*?>/gm, "");

    if (value.indexOf("filenames=") != -1) {
      var filenames = value.replace("filenames=", platformus.string.empty);

      platformus.ui.photoUploadingFinished(filenames);
    }

    else if (value.indexOf("error=") != -1) {
      var error = value.replace("error=", platformus.string.empty);

      platformus.ui.photoUploadingErrorOccurred(error);
    }

    else {
      setTimeout(function () { platformus.ui.isPhotoUploadingFinished(); }, 100);
    }
  }

  platformus.ui.photoUploadingErrorOccurred = function (textStatus) {
    alert(textStatus);
  };

  platformus.ui.removePhoto = function (identity) {
    if (identity.indexOf("newPhoto") == -1) {
      var id = identity.replace("photo", platformus.string.empty);
      var removedPhotoIds = $("#RemovedPhotoIds").val();

      removedPhotoIds += (removedPhotoIds.length == 0 ? platformus.string.empty : ",") + id;
      $("#RemovedPhotoIds").val(removedPhotoIds);
    }

    $("#" + identity).remove();
  };

  platformus.ui.addProductToCart = function (cartId) {
    platformus.forms.productSelectorForm.show(
      null,
      function (productId) {
        $.post(
          "/backend/positions/create?cartid=" + cartId + "&productid=" + productId,
          function () {
            location.reload();
          }
        );
      }
    );
  };

  function getSelectedCatalogCSharpClassName() {
    return $("#cSharpClassName").val();
  }

  function createNewPhoto(filename) {
    var identity = "newPhoto" + ($(".photo--new").length + 1);
    var photo = $("<div>").addClass("photos__photo").addClass("photo").addClass("photo--new").attr("id", identity);

    $("<div>").addClass("photo__thumbnail").css("background-image", "url(/images/temp/" + filename + ")").appendTo(photo);

    var form = $("<div>").addClass("photo__form form").appendTo(photo);

    createIsCoverCheckboxField(identity).appendTo(form);
    createPositionNumericTextBoxField(identity).appendTo(form);
    createButtons(identity).appendTo(form);

    $("<input>").attr("id", identity + "Filename").attr("name", identity + "Filename").attr("type", "hidden").val(filename).appendTo(form);
    return photo;
  }

  function createIsCoverCheckboxField(identity) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: "_" + identity + "IsCover",
        text: "Is cover",
        value: 0
      }
    ).appendTo(field);
    return field;
  }

  function createPositionNumericTextBoxField(identity) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: "Position" }).appendTo(field);
    platformus.controls.numericTextBox.create(
      {
        identity: "_" + identity + "Position",
        value: "0"
      }
    ).appendTo(field);

    platformus.controls.numericTextBox.createNumericButtons().appendTo(field);
    return field;
  }

  function createButtons(identity) {
    var buttons = $("<div>").addClass("form__buttons").addClass("form__buttons--minor").addClass("buttons");

    createRemoveButton(identity).appendTo(buttons);
    return buttons;
  }

  function createRemoveButton(identity) {
    return $("<button>")
      .addClass("buttons__button")
      .addClass("buttons__button--minor")
      .addClass("button")
      .addClass("button--negative")
      .addClass("button--minor")
      .attr("type", "button")
      .html("Remove")
      .click(
        function () {
          platformus.ui.removePhoto(identity);
        }
      );
  }
})(window.platformus = window.platformus || {});