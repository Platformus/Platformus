// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.formHandlerCSharpClassNameChanged = function () {
    var cSharpClassName = getSelectedFormHandlerCSharpClassName();

    platformus.formHandlerParameterEditors.sync(cSharpClassName);
  };

  function getSelectedFormHandlerCSharpClassName() {
    return $("#cSharpClassName").val();
  }
})(window.platformus = window.platformus || {});