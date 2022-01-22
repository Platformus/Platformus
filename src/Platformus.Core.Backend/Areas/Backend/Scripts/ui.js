// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        $(document.body).on("click", ".master-detail__edge", onEdgeClick);
        $(document.body).on("click", ".burger-button", onBurgerButtonClick);
        $(document.body).on("click", ".menu-group__title", onMenuGroupTitleClick);
        syncCollapsedMaster();
        syncExpandedMenuGroups();
      },
      priority: 0
    }
  );

  function onEdgeClick() {
    $(".master-detail__master").not(".master-detail__master--secondary").toggleClass("master-detail__master--collapsed");
    $.cookie("collapse_master", $(".master-detail__master").not(".master-detail__master--secondary").hasClass("master-detail__master--collapsed"));
    syncExpandedMenuGroups();
  }

  function onBurgerButtonClick() {
    $(".burger-button").toggleClass("burger-button--active");
    $(".master-detail__master").toggleClass("master-detail__master--expanded");
  }

  function onMenuGroupTitleClick() {
    var menuGroup = $(this).closest(".menu-group");
    var menu = menuGroup.closest(".menu");

    menuGroup.toggleClass("menu-group--expanded");

    if (menuGroup.hasClass("menu-group--expanded")) {
      addToExpandedMenuGroups(menu.data("code"), menuGroup.data("code"));
      menuGroup.find(".menu-group__menu-items").slideDown("fast");
    }

    else {
      removeFromExpandedMenuGroups(menu.data("code"), menuGroup.data("code"));
      menuGroup.find(".menu-group__menu-items").slideUp("fast");
    }
  }

  function syncCollapsedMaster() {
    if ($.cookie("collapse_master") && JSON.parse($.cookie("collapse_master"))) {
      $(".master-detail__master").not(".master-detail__master--secondary").addClass("master-detail__master--collapsed");
    }
  }

  function syncExpandedMenuGroups() {
    $(".menu").each(
      function () {
        var menu = $(this);

        if (!$.cookie(getExpandedMenuGroupsCookieKey(menu.data("code")))) {
          var menuGroup = menu.find(".menu__menu-group").first();

          addToExpandedMenuGroups(menu.data("code"), menuGroup.data("code"));
        }

        var expandedMenuGroups = getExpandedMenuGroups(menu.data("code"));

        expandedMenuGroups.forEach(function (expandedMenuGroup) {
          var menuGroup = $(".menu__menu-group[data-code='" + expandedMenuGroup + "']");
          var menuGroupMenuItems = menuGroup.find(".menu-group__menu-items");

          menuGroup.addClass("menu-group--expanded");
          menuGroupMenuItems.show();
        });
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

    return !$.cookie(key) ? [] : $.cookie(key).split(",");
  }

  function setExpandedMenuGroups(menuCode, expandedMenuGroups) {
    $.cookie(getExpandedMenuGroupsCookieKey(menuCode), expandedMenuGroups.join(), { path: "/" });
  }

  function getExpandedMenuGroupsCookieKey(menuCode) {
    return "expanded_" + menuCode + "_menu_groups";
  }
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