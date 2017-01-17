// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using NUglify;
using Platformus.Barebone;

namespace Platformus.Designers.Backend
{
  public static class BandleManager
  {
    public static void Bundle(IRequestHandler requestHandler, string filename)
    {
      try
      {
        dynamic bundle = JsonConvert.DeserializeObject(File.ReadAllText(PathManager.GetBundlePath(requestHandler, filename)));
        string outputFile = bundle.outputFile;
        IEnumerable<string> inputFiles = bundle.inputFiles.ToObject<IEnumerable<string>>();
        string input = BandleManager.ConcatFiles(requestHandler, inputFiles);
        UgliflyResult result = outputFile.EndsWith(".css") ? Uglify.Css(input) : outputFile.EndsWith(".js") ? Uglify.Js(input) : default(UgliflyResult);

        if (!result.HasErrors)
          File.WriteAllText(PathManager.GetContentRootPath(requestHandler) + "\\" + outputFile, result.Code);
      }

      catch { }
    }

    private static string ConcatFiles(IRequestHandler requestHandler, IEnumerable<string> files)
    {
      StringBuilder result = new StringBuilder();

      foreach (string file in files)
        result.AppendLine(File.ReadAllText(PathManager.GetContentRootPath(requestHandler) + "\\" + file));

      return result.ToString();
    }
  }
}