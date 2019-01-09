// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.parameterEditors = platformus.parameterEditors || [];
  platformus.parameterEditors.checkbox = {};
  platformus.parameterEditors.checkbox.create = function (container, parameter) {
    createField(parameter).appendTo(container);
  };

  function createField(parameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: platformus.parameterEditors.base.getIdentity(parameter),
        text: parameter.name,
        value: platformus.parameterEditors.base.getValue(parameter)
      }
    ).attr("data-parameter-code", parameter.code)
      .change(platformus.parameterEditors.base.changed)
      .appendTo(field);

    return field;
  }
})(window.platformus = window.platformus || {});