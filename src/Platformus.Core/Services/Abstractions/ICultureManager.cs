// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Services.Abstractions
{
  public interface ICultureManager
  {
    Task<Culture> GetCultureAsync(string id);
    Task<Culture> GetNeutralCultureAsync();
    Task<Culture> GetFrontendDefaultCultureAsync();
    Task<Culture> GetBackendDefaultCultureAsync();
    Task<Culture> GetCurrentCultureAsync();
    Task<IEnumerable<Culture>> GetCulturesAsync();
    Task<IEnumerable<Culture>> GetNotNeutralCulturesAsync();
    void InvalidateCache();
  }
}