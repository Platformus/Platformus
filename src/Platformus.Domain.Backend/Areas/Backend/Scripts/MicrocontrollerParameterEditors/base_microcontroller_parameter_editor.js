// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.microcontrollerParameterEditors = platformus.microcontrollerParameterEditors || [];
  platformus.microcontrollerParameterEditors.base = {};
  platformus.microcontrollerParameterEditors.base.createLabel = function (microcontrollerParameter) {
    return $("<label>").addClass("field__label").addClass("label").html(microcontrollerParameter.name);
  };

  platformus.microcontrollerParameterEditors.base.microcontrollerParameterValue = function (microcontrollerParameter) {
    if (platformus.string.isNullOrEmpty($("#parameters").val())) {
      return microcontrollerParameter.defaultValue;
    }

    var parameters = $("#parameters").val().split(';');

    for (var i = 0; i < parameters.length; i++) {
      var code = parameters[i].split("=")[0];
      var value = parameters[i].split("=")[1];

      if (code == microcontrollerParameter.code) {
        return value;
      }
    }

    return null;
  };

  platformus.microcontrollerParameterEditors.base.microcontrollerParameterChanged = function () {
    var parameters = platformus.string.empty;

    $("[data-microcontroller-parameter-code]").each(
      function (index, element) {
        element = $(element);

        var value = element.val();

        if (!platformus.string.isNullOrEmpty(value)) {
          if (!platformus.string.isNullOrEmpty(parameters))
            parameters += ";";

          parameters += element.attr("data-microcontroller-parameter-code") + "=" + value;
        }
      }
    );

    $("#parameters").val(parameters);
  };
})(window.platformus = window.platformus || {});