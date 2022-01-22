// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults
{
  public class DefaultCleaningManager : ICleaningManager
  {
    public async Task CleanUpAsync(IServiceProvider serviceProvider)
    {
      IStorage storage = serviceProvider.CreateScope().ServiceProvider.GetService<IStorage>();
      IRepository<Guid, ModelState, ModelStateFilter> repository = storage.GetRepository<Guid, ModelState, ModelStateFilter>();
      IEnumerable<ModelState> oldModelStates = await repository.GetAllAsync(new ModelStateFilter(created: new DateTimeFilter(to: DateTime.Now.AddDays(-1))));

      for (int i = 0; i != oldModelStates.Count(); i++)
        repository.Delete(oldModelStates.ElementAt(i).Id);

      await storage.SaveAsync();
    }
  }
}