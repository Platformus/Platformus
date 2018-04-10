// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.productProviderParameterEditors = platformus.productProviderParameterEditors || [];
  platformus.productProviderParameterEditors.sync = function (cSharpClassName) {
    var productProviderParameterEditors = $("#productProviderParameterEditors").html(platformus.string.empty);

    productProviderParameterEditors.html(platformus.string.empty);

    var productProvider = getProductProvider(cSharpClassName);

    $("<div>").addClass("form__description").html(productProvider.description).appendTo(productProviderParameterEditors);

    for (var i = 0; i < productProvider.productProviderParameterGroups.length; i++) {
      var group = $("<h2>").addClass("form__title").html(productProvider.productProviderParameterGroups[i].name).appendTo(productProviderParameterEditors);

      for (var j = 0; j < productProvider.productProviderParameterGroups[i].productProviderParameters.length; j++) {
        var productProviderParameterEditor = platformus.productProviderParameterEditors[productProvider.productProviderParameterGroups[i].productProviderParameters[j].javaScriptEditorClassName];

        if (productProviderParameterEditor != null) {
          var f = productProviderParameterEditor["create"];

          if (f != null) {
            f.call(this, productProviderParameterEditors, productProvider.productProviderParameterGroups[i].productProviderParameters[j]);
          }
        }
      }
    }

    platformus.ui.initializeJQueryValidation();
  };

  function getProductProvider(cSharpClassName) {
    for (var i = 0; i < productProviders.length; i++) {
      if (productProviders[i].cSharpClassName == cSharpClassName) {
        return productProviders[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});