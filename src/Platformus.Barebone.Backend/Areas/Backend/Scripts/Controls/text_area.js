// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.textArea = {};
  platformus.controls.textArea.create = function (descriptor) {
    var textArea = $("<textarea>")
      .addClass("field__text-area")
      .addClass("text-area")
      .attr("id", descriptor.identity)
      .attr("name", descriptor.identity)
      .html(descriptor.value);

    if (descriptor.isMultilingual) {
      textArea.addClass("field__text-area--multilingual");
    }

    if (descriptor.validation != null && (descriptor.validation.isRequired || descriptor.validation.maxLength)) {
      textArea.attr("data-val", true)
    }

    if (descriptor.validation != null && descriptor.validation.isRequired) {
      textArea.addClass("text-area--required").attr("data-val-required", true);
    }

    if (descriptor.validation != null && descriptor.validation.maxLength != null) {
      textArea.attr("maxlength", descriptor.validation.maxLength).attr("data-val-maxlength-max", descriptor.validation.maxLength);
    }

    return textArea;
  };
})(window.platformus = window.platformus || {});