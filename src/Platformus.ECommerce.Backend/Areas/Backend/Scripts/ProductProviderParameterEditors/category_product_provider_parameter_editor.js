// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.productProviderParameterEditors = platformus.productProviderParameterEditors || [];
  platformus.productProviderParameterEditors.category = {};
  platformus.productProviderParameterEditors.category.create = function (container, productProviderParameter) {
    createField(productProviderParameter).appendTo(container);
    syncCategoryNames(productProviderParameter);
  };

  function createField(productProviderParameter) {
    var field = $("<div>").addClass("category-product-provider-parameter-editor").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: productProviderParameter.name }).appendTo(field);
    createInput(productProviderParameter).appendTo(field);
    createNames(productProviderParameter).appendTo(field);
    createButtons(productProviderParameter).appendTo(field);
    return field;
  }

  function createInput(productProviderParameter) {
    var identity = "productProviderParameter" + productProviderParameter.code;
    var input = $("<input>")
      .attr("id", identity)
      .attr("type", "hidden")
      .attr("value", platformus.productProviderParameterEditors.base.getValue(productProviderParameter))
      .attr("data-product-provider-parameter-code", productProviderParameter.code)
      .change(
        function () {
          syncCategoryNames(productProviderParameter);
          platformus.productProviderParameterEditors.base.changed();
        }
      );

    if (productProviderParameter.isRequired) {
      input.attr("data-val", true).attr("data-val-required", true);
    }

    return input;
  }

  function createNames(productProviderParameter) {
    var identity = "productProviderParameter" + productProviderParameter.code;
    var names = $("<div>").addClass("category-product-provider-parameter-editor__names").attr("id", identity + "Names");

    if (productProviderParameter.isRequired) {
      names.addClass("category-product-provider-parameter-editor__names--required");
    }

    return names;
  }

  function createButtons(productProviderParameter) {
    var buttons = $("<div>").addClass("form__buttons").addClass("form__buttons--minor").addClass("buttons");

    createButton(productProviderParameter).appendTo(buttons);
    return buttons;
  }

  function createButton(productProviderParameter) {
    var identity = "productProviderParameter" + productProviderParameter.code;

    return $("<button>").addClass("buttons__button").addClass("buttons__button--minor").addClass("button").addClass("button--positive").addClass("button--minor").attr("type", "button").html("Select…").click(
      function () {
        platformus.forms.categorySelectorForm.show(
          $("#" + identity).val(),
          function (categoryId) {
            $("#" + identity).val(categoryId);
            $("#" + identity).trigger("change");
          }
        );
      }
    );
  }

  function syncCategoryNames(productProviderParameter) {
    var identity = "productProviderParameter" + productProviderParameter.code;

    $.get(
      "/backend/ecommerce/getcategoryname?categoryid=" + $("#" + identity).val(),
      function (result) {
        $("#" + identity + "Names").html(result);
      }
    );
  }
})(window.platformus = window.platformus || {});