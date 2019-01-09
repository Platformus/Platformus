// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.forms = platformus.forms || {};
  platformus.forms.objectSelectorForm = {};
  platformus.forms.objectSelectorForm.show = function (relationClassId, objectIds, minRelatedObjectsNumber, maxRelatedObjectsNumber, callback) {
    return platformus.forms.baseItemSelectorForm.show(
      "/backend/domain/objectselectorform?classid=" + relationClassId + "&objectids=" + objectIds, minRelatedObjectsNumber, maxRelatedObjectsNumber, callback
    );
  };

  platformus.forms.objectSelectorForm.select = function () {
    return platformus.forms.baseItemSelectorForm.select();
  };

  platformus.forms.objectSelectorForm.hideAndRemove = function () {
    return platformus.forms.baseItemSelectorForm.hideAndRemove();
  };
})(window.platformus = window.platformus || {});