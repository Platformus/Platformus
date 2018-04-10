// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function() {
        $(".menu__group-header").click(
          function () {
            var menu = $(this).parent().parent();
            var menuGroup = $(this).parent();
            var menuGroupHeader = menuGroup.find(".menu__group-header");
            var menuGroupContent = menuGroup.find(".menu__group-content");

            if (menuGroupContent.is(":visible")) {
              removeFromExpandedMenuGroups(menu.data("code"), menuGroup.data("code"));
              menuGroupHeader.removeClass("menu__group-header--expanded").addClass("menu__group-header--collapsed");
              menuGroupContent.slideUp("fast");
              
            }

            else {
              addToExpandedMenuGroups(menu.data("code"), menuGroup.data("code"));
              menuGroupHeader.removeClass("menu__group-header--collapsed").addClass("menu__group-header--expanded");
              menuGroupContent.slideDown("fast");
            }
          }
        );

        syncExpandedMenuGroups();
      },
      priority: 0
    }
  );

  function syncExpandedMenuGroups() {
    $(".menu__group-header").removeClass("menu__group-header--expanded").addClass("menu__group-header--collapsed");
    $(".menu").each(
      function () {
        var menu = $(this);

        if ($.cookie(getExpandedMenuGroupsCookieKey(menu.data("code"))) == null) {
          var menuGroup = menu.find(".menu__group").first();

          addToExpandedMenuGroups(menu.data("code"), menuGroup.data("code"));
        }

        var expandedMenuGroups = getExpandedMenuGroups(menu.data("code"));

        for (var i = 0; i < expandedMenuGroups.length; i++) {
          var menuGroup = $(".menu__group[data-code='" + expandedMenuGroups[i] + "']");
          var menuGroupHeader = menuGroup.find(".menu__group-header");
          var menuGroupContent = menuGroup.find(".menu__group-content");

          menuGroupHeader.removeClass("menu__group-header--collapsed").addClass("menu__group-header--expanded");
          menuGroupContent.show();
        }
      }
    );
  }

  function addToExpandedMenuGroups(menuCode, menuGroupCode) {
    var expandedMenuGroups = getExpandedMenuGroups(menuCode);

    if (expandedMenuGroups.indexOf(menuGroupCode) == -1) {
      expandedMenuGroups.push(menuGroupCode);
    }

    setExpandedMenuGroups(menuCode, expandedMenuGroups);
  }

  function removeFromExpandedMenuGroups(menuCode, menuGroupCode) {
    var expandedMenuGroups = getExpandedMenuGroups(menuCode);
    var index = expandedMenuGroups.indexOf(menuGroupCode);

    if (index != -1) {
      expandedMenuGroups.splice(index, 1);
    }

    setExpandedMenuGroups(menuCode, expandedMenuGroups);
  }

  function getExpandedMenuGroups(menuCode) {
    var key = getExpandedMenuGroupsCookieKey(menuCode);

    return $.cookie(key) == null ? [] : $.cookie(key).split(",");
  }

  function setExpandedMenuGroups(menuCode, expandedMenuGroups) {
    $.cookie(getExpandedMenuGroupsCookieKey(menuCode), expandedMenuGroups.join(), { path: "/" });
  }

  function getExpandedMenuGroupsCookieKey(menuCode) {
    return "expanded-" + menuCode + "-menu-groups";
  }
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        if ($("form").length == 0) {
          return;
        }

        $(window).on("beforeunload", function () {
          if (platformus.ui.needsSaveConfirmation) {
            return confirm("Your changes might be lost!");
          }
        });

        // TODO: add another controls change events
        $(document.body).on("change", "input, textarea", function () {
          if ($(this).attr("type") != "file") {
            platformus.ui.needsSaveConfirmation = true;
          }
        });

        $(document.body).on("click", "button[type='submit']", function () {
          platformus.ui.needsSaveConfirmation = null;
        });
      },
      priority: 0
    }
  );
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.initializeTinyMce = function (identity) {
    tinymce.init(
      {
        selector: "#" + identity,
        plugins: [
          "advlist anchor autolink charmap code contextmenu fullscreen image insertdatetime link lists media paste preview print searchreplace table visualblocks",
        ],
        menubar: "edit insert view format table tools",
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist | outdent indent | link image",
        statusbar: false,
        convert_urls: false,
        file_browser_callback: platformus.ui.tinyMceFileBrowserCallback
      }
    );
  };
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.initializeJQueryValidation = function () {
    var form = $("form")
      .removeData("validator")
      .removeData("unobtrusiveValidation");

    $.validator.unobtrusive.parse(form);
  };
})(window.platformus = window.platformus || {});