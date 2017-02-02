// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Infrastructure;

namespace Platformus.Barebone
{
  public class BackendMetadata : BackendMetadataBase
  {
    public override IEnumerable<BackendStyleSheet> BackendStyleSheets
    {
      get
      {
        return new BackendStyleSheet[]
        {
          new BackendStyleSheet("/wwwroot.areas.backend.css.platformus.barebone.min.css", 1000),
          new BackendStyleSheet("//fonts.googleapis.com/css?family=PT+Sans:400,400italic&subset=latin,cyrillic", 10000)
        };
      }
    }

    public override IEnumerable<BackendScript> BackendScripts
    {
      get
      {
        return new BackendScript[]
        {
          new BackendScript("//ajax.aspnetcdn.com/ajax/jquery/jquery-1.11.3.min.js", 100),
          new BackendScript("//ajax.aspnetcdn.com/ajax/jquery.validate/1.14.0/jquery.validate.min.js", 200),
          new BackendScript("//ajax.aspnetcdn.com/ajax/jquery.validation.unobtrusive/3.2.6/jquery.validate.unobtrusive.min.js", 300),
          new BackendScript("//cdn.tinymce.com/4/tinymce.min.js", 400),
          new BackendScript("//cdnjs.cloudflare.com/ajax/libs/moment.js/2.17.1/moment-with-locales.min.js", 500),
          new BackendScript("/wwwroot.areas.backend.js.platformus.barebone.min.js", 1000)
        };
      }
    }
  }
}