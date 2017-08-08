// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.textBox = {};
  platformus.dataTypeParameterEditors.textBox.create = function (container, dataTypeParameter) {
    createField(dataTypeParameter).appendTo(container);
  };

  function createField(dataTypeParameter) {
    var identity = platformus.dataTypeParameterEditors.base.getIdentity(dataTypeParameter);
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ identity: identity, text: dataTypeParameter.name }).appendTo(field);
    platformus.controls.textBox.create({ identity: identity, value: dataTypeParameter.value }).appendTo(field);
    return field;
  }
})(window.platformus = window.platformus || {});