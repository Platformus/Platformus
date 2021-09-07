// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.base = {};
  platformus.parameterEditors.base.getIdentity = function (parameter) {
    return "parameter" + parameter.code;
  };

  platformus.parameterEditors.base.getValue = function (parameter) {
    if (!$("#parameters").val()) {
      return parameter.defaultValue;
    }

    var parameter = $("#parameters")
      .val()
      .split(";")
      .find(p => p.split("=")[0] == parameter.code);

    return parameter ? parameter.split("=")[1] : null;
  };

  platformus.parameterEditors.base.changed = function () {
    var parameters = "";

    $("[data-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (value) {
          if (parameters) {
            parameters += ";";
          }

          parameters += element.attr("data-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});