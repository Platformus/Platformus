// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Installer
{
  public class Installation
  {
    public IEnumerable<UsageScenario> UsageScenarios { get; set; }
    public IEnumerable<StorageType> StorageTypes { get; set; }
    public IEnumerable<LanguagePack> LanguagePacks { get; set; }
  }
}