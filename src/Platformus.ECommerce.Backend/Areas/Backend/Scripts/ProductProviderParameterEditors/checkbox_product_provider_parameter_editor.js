// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.productProviderParameterEditors = platformus.productProviderParameterEditors || [];
  platformus.productProviderParameterEditors.checkbox = {};
  platformus.productProviderParameterEditors.checkbox.create = function (container, productProviderParameter) {
    createField(productProviderParameter).appendTo(container);
  };

  function createField(productProviderParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: platformus.productProviderParameterEditors.base.getIdentity(productProviderParameter),
        text: productProviderParameter.name,
        value: platformus.productProviderParameterEditors.base.getValue(productProviderParameter)
      }
    ).attr("data-product-provider-parameter-code", productProviderParameter.code)
      .change(platformus.productProviderParameterEditors.base.changed)
      .appendTo(field);

    return field;
  }
})(window.platformus = window.platformus || {});