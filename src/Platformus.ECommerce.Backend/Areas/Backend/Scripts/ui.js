// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.productProviderCSharpClassNameChanged = function () {
    platformus.parameterEditor.load("productProvider", "/backend/core/parametereditor?csharpclassname=" + $("#productProviderCSharpClassName").val());
  };

  platformus.ui.addPosition = function () {
    platformus.forms.productSelectorForm.show(
      null,
      function (productId) {
        $.getJSON(
          "/backend/ecommerce/product",
          { id: productId },
          function (product) {
            positions.push({
              id: 0,
              product: product,
              price: product.price,
              quantity: 1.0,
              subtotal: product.price
            });

            syncPositions();
          }
        );
      }
    );
  };

  platformus.ui.onPositionQuantityChange = function (numericBox) {
    var row = $(numericBox).closest(".table__row");
    var quantity = parseFloat($(numericBox).find("input").val());
    var position = positions[row.index() - 1];

    position.quantity = quantity;
    position.subtotal = position.price * position.quantity;
    syncPositions();
  };

  platformus.ui.removePosition = function (button) {
    var row = $(button).closest(".table__row");

    positions.splice(row.index() - 1, 1);
    syncPositions();
    return false;
  };

  function syncPositions() {
    $.ajax(
      {
        type: "POST",
        url: "/backend/orders/positions",
        contentType: "application/json",
        data: JSON.stringify(positions),
        success: function (result) {
          $("table").replaceWith(result);
        }
      }
    );
  }
})(window.platformus = window.platformus || {});