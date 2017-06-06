// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.base = {};
  platformus.dataTypeParameterEditors.base.createLabel = function (dataTypeParameter) {
    return $("<label>").addClass("field__label").addClass("label").html(dataTypeParameter.name);
  };
})(window.platformus = window.platformus || {});