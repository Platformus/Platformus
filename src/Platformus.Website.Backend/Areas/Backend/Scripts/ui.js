// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.formHandlerCSharpClassNameChanged = function () {
    platformus.parameterEditor.load("formHandler", "/backend/core/parametereditor?csharpclassname=" + $("#formHandlerCSharpClassName").val());
  };

  platformus.ui.propertyDataTypeIdChanged = function () {
    var propertyDataTypeId = $("#propertyDataTypeId").val();

    if (propertyDataTypeId) {
      $("#propertyDataTypeOptions").show();
    }

    else {
      $("#propertyDataTypeOptions").hide();
    }

    platformus.parameterEditor.load("propertyDataType", "/backend/website/parametereditor?datatypeid=" + propertyDataTypeId);
  };

  platformus.ui.relationClassIdChanged = function () {
    if ($("#relationClassId").val()) {
      $("#relationClassOptions").show();
    }

    else {
      $("#relationClassOptions").hide();
    }
  };

  platformus.ui.isRelationSingleParentChanged = function () {
    if ($("#isRelationSingleParent").val()) {
      $("#isRelationSingleParentOptions").hide();
    }

    else {
      $("#isRelationSingleParentOptions").show();
    }
  };

  platformus.ui.disallowAnonymousChanged = function () {
    if ($("#disallowAnonymous").val()) {
      $("#disallowAnonymousOptions").show();
    }

    else {
      $("#disallowAnonymousOptions").hide();
    }
  };

  platformus.ui.requestProcessorCSharpClassNameChanged = function () {
    platformus.parameterEditor.load("requestProcessor", "/backend/core/parametereditor?csharpclassname=" + $("#requestProcessorCSharpClassName").val());
  };

  platformus.ui.responseCacheCSharpClassNameChanged = function () {
    if (!$("#responseCacheCSharpClassName").val()) {
      $("#responseCacheParameterEditor").html("");
      return;
    }

    platformus.parameterEditor.load("responseCache", "/backend/core/parametereditor?csharpclassname=" + $("#responseCacheCSharpClassName").val());
  };

  platformus.ui.dataProviderCSharpClassNameChanged = function () {
    platformus.parameterEditor.load("dataProvider", "/backend/core/parametereditor?csharpclassname=" + $("#dataProviderCSharpClassName").val());
  };
})(window.platformus = window.platformus || {});

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.tinyMceFileBrowserCallback = function (fieldId, url, type, wnd) {
    platformus.forms.fileSelectorForm.show(
      function (filename) {
        wnd.document.getElementById(fieldId).value = "/files/" + filename;
      }
    );
  };
})(window.platformus = window.platformus || {});