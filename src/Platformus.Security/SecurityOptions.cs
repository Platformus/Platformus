// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Platformus.Security
{
  public class SecurityOptions
  {
    public bool EnableAuthentication { get; set; }
    public PathString AccessDeniedPath { get; set; }
    public PathString LoginPath { get; set; }
    public PathString LogoutPath { get; set; }
    public string ReturnUrlParameter { get; set; }
  }
}