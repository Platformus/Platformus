// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc.Razor.Precompilation;
using Microsoft.Dnx.Compilation.CSharp;

namespace Platformus.Static.Backend
{
  public class RazorPreCompilation : RazorPreCompileModule
  {
    protected override bool EnablePreCompilation(BeforeCompileContext context) => true;
  }
}