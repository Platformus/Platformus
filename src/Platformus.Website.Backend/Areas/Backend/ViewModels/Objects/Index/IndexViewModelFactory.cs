// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Extensions;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Objects
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public async Task<IndexViewModel> CreateAsync(HttpContext httpContext, ObjectFilter filter, IEnumerable<Object> objects, string orderBy, int skip, int take, int total)
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
        Class = @class == null ? null : new ClassViewModelFactory().Create(@class),
        ClassesByAbstractClasses = await this.GetClassesByAbstractClassesAsync(httpContext),
        Grid = @class == null ? null : new GridViewModelFactory().Create(
          httpContext, orderBy, skip, take, total,
          this.GetGridColumns(@class),
          objects.Select(o => new ObjectViewModelFactory().Create(o, @class.GetVisibleInListMembers())),
          "_Object"
        )
      };
    }

    private async Task<IDictionary<ClassViewModel, IEnumerable<ClassViewModel>>> GetClassesByAbstractClassesAsync(HttpContext httpContext)
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
        IStringLocalizer<IndexViewModelFactory> localizer = httpContext.GetStringLocalizer<IndexViewModelFactory>();

        abstractClasses.Add(new Class() { PluralizedName = localizer["Others"], Classes = classes });
      }

      return abstractClasses.ToDictionary(
        ac => new ClassViewModelFactory().Create(ac),
        ac => ac.Classes.Select(c => new ClassViewModelFactory().Create(c))
      );
    }

    private IEnumerable<GridColumnViewModel> GetGridColumns(Class @class)
    {
      List<GridColumnViewModel> gridColumns = @class.GetVisibleInListMembers().Select(
        m => new GridColumnViewModelFactory().Create(m.Name)
      ).ToList();

      gridColumns.Add(new GridColumnViewModelFactory().CreateEmpty());
      return gridColumns;
    }
  }
}