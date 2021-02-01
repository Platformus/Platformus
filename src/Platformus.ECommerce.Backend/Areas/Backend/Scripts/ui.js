// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.catalogCSharpClassNameChanged = function () {
    platformus.parameterEditors.sync(getProductProviderByCSharpClassName(getSelectedCatalogCSharpClassName()));
  };

  platformus.ui.addPosition = function () {
    platformus.forms.productSelectorForm.show(
      null,
      function (productId) {
        $.getJSON(
          "/backend/ecommerce/product",
          { id: productId },
          function (product) {
            createPositionTableRow(product).insertBefore($("#totalRow"));
            updateTotal();
          }
        );
      }
    );
  };

  platformus.ui.onQuantityChange = function (index) {
    updateSubtotal(index);
  }

  platformus.ui.removePosition = function (button) {
    $(button).parent().parent().remove();
    updateTotal();
    return false;
  };

  function createPositionTableRow(product) {
    var index = getMaxPositionIndex() + 1;
    var row = createTableRow(index).insertBefore($("#totalRow"));

    createCategoryTableCell(product).appendTo(row);
    createProductTableCell(product, index).appendTo(row);
    createPriceTableCell(product, index).appendTo(row);
    createQuantityTableCell(product, index).appendTo(row);
    createSubtotalTableCell(product, index).appendTo(row);
    createRemovePositionTableCell(product).appendTo(row);
    return row;
  }

  function getMaxPositionIndex() {
    var maxIndex = -1;

    $(".table__row[data-index]").each(function () {
      var index = parseInt($(this).data("index"));

      if (index > maxIndex) {
        maxIndex = index;
      }
    });

    return maxIndex;
  }

  function createCategoryTableCell(product) {
    return createTableCell().html(product.category.name);
  }

  function createProductTableCell(product, index) {
    var cell = createTableCell().html(product.name);

    $("<input>").attr("type", "hidden").attr("name", "positions[" + index + "].product.id").val(product.id).appendTo(cell);
    return cell;
  }

  function createPriceTableCell(product, index) {
    var cell = createTableCell().html(product.price);

    $("<input>").attr("type", "hidden").attr("name", "positions[" + index + "].price").val(product.price).appendTo(cell);
    return cell;
  }

  function createQuantityTableCell(product, index) {
    var cell = createTableCell().addClass("table__cell--controls");

    platformus.controls.numericTextBox.create(
      {
        identity: "positions[" + index + "].quantity",
        value: "1"
      }
    )
      .addClass("table__text-box--numeric")
      .change(function () { updateSubtotal(index) })
      .appendTo(cell);

    platformus.controls.numericTextBox.createNumericButtons()
      .addClass("table__numeric-buttons")
      .appendTo(cell);

    return cell;
  }

  function createSubtotalTableCell(product, index) {
    return createTableCell()
      .attr("data-subtotal", true)
      .html(product.price);
  }

  function createRemovePositionTableCell(product) {
    var cell = createTableCell().addClass("table__buttons buttons");

    createRemoveButton().appendTo(cell);
    return cell;
  }

  function createTableRow(index) {
    return $("<tr>").addClass("table__row").attr("data-index", index);
  }

  function createTableCell() {
    return $("<td>").addClass("table__cell");
  }

  function createRemoveButton() {
    return $("<button>")
      .addClass("table__button")
      .addClass("buttons__button")
      .addClass("buttons__button--minor")
      .addClass("button")
      .addClass("button--negative")
      .addClass("button--minor")
      .addClass("button--delete")
      .attr("type", "button")
      .click(
        function () {
          $(this).parent().parent().remove();
          updateTotal();
        }
      );
  }

  function updateSubtotal(index) {
    $(".table__row[data-index='" + index + "']").find(".table__cell[data-subtotal]").html(getSubtotal(index));
    updateTotal();
  }

  function updateTotal() {
    var total = 0.0;

    $(".table__row[data-index]").each(function () {
      var index = parseInt($(this).data("index"));
      
      total += getSubtotal(index);
    });

    $("#total").html(parseFloat(total));
  }

  function getSubtotal(index) {
    var price = parseFloat($("input[name='positions[" + index + "].price']").val());
    var quantity = parseFloat($("input[name='positions[" + index + "].quantity']").val());

    return price * quantity;
  }

  function getSelectedCatalogCSharpClassName() {
    return $("#cSharpClassName").val();
  }

  function getProductProviderByCSharpClassName(cSharpClassName) {
    for (var i = 0; i < productProviders.length; i++) {
      if (productProviders[i].cSharpClassName === cSharpClassName) {
        return productProviders[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});