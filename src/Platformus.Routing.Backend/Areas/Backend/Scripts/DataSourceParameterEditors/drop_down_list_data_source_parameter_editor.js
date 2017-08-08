// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.dataSourceParameterEditors = platformus.dataSourceParameterEditors || [];
  platformus.dataSourceParameterEditors.dropDownList = {};
  platformus.dataSourceParameterEditors.dropDownList.create = function (container, dataSourceParameter) {
    createField(dataSourceParameter).appendTo(container);
  };

  function createField(dataSourceParameter) {
    var identity = platformus.dataSourceParameterEditors.base.getIdentity(dataSourceParameter);
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ identity: identity, text: dataSourceParameter.name }).appendTo(field);
    platformus.controls.dropDownList.create(
      {
        identity: identity,
        options: dataSourceParameter.options,
        value: platformus.dataSourceParameterEditors.base.getValue(dataSourceParameter)
      }
    ).attr("data-data-source-parameter-code", dataSourceParameter.code)
      .change(platformus.dataSourceParameterEditors.base.changed)
      .appendTo(field);

    return field;
  }
})(window.platformus = window.platformus || {});