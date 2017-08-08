// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.base = {};
  platformus.endpointParameterEditors.base.getIdentity = function (endpointParameter) {
    return "endpointParameter" + endpointParameter.code;
  };

  platformus.endpointParameterEditors.base.getValue = function (endpointParameter) {
    if (platformus.string.isNullOrEmpty($("#parameters").val())) {
      return endpointParameter.defaultValue;
    }

    var parameters = $("#parameters").val().split(';');

    for (var i = 0; i < parameters.length; i++) {
      var code = parameters[i].split("=")[0];
      var value = parameters[i].split("=")[1];

      if (code == endpointParameter.code) {
        return value;
      }
    }

    return null;
  };

  platformus.endpointParameterEditors.base.changed = function () {
    var parameters = platformus.string.empty;

    $("[data-endpoint-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (!platformus.string.isNullOrEmpty(value)) {
          if (!platformus.string.isNullOrEmpty(parameters))
            parameters += ";";

          parameters += element.attr("data-endpoint-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});