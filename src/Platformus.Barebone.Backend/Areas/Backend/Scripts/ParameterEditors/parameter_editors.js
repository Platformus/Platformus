// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.sync = function (parameterizableTarget) {
    var parameterEditors = $("#parameterEditors");

    parameterEditors.html(platformus.string.empty);

    if (parameterizableTarget == null) {
      return;
    }

    $("<div>").addClass("form__description").html(parameterizableTarget.description).appendTo(parameterEditors);

    for (var i = 0; i < parameterizableTarget.parameterGroups.length; i++) {
      var group = $("<h2>").addClass("form__title").html(parameterizableTarget.parameterGroups[i].name).appendTo(parameterEditors);

      for (var j = 0; j < parameterizableTarget.parameterGroups[i].parameters.length; j++) {
        var parameterEditor = platformus.parameterEditors[parameterizableTarget.parameterGroups[i].parameters[j].javaScriptEditorClassName];

        if (parameterEditor != null) {
          var f = parameterEditor["create"];

          if (f != null) {
            f.call(this, parameterEditors, parameterizableTarget.parameterGroups[i].parameters[j]);
          }
        }
      }
    }

    platformus.ui.initializeJQueryValidation();
  };
})(window.platformus = window.platformus || {});