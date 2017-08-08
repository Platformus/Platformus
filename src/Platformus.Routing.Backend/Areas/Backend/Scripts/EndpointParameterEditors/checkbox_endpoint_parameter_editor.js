// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.checkbox = {};
  platformus.endpointParameterEditors.checkbox.create = function (container, endpointParameter) {
    createField(endpointParameter).appendTo(container);
  };

  function createField(endpointParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: platformus.endpointParameterEditors.base.getIdentity(endpointParameter),
        text: endpointParameter.name,
        value: platformus.endpointParameterEditors.base.getValue(endpointParameter)
      }
    ).attr("data-endpoint-parameter-code", endpointParameter.code)
      .change(platformus.endpointParameterEditors.base.changed)
      .appendTo(field);
 
    return field;
  }
})(window.platformus = window.platformus || {});