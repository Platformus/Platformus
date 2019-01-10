// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
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