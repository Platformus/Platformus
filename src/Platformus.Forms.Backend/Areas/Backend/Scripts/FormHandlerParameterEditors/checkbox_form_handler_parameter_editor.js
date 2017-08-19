// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.formHandlerParameterEditors = platformus.formHandlerParameterEditors || [];
  platformus.formHandlerParameterEditors.checkbox = {};
  platformus.formHandlerParameterEditors.checkbox.create = function (container, formHandlerParameter) {
    createField(formHandlerParameter).appendTo(container);
  };

  function createField(formHandlerParameter) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: platformus.formHandlerParameterEditors.base.getIdentity(formHandlerParameter),
        text: formHandlerParameter.name,
        value: platformus.formHandlerParameterEditors.base.getValue(formHandlerParameter)
      }
    ).attr("data-form-handler-parameter-code", formHandlerParameter.code)
      .change(platformus.formHandlerParameterEditors.base.changed)
      .appendTo(field);
 
    return field;
  }
})(window.platformus = window.platformus || {});