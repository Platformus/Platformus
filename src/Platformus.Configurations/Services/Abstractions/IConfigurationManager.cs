// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Configurations.Services.Abstractions
{
  public interface IConfigurationManager
  {
    string this[string configurationCode, string variableCode] { get; }

    void InvalidateCache();
  }
}