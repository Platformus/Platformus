// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function() {
        $(".menu__group-header").click(
          function () {
            var menuGroup = $(this).parent();
            var menuGroupContent = menuGroup.find(".menu__group-content");

            if (menuGroupContent.is(":visible")) {
              removeFromExpandedMenuGroups(menuGroup.data("code"));
              menuGroupContent.slideUp("fast");
              
            }

            else {
              addToExpandedMenuGroups(menuGroup.data("code"));
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
    if ($.cookie("expanded-menu-groups") == null) {
      $(".menu").each(
        function () {
          var menuGroup = $(this).find(".menu__group").first();
          var menuGroupContent = menuGroup.find(".menu__group-content");

          addToExpandedMenuGroups(menuGroup.data("code"));
          menuGroupContent.show();
        }
      );
    }

    else {
      var expandedMenuGroups = getExpandedMenuGroups();

      for (var i = 0; i < expandedMenuGroups.length; i++) {
        $(".menu__group[data-code='" + expandedMenuGroups[i] + "']").find(".menu__group-content").show();
      }
    }
  }

  function addToExpandedMenuGroups(code) {
    var expandedMenuGroups = getExpandedMenuGroups();

    if (expandedMenuGroups.indexOf(code) == -1) {
      expandedMenuGroups.push(code);
    }

    setExpandedMenuGroups(expandedMenuGroups);
  }

  function removeFromExpandedMenuGroups(code) {
    var expandedMenuGroups = getExpandedMenuGroups();
    var index = expandedMenuGroups.indexOf(code);

    if (index != -1) {
      expandedMenuGroups.splice(index, 1);
    }

    setExpandedMenuGroups(expandedMenuGroups);
  }

  function getExpandedMenuGroups() {
    return $.cookie("expanded-menu-groups") == null ? [] : $.cookie("expanded-menu-groups").split(",");
  }

  function setExpandedMenuGroups(expandedMenuGroups) {
    $.cookie("expanded-menu-groups", expandedMenuGroups.join(), { path: "/backend/" });
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