// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.base = {};
  platformus.dataTypeParameterEditors.base.createLabel = function (dataTypeParameter) {
    return $("<label>").addClass("field__label").addClass("label").html(dataTypeParameter.name);
  };

  platformus.dataTypeParameterEditors.base.dataTypeParameterValue = function (dataTypeParameterCode) {
    var parameters = $("#parameters").val().split(';');

    for (var i = 0; i < parameters.length; i++) {
      var code = parameters[i].split("=")[0];
      var value = parameters[i].split("=")[1];

      if (code == dataTypeParameterCode) {
        return value;
      }
    }

    return null;
  };

  platformus.dataTypeParameterEditors.base.dataTypeParameterChanged = function () {
    var parameters = platformus.string.empty;
    $("[data-datatype-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (!platformus.string.isNullOrEmpty(value)) {
          if (!platformus.string.isNullOrEmpty(parameters))
            parameters += ";";

          parameters += element.attr("data-datatype-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});