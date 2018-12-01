// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using NUglify;
using Platformus.Barebone;

namespace Platformus.Designers
{
  public static class BandleManager
  {
    public static void RebuildAllBundles(IRequestHandler requestHandler)
    {
      foreach (FileInfo bundleFileInfo in FileSystemRepository.GetFiles(PathManager.GetBundlesPath(requestHandler), "*.json", null))
        BandleManager.RebuildBundle(requestHandler, bundleFileInfo.Name);
    }

    public static void RebuildBundle(IRequestHandler requestHandler, string bundleFilename)
    {
      try
      {
        dynamic bundle = JsonConvert.DeserializeObject(File.ReadAllText(PathManager.GetBundlePath(requestHandler, bundleFilename)));
        string outputFile = bundle.outputFile;
        IEnumerable<string> inputFiles = bundle.inputFiles.ToObject<IEnumerable<string>>();
        string input = BandleManager.ConcatFiles(requestHandler, inputFiles);
        UglifyResult result = outputFile.EndsWith(".css") ? Uglify.Css(input) : outputFile.EndsWith(".js") ? Uglify.Js(input) : default(UglifyResult);

        if (!result.HasErrors)
        {
          string outputFilepath = PathManager.Combine(PathManager.GetContentRootPath(requestHandler), outputFile);

          PathManager.EnsureFilepathExists(outputFilepath);
          File.WriteAllText(outputFilepath, result.Code);
        }
      }

      catch { }
    }

    private static string ConcatFiles(IRequestHandler requestHandler, IEnumerable<string> files)
    {
      StringBuilder result = new StringBuilder();

      foreach (string file in files)
        result.AppendLine(File.ReadAllText(PathManager.Combine(PathManager.GetContentRootPath(requestHandler), file)));

      return result.ToString();
    }
  }
}