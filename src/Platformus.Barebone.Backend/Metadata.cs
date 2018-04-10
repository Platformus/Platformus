// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Barebone.Backend.Metadata
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<StyleSheet> GetStyleSheets(IRequestHandler requestHandler)
    {
      return new StyleSheet[]
      {
        new StyleSheet("/wwwroot.areas.backend.css.platformus.barebone.min.css", 1000),
        new StyleSheet("//fonts.googleapis.com/css?family=PT+Sans:400,400italic&subset=latin,cyrillic", 10000)
      };
    }

    public override IEnumerable<Script> GetScripts(IRequestHandler requestHandler)
    {
      return new Script[]
      {
        new Script("//ajax.aspnetcdn.com/ajax/jquery/jquery-1.11.3.min.js", 100),
        new Script("//ajax.aspnetcdn.com/ajax/jquery.validate/1.14.0/jquery.validate.min.js", 200),
        new Script("//ajax.aspnetcdn.com/ajax/jquery.validation.unobtrusive/3.2.6/jquery.validate.unobtrusive.min.js", 300),
        new Script("//cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.4.1/jquery.cookie.min.js", 400),
        new Script("//cdn.tinymce.com/4/tinymce.min.js", 500),
        new Script("//cdnjs.cloudflare.com/ajax/libs/moment.js/2.17.1/moment-with-locales.min.js", 600),
        new Script("/wwwroot.areas.backend.js.platformus.barebone.min.js", 1000)
      };
    }
  }
}