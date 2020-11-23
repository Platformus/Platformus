// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.propertyDataTypeIdChanged = function () {
    var propertyDataTypeId = getSelectedPropertyDataTypeId();

    if (platformus.string.isNullOrEmpty(propertyDataTypeId)) {
      $("#isPropertyLocalizable").hide();
      $("#isPropertyVisibleInList").hide();
    }

    else {
      if (getDataTypeById(propertyDataTypeId).storageDataType === "string") {
        $("#isPropertyLocalizable").show();
      }

      else {
        $("#isPropertyLocalizable").hide();
      }

      $("#isPropertyVisibleInList").show();
    }

    platformus.parameterEditors.sync(getDataTypeById(propertyDataTypeId));
  };

  platformus.ui.relationClassIdChanged = function () {
    var relationClassId = getSelectedRelationClassId();

    if (platformus.string.isNullOrEmpty(relationClassId)) {
      $("#isRelationSingleParent").hide();
      $("#minRelatedObjectsNumber").parent().hide();
      $("#maxRelatedObjectsNumber").parent().hide();
    }

    else {
      $("#isRelationSingleParent").show();
      $("#minRelatedObjectsNumber").parent().show();
      $("#maxRelatedObjectsNumber").parent().show();
    }
  };

  platformus.ui.isRelationSingleParentChanged = function () {
    var isRelationSingleParent = $("#isRelationSingleParent").val();

    if (isRelationSingleParent) {
      $("#minRelatedObjectsNumber").parent().hide();
      $("#maxRelatedObjectsNumber").parent().hide();
    }

    else {
      $("#minRelatedObjectsNumber").parent().show();
      $("#maxRelatedObjectsNumber").parent().show();
    }
  };

  function getSelectedPropertyDataTypeId() {
    return $("#propertyDataTypeId").val();
  }

  function getSelectedRelationClassId() {
    return $("#relationClassId").val();
  }

  function getDataTypeById(dataTypeId) {
    for (var i = 0; i < dataTypes.length; i++) {
      if (dataTypes[i].id == dataTypeId) {
        return dataTypes[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});

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

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.tinyMceFileBrowserCallback = function (fieldId, url, type, wnd) {
    platformus.forms.fileSelectorForm.show(
      function (filename) {
        wnd.document.getElementById(fieldId).value = "/files/" + filename;
      }
    );
  };
})(window.platformus = window.platformus || {});

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