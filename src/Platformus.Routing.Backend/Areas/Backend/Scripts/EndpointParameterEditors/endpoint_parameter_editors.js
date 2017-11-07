// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.endpointParameterEditors = platformus.endpointParameterEditors || [];
  platformus.endpointParameterEditors.sync = function (cSharpClassName) {
    var endpointParameterEditors = $("#endpointParameterEditors").html(platformus.string.empty);

    endpointParameterEditors.html(platformus.string.empty);

    var endpoint = getEndpoint(cSharpClassName);

    $("<div>").addClass("form__description").html(endpoint.description).appendTo(endpointParameterEditors);

    for (var i = 0; i < endpoint.endpointParameterGroups.length; i++) {
      var group = $("<h2>").addClass("form__title").html(endpoint.endpointParameterGroups[i].name).appendTo(endpointParameterEditors);

      for (var j = 0; j < endpoint.endpointParameterGroups[i].endpointParameters.length; j++) {
        var endpointParameterEditor = platformus.endpointParameterEditors[endpoint.endpointParameterGroups[i].endpointParameters[j].javaScriptEditorClassName];

        if (endpointParameterEditor != null) {
          var f = endpointParameterEditor["create"];

          if (f != null) {
            f.call(this, endpointParameterEditors, endpoint.endpointParameterGroups[i].endpointParameters[j]);
          }
        }
      }
    }

    platformus.ui.initializeJQueryValidation();
  };

  function getEndpoint(cSharpClassName) {
    for (var i = 0; i < endpoints.length; i++) {
      if (endpoints[i].cSharpClassName == cSharpClassName) {
        return endpoints[i];
      }
    }

    return null;
  }
})(window.platformus = window.platformus || {});