// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Platformus.Core;
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
      List<Class> abstractClasses = (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync(new ClassFilter(isAbstract: true), inclusions: new Inclusion<Class>("Classes.RelationMembers"))).ToList();
      List<Class> nonAbstractClasses = (await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetAllAsync(new ClassFilter(isAbstract: false), inclusions: new Inclusion<Class>("RelationMembers"))).Where(c => c.ClassId == null).ToList();

      if (nonAbstractClasses.Count() != 0)
      {
        IStringLocalizer<IndexViewModel> localizer = httpContext.GetStringLocalizer<IndexViewModel>();

        abstractClasses.Add(new Class() { PluralizedName = localizer["Others"], Classes = nonAbstractClasses });
      }

      return abstractClasses.ToDictionary(
        ClassViewModelFactory.Create,
        ac => ac.Classes.Where(c => !c.RelationMembers.Any(rm => rm.IsRelationSingleParent == true)).Select(ClassViewModelFactory.Create)
      );
    }

    private static IEnumerable<GridColumnViewModel> GetGridColumns(Class @class)
    {
      List<GridColumnViewModel> gridColumns = @class.GetVisibleInListMembers().Select(
        m => GridColumnViewModelFactory.Create(m.Name, GetMemberSortingName(m))
      ).ToList();

      gridColumns.Add(GridColumnViewModelFactory.CreateEmpty());
      return gridColumns;
    }

    private static string GetMemberSortingName(Member member)
    {
      if (member.PropertyDataType == null)
        return null;

      if (member.PropertyDataType.StorageDataType == StorageDataTypes.Integer)
        return $"Properties.First(p=>p.Member.Code=\"{member.Code}\").IntegerValue";

      if (member.PropertyDataType.StorageDataType == StorageDataTypes.Decimal)
        return $"Properties.First(p=>p.Member.Code=\"{member.Code}\").DecimalValue";

      if (member.PropertyDataType.StorageDataType == StorageDataTypes.String)
      {
        string cultureCode = string.Empty;

        if (member.IsPropertyLocalizable == true)
          cultureCode = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        else cultureCode = NeutralCulture.Id;

        return $"Properties.First(p=>p.Member.Code=\"{member.Code}\").StringValue.Localizations.First(l=>l.Culture.Id=\"{cultureCode}\").Value";
      }

      if (member.PropertyDataType.StorageDataType == StorageDataTypes.DateTime)
        return $"Properties.First(p=>p.Member.Code=\"{member.Code}\").DateTimeValue";

      return null;
    }
  }
}