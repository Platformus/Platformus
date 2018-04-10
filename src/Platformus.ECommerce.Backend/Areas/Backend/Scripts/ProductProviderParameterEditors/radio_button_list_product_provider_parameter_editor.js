// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.productProviderParameterEditors = platformus.productProviderParameterEditors || [];
  platformus.productProviderParameterEditors.radioButtonList = {};
  platformus.productProviderParameterEditors.radioButtonList.create = function (container, productProviderParameter) {
    createField(productProviderParameter).appendTo(container);
  };

  function createField(productProviderParameter) {
    var identity = platformus.productProviderParameterEditors.base.getIdentity(productProviderParameter);
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ identity: identity, text: productProviderParameter.name }).appendTo(field);
    platformus.controls.radioButtonList.create(
      {
        identity: identity,
        options: productProviderParameter.options,
        value: platformus.productProviderParameterEditors.base.getValue(productProviderParameter)
      }
    ).attr("data-product-provider-parameter-code", productProviderParameter.code)
      .change(platformus.productProviderParameterEditors.base.changed)
      .appendTo(field);

    return field;
  }
})(window.platformus = window.platformus || {});