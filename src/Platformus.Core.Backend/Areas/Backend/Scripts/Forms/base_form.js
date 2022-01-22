// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.baseForm = {};
  platformus.forms.baseForm.show = function (url, callback, additionalCssClass) {
    $("body").addClass("prevent-scroll");

    var modal = createModal().appendTo($(document.body)).animate({ opacity: 1 }, "fast");

    platformus.forms.activeForm = createForm(additionalCssClass).appendTo(modal);
    loadForm(url, callback);
    return false;
  };

  platformus.forms.baseForm.hideAndRemove = function () {
    $(".pop-up-modal").animate(
      { opacity: 0 },
      "fast",
      function () {
        $(this).remove();
        $("body").removeClass("prevent-scroll");
      }
    );

    return false;
  };

  function createModal() {
    return $("<div>").addClass("pop-up-modal");
  }

  function createForm(additionalCssClass) {
    var form = $("<div>").addClass("pop-up-form");

    if (additionalCssClass)
      form.addClass(additionalCssClass);

    return form;
  }

  function loadForm(url, callback) {
    $.ajax(
      {
        url: url,
        type: "GET",
        cache: false,
        success: function (result) {
          platformus.forms.activeForm.html(result);

          if (callback) {
            callback();
          }
        }
      }
    );
  }
})(window.platformus = window.platformus || {});