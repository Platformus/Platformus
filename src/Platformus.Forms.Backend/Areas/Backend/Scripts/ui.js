// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.formHandlerCSharpClassNameChanged = function () {
    platformus.parameterEditors.sync(getFormHandlerByCSharpClassName(getSelectedCSharpClassName()));
  };

  function getSelectedCSharpClassName() {
    return $("#cSharpClassName").val();
  }

  function getFormHandlerByCSharpClassName(cSharpClassName) {
    for (var i = 0; i < formHandlers.length; i++) {
      if (formHandlers[i].cSharpClassName === cSharpClassName) {
        return formHandlers[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});