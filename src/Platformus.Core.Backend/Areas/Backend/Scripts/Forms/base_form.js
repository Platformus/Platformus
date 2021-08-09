// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.baseForm = {};
  platformus.forms.baseForm.show = function (url, callback, additionalCssClass) {
    platformus.forms.activeModal = createModal().appendTo($(document.body));
    showOverlay(platformus.forms.activeModal);
    platformus.forms.activeForm = createForm(additionalCssClass).appendTo($(document.body));
    showOverlay(platformus.forms.activeForm);
    positionForm(platformus.forms.activeForm);
    loadForm(
      platformus.forms.activeForm,
      url,
      function () {
        positionForm(platformus.forms.activeForm);

        if (callback) {
          callback();
        }
      }
    );

    return false;
  };

  platformus.forms.baseForm.hideAndRemove = function () {
    hideAndRemoveOverlay(platformus.forms.activeModal);
    hideAndRemoveOverlay(platformus.forms.activeForm);
    return false;
  };

  function createModal() {
    return $("<div>").addClass("pop-up-modal");
  }

  function createForm(additionalCssClass) {
    var form = $("<div>").addClass("pop-up-form");

    if (additionalCssClass != null)
      form.addClass(additionalCssClass);

    return form;
  }

  function positionForm(form) {
    form.css(
      {
        left: $(window).width() / 2 - form.outerWidth() / 2,
        top: $(window).height() / 2 - form.outerHeight() / 2
      }
    );
  }

  function loadForm(form, url, callback) {
    $.ajax(
      {
        url: url,
        type: "GET",
        cache: false,
        success: function (result) {
          form.html(result);
          callback();
        }
      }
    );
  }

  function showOverlay(overlay) {
    overlay.animate({ opacity: 1 }, "fast");
  }

  function hideAndRemoveOverlay(overlay) {
    overlay.animate({ opacity: 0 }, "fast", function () { $(this).remove(); });
  }
})(window.platformus = window.platformus || {});