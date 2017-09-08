// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Installer
{
  public class UsageScenario
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public IEnumerable<Package> Packages { get; set; }
    public IEnumerable<string> Content { get; set; }
  }
}