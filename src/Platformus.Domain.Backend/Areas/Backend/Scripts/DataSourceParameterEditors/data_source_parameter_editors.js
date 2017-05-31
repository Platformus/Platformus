// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.sync = function (dataSources, cSharpClassName) {
    var dataSourceParameterEditors = $("#dataSourceParameterEditors").html(platformus.string.empty);

    dataSourceParameterEditors.html(platformus.string.empty);

    var dataSource = getDataSource(dataSources, cSharpClassName);

    for (var i = 0; i < dataSource.dataSourceParameters.length; i++) {
      platformus.dataSourceParameterEditors.temp.create(dataSourceParameterEditors, dataSource.dataSourceParameters[i]);
    }
  };

  function getDataSource(dataSources, cSharpClassName) {
    for (var i = 0; i < dataSources.length; i++) {
      if (dataSources[i].cSharpClassName == cSharpClassName) {
        return dataSources[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});