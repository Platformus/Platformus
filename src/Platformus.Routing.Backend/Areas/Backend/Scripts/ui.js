// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.microcontrollerCSharpClassNameChanged = function () {
    var cSharpClassName = getSelectedMicrocontrollerCSharpClassName();

    platformus.microcontrollerParameterEditors.sync(cSharpClassName);
  };

  platformus.ui.dataSourceCSharpClassNameChanged = function () {
    var cSharpClassName = getSelectedDataSourceCSharpClassName();

    platformus.dataSourceParameterEditors.sync(cSharpClassName);
  };

  function getSelectedMicrocontrollerCSharpClassName() {
    return $("#cSharpClassName").val();
  }

  function getSelectedDataSourceCSharpClassName() {
    return $("#cSharpClassName").val();
  }
})(window.platformus = window.platformus || {});