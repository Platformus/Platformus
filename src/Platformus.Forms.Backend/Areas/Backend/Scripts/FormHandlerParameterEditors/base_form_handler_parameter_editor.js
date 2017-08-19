// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.formHandlerParameterEditors = platformus.formHandlerParameterEditors || [];
  platformus.formHandlerParameterEditors.base = {};
  platformus.formHandlerParameterEditors.base.getIdentity = function (formHandlerParameter) {
    return "formHandlerParameter" + formHandlerParameter.code;
  };

  platformus.formHandlerParameterEditors.base.getValue = function (formHandlerParameter) {
    if (platformus.string.isNullOrEmpty($("#parameters").val())) {
      return formHandlerParameter.defaultValue;
    }

    var parameters = $("#parameters").val().split(';');

    for (var i = 0; i < parameters.length; i++) {
      var code = parameters[i].split("=")[0];
      var value = parameters[i].split("=")[1];

      if (code == formHandlerParameter.code) {
        return value;
      }
    }

    return null;
  };

  platformus.formHandlerParameterEditors.base.changed = function () {
    var parameters = platformus.string.empty;

    $("[data-form-handler-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (!platformus.string.isNullOrEmpty(value)) {
          if (!platformus.string.isNullOrEmpty(parameters))
            parameters += ";";

          parameters += element.attr("data-form-handler-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});