// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.sync = function (cSharpClassName) {
    var dataSourceParameterEditors = $("#dataSourceParameterEditors").html(platformus.string.empty);

    dataSourceParameterEditors.html(platformus.string.empty);

    var dataSource = getDataSource(cSharpClassName);

    $("<div>").addClass("form__description").html(dataSource.description).appendTo(dataSourceParameterEditors);

    for (var i = 0; i < dataSource.dataSourceParameterGroups.length; i++) {
      var group = $("<h2>").addClass("form__title").html(dataSource.dataSourceParameterGroups[i].name).appendTo(dataSourceParameterEditors);

      for (var j = 0; j < dataSource.dataSourceParameterGroups[i].dataSourceParameters.length; j++) {
        var dataSourceParameterEditor = platformus.dataSourceParameterEditors[dataSource.dataSourceParameterGroups[i].dataSourceParameters[j].javaScriptEditorClassName];

        if (dataSourceParameterEditor != null) {
          var f = dataSourceParameterEditor["create"];

          if (f != null) {
            f.call(this, dataSourceParameterEditors, dataSource.dataSourceParameterGroups[i].dataSourceParameters[j]);
          }
        }
      }
    }

    platformus.ui.initializeJQueryValidation();
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