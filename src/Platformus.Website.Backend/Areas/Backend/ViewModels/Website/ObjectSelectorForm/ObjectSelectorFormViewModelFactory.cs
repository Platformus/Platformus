// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public static class ObjectSelectorFormViewModelFactory
  {
    public static async Task<ObjectSelectorFormViewModel> CreateAsync(HttpContext httpContext, ObjectFilter filter, IEnumerable<Object> objects, string objectIds)
    {
      Class @class = filter?.Class?.Id == null ? null : await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetByIdAsync(
        (int)filter.Class.Id,
        new Inclusion<Class>("Members.PropertyDataType"),
        new Inclusion<Class>("Members.RelationClass"),
        new Inclusion<Class>("Parent.Members.PropertyDataType"),
        new Inclusion<Class>("Parent.Members.RelationClass")
      );

      objects.ToList().ForEach(o => o.Class = @class);

      return new ObjectSelectorFormViewModel()
      {
        Class = ClassViewModelFactory.Create(@class),
        TableColumns = GetTableColumns(@class),
        Objects = objects.Select(o => ObjectViewModelFactory.Create(httpContext, o)).ToList(),
        ObjectIds = string.IsNullOrEmpty(objectIds) ? new int[] { } : objectIds.Split(',').Select(objectId => int.Parse(objectId)).ToList()
      };
    }

    private static IEnumerable<TableTagHelper.Column> GetTableColumns(Class @class)
    {
      List<TableTagHelper.Column> tableColumns = @class.GetVisibleInListMembers().Select(
        m => new TableTagHelper.Column(m.Name)
      ).ToList();

      return tableColumns;
    }
  }
}