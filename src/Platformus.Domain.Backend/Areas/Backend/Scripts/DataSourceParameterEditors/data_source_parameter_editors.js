// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.sync = function (cSharpClassName) {
    var dataSourceParameterEditors = $("#dataSourceParameterEditors").html(platformus.string.empty);

    dataSourceParameterEditors.html(platformus.string.empty);

    var dataSource = getDataSource(cSharpClassName);

    for (var i = 0; i < dataSource.dataSourceParameterGroups.length; i++) {
      var group = $("<h2>").addClass("form_title").html(dataSource.dataSourceParameterGroups[i].name).appendTo(dataSourceParameterEditors);

      for (var j = 0; j < dataSource.dataSourceParameterGroups[i].dataSourceParameters.length; j++) {
        var f = platformus.dataSourceParameterEditors[dataSource.dataSourceParameterGroups[i].dataSourceParameters[j].javaScriptEditorClassName]["create"];

        f.call(this, dataSourceParameterEditors, dataSource.dataSourceParameterGroups[i].dataSourceParameters[j]);
      }
    }
  };

  function getDataSource(cSharpClassName) {
    for (var i = 0; i < dataSources.length; i++) {
      if (dataSources[i].cSharpClassName == cSharpClassName) {
        return dataSources[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});