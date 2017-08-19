// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.formHandlerParameterEditors = platformus.formHandlerParameterEditors || [];
  platformus.formHandlerParameterEditors.textBox = {};
  platformus.formHandlerParameterEditors.textBox.create = function (container, formHandlerParameter) {
    createField(formHandlerParameter).appendTo(container);
  };

  function createField(formHandlerParameter) {
    var identity = platformus.formHandlerParameterEditors.base.getIdentity(formHandlerParameter);
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ identity: identity, text: formHandlerParameter.name }).appendTo(field);
    platformus.controls.textBox.create(
      {
        identity: identity,
        value: platformus.formHandlerParameterEditors.base.getValue(formHandlerParameter),
        validation: { isRequired: formHandlerParameter.isRequired }
      }
    ).attr("data-form-handler-parameter-code", formHandlerParameter.code).appendTo(field)
      .change(platformus.formHandlerParameterEditors.base.changed)
      .appendTo(field);

    return field;
  }
})(window.platformus = window.platformus || {});