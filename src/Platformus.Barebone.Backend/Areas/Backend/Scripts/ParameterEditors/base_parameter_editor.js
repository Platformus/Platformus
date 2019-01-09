// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.base = {};
  platformus.parameterEditors.base.getIdentity = function (parameter) {
    return "parameter" + parameter.code;
  };

  platformus.parameterEditors.base.getValue = function (parameter) {
    if (platformus.string.isNullOrEmpty($("#parameters").val())) {
      return parameter.defaultValue;
    }

    var parameters = $("#parameters").val().split(';');

    for (var i = 0; i < parameters.length; i++) {
      var code = parameters[i].split("=")[0];
      var value = parameters[i].split("=")[1];
     
      if (code == parameter.code) {
        return value;
      }
    }

    return null;
  };

  platformus.parameterEditors.base.changed = function () {
    var parameters = platformus.string.empty;

    $("[data-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (!platformus.string.isNullOrEmpty(value)) {
          if (!platformus.string.isNullOrEmpty(parameters))
            parameters += ";";

          parameters += element.attr("data-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});