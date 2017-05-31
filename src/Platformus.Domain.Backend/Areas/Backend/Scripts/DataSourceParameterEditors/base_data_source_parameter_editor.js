// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.base = {};
  platformus.dataSourceParameterEditors.base.createLabel = function (dataSourceParameter) {
    return $("<label>").addClass("field__label").addClass("label").html(dataSourceParameter.name);
  };

  platformus.dataSourceParameterEditors.base.dataSourceParameterValue = function (dataSourceParameterCode) {
    var parameters = $("#parameters").val().split(';');

    for (var i = 0; i < parameters.length; i++) {
      var code = parameters[i].split("=")[0];
      var value = parameters[i].split("=")[1];

      if (code == dataSourceParameterCode) {
        return value;
      }
    }

    return null;
  };

  platformus.dataSourceParameterEditors.base.dataSourceParameterChanged = function () {
    var parameters = platformus.string.empty;
    $("[data-datasource-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (!platformus.string.isNullOrEmpty(value)) {
          if (!platformus.string.isNullOrEmpty(parameters))
            parameters += ";";

          parameters += element.attr("data-datasource-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});