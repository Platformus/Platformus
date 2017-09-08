// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.formHandlerParameterEditors = platformus.formHandlerParameterEditors || [];
  platformus.formHandlerParameterEditors.sync = function (cSharpClassName) {
    var formHandlerParameterEditors = $("#formHandlerParameterEditors").html(platformus.string.empty);

    formHandlerParameterEditors.html(platformus.string.empty);

    var formHandler = getFormHandler(cSharpClassName);

    $("<div>").addClass("form__description").html(formHandler.description).appendTo(formHandlerParameterEditors);

    for (var i = 0; i < formHandler.formHandlerParameterGroups.length; i++) {
      var group = $("<h2>").addClass("form__title").html(formHandler.formHandlerParameterGroups[i].name).appendTo(formHandlerParameterEditors);

      for (var j = 0; j < formHandler.formHandlerParameterGroups[i].formHandlerParameters.length; j++) {
        var f = platformus.formHandlerParameterEditors[formHandler.formHandlerParameterGroups[i].formHandlerParameters[j].javaScriptEditorClassName]["create"];

        f.call(this, formHandlerParameterEditors, formHandler.formHandlerParameterGroups[i].formHandlerParameters[j]);
      }
    }

    platformus.ui.initializeJQueryValidation();
  };

  function getFormHandler(cSharpClassName) {
    for (var i = 0; i < formHandlers.length; i++) {
      if (formHandlers[i].cSharpClassName == cSharpClassName) {
        return formHandlers[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});