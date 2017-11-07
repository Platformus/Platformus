// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.sync = function (dataTypeId) {
    var dataTypeParameterEditors = $("#dataTypeParameterEditors").html(platformus.string.empty);

    dataTypeParameterEditors.html(platformus.string.empty);

    var dataType = getDataType(dataTypeId);

    if (dataType == null) {
      return;
    }

    for (var i = 0; i < dataType.dataTypeParameters.length; i++) {
      var dataTypeParameterEditor = platformus.dataTypeParameterEditors[dataType.dataTypeParameters[i].javaScriptEditorClassName];

      if (dataTypeParameterEditor != null) {
        var f = dataTypeParameterEditor["create"];

        if (f != null) {
          f.call(this, dataTypeParameterEditors, dataType.dataTypeParameters[i]);
        }
      }
    }
  };

  function getDataType(dataTypeId) {
    for (var i = 0; i < dataTypes.length; i++) {
      if (dataTypes[i].id == dataTypeId) {
        return dataTypes[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});