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
      if (getPropertyDataTypeStorageDataType(propertyDataTypeId) == "string") {
        $("#isPropertyLocalizable").show();
      }

      else {
        $("#isPropertyLocalizable").hide();
      }

      $("#isPropertyVisibleInList").show();
    }
  };

  platformus.ui.relationClassIdChanged = function () {
    var relationClassId = getSelectedRelationClassId();

    if (platformus.string.isNullOrEmpty(relationClassId)) {
      $("#isRelationSingleParent").hide();
    }

    else {
      $("#isRelationSingleParent").show();
    }
  };

  function getSelectedPropertyDataTypeId() {
    return $("#propertyDataTypeId").val();
  }

  function getPropertyDataTypeStorageDataType(propertyDataTypeId) {
    for (var i = 0; i < dataTypes.length; i++) {
      if (dataTypes[i].id == propertyDataTypeId) {
        return dataTypes[i].storageDataType;
      }
    }

    return platformus.string.empty;
  }

  function getSelectedRelationClassId() {
    return $("#relationClassId").val();
  }
})(window.platformus = window.platformus || {});