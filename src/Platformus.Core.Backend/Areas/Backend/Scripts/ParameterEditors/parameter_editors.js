// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.sync = function (target) {
    var parameterEditors = $("#parameterEditors");

    parameterEditors.html("");

    if (!target) {
      return;
    }

    if (target.description) {
      $("<div>").addClass("form__description").html(target.description).appendTo(parameterEditors);
    }

    target.parameterGroups.forEach(function (parameterGroup) {
      if (parameterGroup.name) {
        $("<h2>").addClass("form__title").html(parameterGroup.name).appendTo(parameterEditors);
      }

      parameterGroup.parameters.forEach(function (parameter) {
        var parameterEditor = platformus.parameterEditors[parameter.javaScriptEditorClassName];

        if (parameterEditor != null) {
          var f = parameterEditor["create"];

          if (f) {
            f.call(this, parameterEditors, parameter);
          }
        }
      });
    });

    platformus.ui.initializeJQueryValidation();
  };
})(window.platformus = window.platformus || {});