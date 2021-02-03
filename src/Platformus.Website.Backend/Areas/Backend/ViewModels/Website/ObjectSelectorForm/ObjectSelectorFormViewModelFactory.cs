// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public class ObjectSelectorFormViewModelFactory : ViewModelFactoryBase
  {
    public async Task<ObjectSelectorFormViewModel> CreateAsync(HttpContext httpContext, ObjectFilter filter, IEnumerable<Object> objects, string objectIds)
    {
      Class @class = filter?.Class?.Id == null ? null : await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetByIdAsync(
        (int)filter.Class.Id,
        new Inclusion<Class>("Members.PropertyDataType"),
        new Inclusion<Class>("Members.RelationClass"),
        new Inclusion<Class>("Parent.Members.PropertyDataType"),
        new Inclusion<Class>("Parent.Members.RelationClass")
      );

      return new ObjectSelectorFormViewModel()
      {
        Class = new ClassViewModelFactory().Create(@class),
        GridColumns = this.GetGridColumns(@class),
        Objects = objects.Select(o => new ObjectViewModelFactory().Create(o, @class.GetVisibleInListMembers())),
        ObjectIds = string.IsNullOrEmpty(objectIds) ? new int[] { } : objectIds.Split(',').Select(objectId => int.Parse(objectId))
      };
    }

    private IEnumerable<GridColumnViewModel> GetGridColumns(Class @class)
    {
      List<GridColumnViewModel> gridColumns = @class.GetVisibleInListMembers().Select(
        m => new GridColumnViewModelFactory().Create(m.Name)
      ).ToList();

      return gridColumns;
    }
  }
}