// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

(function (platformus) {
  platformus.ui = platformus.ui || {};
  platformus.ui.showWizardPage = function (nextWizardPage) {
    $(".wizard__page").hide();
    $("#" + nextWizardPage).show();
  };

  platformus.ui.finish = function () {
    var languagePacks = "";
    $("[data-language-pack]:checked").each(
      function () {
        languagePacks += (languagePacks == "" ? "" : ",") + $(this).data("languagePack");
      }
    );

    $.post(
      "/installation/complete",
      {
        usageScenarioCode: $("input[name='usageScenario']:checked").val(),
        storageTypeCode: $("input[name='storageType']:checked").val(),
        connectionString: $("#connectionString").val(),
        languagePacks: languagePacks
      },
      function () {
        location.href = "/installation/complete";
      }
    );
  };

  platformus.ui.testConnection = function () {
    $.post(
      "/installation/testconnection",
      {
        storageTypeCode: $("input[name='storageType']:checked").val(),
        connectionString: $("#connectionString").val()
      },
      function (result) {
        if (result.successfulConnection) {
          alert("Successful connection!");
        }

        else {
          alert("An error occurred while trying to connect!");
        }
      }
    );
  };
})(window.platformus = window.platformus || {});