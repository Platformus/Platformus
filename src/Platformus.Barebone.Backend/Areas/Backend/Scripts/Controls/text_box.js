// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.controls = platformus.controls || [];
  platformus.controls.textBox = {};
  platformus.controls.textBox.create = function (descriptor) {
    var textBox = $("<input>")
      .addClass("field__text-box")
      .addClass("text-box")
      .attr("id", descriptor.identity)
      .attr("name", descriptor.identity)
      .attr("type", "text")
      .attr("value", descriptor.value);

    if (descriptor.isMultilingual) {
      textBox.addClass("field__text-box--multilingual");
    }

    if (descriptor.validation != null && (descriptor.validation.isRequired || descriptor.validation.maxLength)) {
      textBox.attr("data-val", true)
    }

    if (descriptor.validation != null && descriptor.validation.isRequired) {
      textBox.addClass("text-box--required").attr("data-val-required", true);
    }

    if (descriptor.validation != null && descriptor.validation.maxLength != null) {
      textBox.attr("maxlength", descriptor.validation.maxLength).attr("data-val-maxlength-max", descriptor.validation.maxLength);
    }

    return textBox;
  };
})(window.platformus = window.platformus || {});