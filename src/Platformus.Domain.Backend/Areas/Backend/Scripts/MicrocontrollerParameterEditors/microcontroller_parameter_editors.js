// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.microcontrollerParameterEditors = platformus.microcontrollerParameterEditors || [];
  platformus.microcontrollerParameterEditors.sync = function (cSharpClassName) {
    var microcontrollerParameterEditors = $("#microcontrollerParameterEditors").html(platformus.string.empty);

    microcontrollerParameterEditors.html(platformus.string.empty);

    var microcontroller = getMicrocontroller(cSharpClassName);

    for (var i = 0; i < microcontroller.microcontrollerParameterGroups.length; i++) {
      var group = $("<h2>").addClass("form_title").html(microcontroller.microcontrollerParameterGroups[i].name).appendTo(microcontrollerParameterEditors);

      for (var j = 0; j < microcontroller.microcontrollerParameterGroups[i].microcontrollerParameters.length; j++) {
        var f = platformus.microcontrollerParameterEditors[microcontroller.microcontrollerParameterGroups[i].microcontrollerParameters[j].javaScriptEditorClassName]["create"];

        f.call(this, microcontrollerParameterEditors, microcontroller.microcontrollerParameterGroups[i].microcontrollerParameters[j]);
      }
    }
  };

  function getMicrocontroller(cSharpClassName) {
    for (var i = 0; i < microcontrollers.length; i++) {
      if (microcontrollers[i].cSharpClassName == cSharpClassName) {
        return microcontrollers[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});