// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Objects
{
  public static class IndexViewModelFactory
  {
    public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, ObjectFilter filter, IEnumerable<Object> objects, string orderBy, int skip, int take, int total)
    {
      Class @class = filter?.Class?.Id == null ? null : await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetByIdAsync(
        (int)filter.Class.Id,
        new Inclusion<Class>("Members.PropertyDataType"),
        new Inclusion<Class>("Members.RelationClass"),
        new Inclusion<Class>("Parent.Members.PropertyDataType"),
        new Inclusion<Class>("Parent.Members.RelationClass")
      );

      return new IndexViewModel()
      {
        Class = @class == null ? null : ClassViewModelFactory.Create(@class),
        ClassesByAbstractClasses = await GetClassesByAbstractClassesAsync(httpContext),
        Grid = @class == null ? null : GridViewModelFactory.Create(
          httpContext, orderBy, skip, take, total,
          GetGridColumns(@class),
          objects.Select(o => ObjectViewModelFactory.Create(o, @class.GetVisibleInListMembers())),
          "_Object"
        )
      };
    }

    private static async Task<IDictionary<ClassViewModel, IEnumerable<ClassViewModel>>> GetClassesByAbstractClassesAsync(HttpContext httpContext)
    {
      List<Class> abstractClasses = (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync(new ClassFilter(isAbstract: true), inclusions: new Inclusion<Class>(c => c.Classes))).ToList();
      
      // TODO: must be refactored
      List<Class> tempClasses = (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync(new ClassFilter(isAbstract: false)))
        .Where(c => c.ClassId == null).ToList();
      List<Class> classes = new List<Class>();

      foreach (Class @class in tempClasses)
        if (await httpContext.GetStorage().GetRepository<int, Member, MemberFilter>().CountAsync(new MemberFilter(relationClass: new ClassFilter(id: @class.Id), isRelationSingleParent: true)) == 0)
          classes.Add(@class);

      if (classes.Count() != 0)
      {
        IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

        abstractClasses.Add(new Class() { PluralizedName = localizer["Others"], Classes = classes });
      }

      return abstractClasses.ToDictionary(
        ac => ClassViewModelFactory.Create(ac),
        ac => ac.Classes.Select(c => ClassViewModelFactory.Create(c))
      );
    }

    private static IEnumerable<GridColumnViewModel> GetGridColumns(Class @class)
    {
      List<GridColumnViewModel> gridColumns = @class.GetVisibleInListMembers().Select(
        m => GridColumnViewModelFactory.Create(m.Name)
      ).ToList();

      gridColumns.Add(GridColumnViewModelFactory.CreateEmpty());
      return gridColumns;
    }
  }
}