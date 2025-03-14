// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Admin.Services.Abstractions;

public interface IUrl
{
  Url Set(string name, string value);
  Url Remove(string name);
  string Build();
  void Go();
}