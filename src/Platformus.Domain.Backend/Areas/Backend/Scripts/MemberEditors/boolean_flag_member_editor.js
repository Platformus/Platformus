// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.booleanFlag = {};
  platformus.memberEditors.booleanFlag.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field form__field--separated field");

    platformus.controls.checkbox.create(
      {
        identity: platformus.memberEditors.base.getIdentity(member),
        text: member.name,
        value: member.property.integerValue,
        useIntegerNumber: true
      }
    ).appendTo(field);
    return field;
  }
})(window.platformus = window.platformus || {});