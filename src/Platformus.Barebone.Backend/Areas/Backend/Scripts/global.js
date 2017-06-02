// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

$(document).ready(
  function () {
    platformus.initializers.sort(
      function (a, b) { return (a.priority > b.priority) ? 1 : ((a.priority < b.priority) ? -1 : 0); }
    );

    for (var i = 0; i < platformus.initializers.length; i++) {
      platformus.initializers[i].action();
    }
  }
);