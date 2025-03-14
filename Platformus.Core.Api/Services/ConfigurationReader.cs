// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Services.Abstractions;
using Magicalizer.Filters.Abstractions;
using Platformus.Core.Api.Services.Abstractions;
using Platformus.Core.Domain.Models;
using Platformus.Core.Filters;

namespace Platformus.Core.Api.Services;

public class ConfigurationReader : IConfigurationReader
{
  private readonly IService<int, Variable, VariableFilter> variableService;

  public ConfigurationReader(IService<int, Variable, VariableFilter> variableService)
  {
    this.variableService = variableService;
  }

  public async Task<string?> GetAsync(string configurationCode, string variableCode)
  {
    return (await this.variableService.GetAllAsync(
      new VariableFilter
      {
        Configuration = new ConfigurationFilter { Code = new StringFilter { Equals = configurationCode } },
        Code = new StringFilter { Equals = variableCode }
      }
    )).FirstOrDefault()?.Value;
  }
}