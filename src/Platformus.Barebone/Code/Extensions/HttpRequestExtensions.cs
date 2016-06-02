// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Platformus
{
  public static class HttpRequestBaseExtensions
  {
    public static string CombineUrl(this HttpRequest request, params Url.Descriptor[] descriptors)
    {
      return Url.Combine(request, null, descriptors);
    }

    public static string CombineUrl(this HttpRequest request, string path, params Url.Descriptor[] descriptors)
    {
      return Url.Combine(request, path, descriptors);
    }
  }
}