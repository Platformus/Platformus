// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.endpointCSharpClassNameChanged = function () {
    platformus.parameterEditors.sync(getEndpointByCSharpClassName(getSelectedEndpointCSharpClassName()));
  };

  platformus.ui.dataSourceCSharpClassNameChanged = function () {
    platformus.parameterEditors.sync(getDataSourceByCSharpClassName(getSelectedDataSourceCSharpClassName()));
  };

  function getSelectedEndpointCSharpClassName() {
    return $("#cSharpClassName").val();
  }

  function getSelectedDataSourceCSharpClassName() {
    return $("#cSharpClassName").val();
  }

  function getEndpointByCSharpClassName(cSharpClassName) {
    for (var i = 0; i < endpoints.length; i++) {
      if (endpoints[i].cSharpClassName === cSharpClassName) {
        return endpoints[i];
      }
    }

    return null;
  }

  function getDataSourceByCSharpClassName(cSharpClassName) {
    for (var i = 0; i < dataSources.length; i++) {
      if (dataSources[i].cSharpClassName === cSharpClassName) {
        return dataSources[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});