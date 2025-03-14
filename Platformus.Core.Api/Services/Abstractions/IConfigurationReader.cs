// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Api.Services.Abstractions;

public interface IConfigurationReader
{
  Task<string?> GetAsync(string configurationCode, string variableCode);
}