// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.productProviderParameterEditors = platformus.productProviderParameterEditors || [];
  platformus.productProviderParameterEditors.base = {};
  platformus.productProviderParameterEditors.base.getIdentity = function (productProviderParameter) {
    return "productProviderParameter" + productProviderParameter.code;
  };

  platformus.productProviderParameterEditors.base.getValue = function (productProviderParameter) {
    if (platformus.string.isNullOrEmpty($("#parameters").val())) {
      return productProviderParameter.defaultValue;
    }

    var parameters = $("#parameters").val().split(';');

    for (var i = 0; i < parameters.length; i++) {
      var code = parameters[i].split("=")[0];
      var value = parameters[i].split("=")[1];
     
      if (code == productProviderParameter.code) {
        return value;
      }
    }

    return null;
  };

  platformus.productProviderParameterEditors.base.changed = function () {
    var parameters = platformus.string.empty;

    $("[data-product-provider-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (!platformus.string.isNullOrEmpty(value)) {
          if (!platformus.string.isNullOrEmpty(parameters))
            parameters += ";";

          parameters += element.attr("data-product-provider-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});