// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Admin;

public class TinyMCEOptions
{
  public string ScriptSrc { get; set; } = "/tinymce/tinymce.min.js";

  public Dictionary<string, object> Configuration { get; set; } = new Dictionary<string, object>
  {
    { "plugins", "paste contextmenu lists advlist link autolink anchor image media table charmap fullscreen code" },
    { "menubar", false },
    { "toolbar", "styles bold italic underline | bullist numlist | link unlink image media table charmap | removeformat | fullscreen | code" },
    { "statusbar", false }
  };

  public string LicenseKey { get; set; } = "gpl";
}