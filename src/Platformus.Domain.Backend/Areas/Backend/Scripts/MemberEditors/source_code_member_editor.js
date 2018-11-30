// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.memberEditors = platformus.memberEditors || [];
  platformus.memberEditors.sourceCode = {};
  platformus.memberEditors.sourceCode.create = function (container, member) {
    createField(member).appendTo(container);
  };

  function createField(member) {
    var field = $("<div>").addClass("form__field").addClass("field");

    platformus.controls.label.create({ text: member.name }).appendTo(field);

    if (member.isPropertyLocalizable) {
    }

    else {
      for (var i = 0; i < member.property.stringValue.localizations.length; i++) {
        var localization = member.property.stringValue.localizations[i];

        if (localization.culture.code == "__") {
          createCodeEditor(member, localization).appendTo(field);
        }
      }
    }

    return field;
  }

  function createCodeEditor(member, localization) {
    var mode = platformus.memberEditors.base.getDataTypeParameterValue(member, "Mode", "ace/mode/javascript");
    var identity = platformus.memberEditors.base.getIdentity(member, localization);
    var codeEditor = $("<div>").addClass("code-editor");

    $("<div>").addClass("code-editor__widget").attr("id", identity).attr("data-mode", mode).html($("<div>").text(localization.value).html()).appendTo(codeEditor);
    $("<input>").attr("name", identity).attr("type", "hidden").attr("data-culture", localization.culture.code).val(localization.value).appendTo(codeEditor);
    return codeEditor;
  }
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.initializers = platformus.initializers || [];
  platformus.initializers.push(
    {
      action: function () {
        $(".code-editor").each(
          function () {
            var codeEditor = ace.edit($(this).find(".code-editor__widget").attr("id"));
            var input = $(this).find("input");

            codeEditor.setTheme("ace/theme/chrome");
            codeEditor.getSession().setMode($(this).find(".code-editor__widget").attr("data-mode"));
            codeEditor.getSession().on("change", function () { input.val(codeEditor.getSession().getValue()); });
          }
        );
      },
      priority: 10
    }
  );
})(window.platformus = window.platformus || {});