// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataTypeParameterEditors = platformus.dataTypeParameterEditors || [];
  platformus.dataTypeParameterEditors.checkbox = {};
  platformus.dataTypeParameterEditors.checkbox.create = function (container, dataTypeParameter) {
    createField(dataTypeParameter).appendTo(container);
  };

  function createField(dataTypeParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: getIdentity(dataTypeParameter),
        text: dataTypeParameter.name,
        value: dataTypeParameter.value
      }
    ).change(platformus.dataTypeParameterEditors.base.dataTypeParameterChanged).appendTo(field);

    return field;
  }

  function getIdentity(dataTypeParameter) {
    return "dataTypeParameter" + dataTypeParameter.id;
  }
})(window.platformus = window.platformus || {});