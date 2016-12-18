// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.baseForm = {};
  platformus.forms.baseForm.show = function (url, callback, additionalCssClass) {
    var modal = createModal();

    showOverlay(modal);

    var form = createForm(additionalCssClass);

    showOverlay(form);
    positionForm(form);
    loadForm(
      form,
      url,
      function () {
        positionForm(form);

        if (callback != null) {
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
    var modal = $("<div>").addClass("pop-up-modal").appendTo($(document.body));

    platformus.forms.activeModal = modal;
    return modal;
  }

  function createForm(additionalCssClass) {
    var form = $("<div>");

    if (additionalCssClass != null)
      form.addClass(additionalCssClass);

    form.addClass("pop-up-form").appendTo($(document.body));
    platformus.forms.activeForm = form;
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