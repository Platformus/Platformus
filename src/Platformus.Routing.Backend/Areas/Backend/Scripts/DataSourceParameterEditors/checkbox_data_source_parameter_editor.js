// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.checkbox = {};
  platformus.dataSourceParameterEditors.checkbox.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
  };

  function createField(dataSourceParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: platformus.dataSourceParameterEditors.base.getIdentity(dataSourceParameter),
        text: dataSourceParameter.name,
        value: platformus.dataSourceParameterEditors.base.getValue(dataSourceParameter)
      }
    ).attr("data-data-source-parameter-code", dataSourceParameter.code)
      .change(platformus.dataSourceParameterEditors.base.changed)
      .appendTo(field);

    return field;
  }
})(window.platformus = window.platformus || {});