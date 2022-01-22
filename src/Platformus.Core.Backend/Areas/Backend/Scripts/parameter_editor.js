// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditor = platformus.parameterEditor || [];
  platformus.parameterEditor.load = function (code, url) {
    $.get(
      url,
      function (result) {
        $("#" + code + "ParameterEditor")
          .html(result)
          .find("[data-parameter-code]")
          .find("input")
          .change(function () { onParameterValueChange(code); });

        platformus.ui.initializeJQueryValidation();
        setParameterValues(code);
      }
    );
  };

  function setParameterValues(code) {
    $("#" + code + "Parameters").val().split(';').forEach(function (parameterCodeValuePair) {
      var parameterCode = parameterCodeValuePair.split('=')[0];
      var parameterValue = parameterCodeValuePair.split('=')[1];

      $("#parameter" + parameterCode).val(parameterValue);
    });
  }

  function onParameterValueChange(code) {
    var parameters = "";

    $("#" + code + "ParameterEditor").find("[data-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.find("input").val();

        if (value) {
          if (parameters) {
            parameters += ";";
          }

          parameters += element.data("parameterCode") + "=" + value;
        }
      }
    );

    $("#" + code + "Parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});