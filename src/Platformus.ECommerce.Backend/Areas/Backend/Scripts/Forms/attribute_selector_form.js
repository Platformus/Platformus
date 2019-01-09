// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.attributeSelectorForm = {};
  platformus.forms.attributeSelectorForm.show = function (attributeId, callback) {
    return platformus.forms.baseItemSelectorForm.show(
      "/backend/ecommerce/attributeselectorform?attributeid=" + attributeId, null, 1, callback
    );
  };

  platformus.forms.attributeSelectorForm.select = function () {
    return platformus.forms.baseItemSelectorForm.select();
  };

  platformus.forms.attributeSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseItemSelectorForm.hideAndRemove();
  };
})(window.platformus = window.platformus || {});