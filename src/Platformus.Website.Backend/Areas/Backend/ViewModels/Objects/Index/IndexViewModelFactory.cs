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
using Platformus.Core.Backend;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Data.Entities;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Objects;

public static class IndexViewModelFactory
{
  public static async Task<IndexViewModel> CreateAsync(HttpContext httpContext, ObjectFilter filter, string sorting, int offset, int limit, int total, IEnumerable<Object> objects)
  {
    Class parentClass = (filter?.Primary?.Id == null ? null : await httpContext.GetStorage().GetRepository<int, Object, ObjectFilter>().GetByIdAsync(
      (int)filter?.Primary?.Id,
      new Inclusion<Object>(o => o.Class)
    ))?.Class;

    Class @class = filter?.Class?.Id == null ? null : await httpContext.GetStorage().GetRepository<int, Class, ClassFilter>().GetByIdAsync(
      (int)filter.Class.Id,
      new Inclusion<Class>("Members.PropertyDataType"),
      new Inclusion<Class>("Members.RelationClass"),
      new Inclusion<Class>("Parent.Members.PropertyDataType"),
      new Inclusion<Class>("Parent.Members.RelationClass")
    );

    if (objects != null)
      objects.ToList().ForEach(o => o.Class = @class);

    return new IndexViewModel()
    {
      ParentClass = parentClass == null ? null : ClassViewModelFactory.Create(parentClass),
      Class = @class == null ? null : ClassViewModelFactory.Create(@class),
      ClassesByAbstractClasses = await GetClassesByAbstractClassesAsync(httpContext),
      Sorting = sorting,
      Offset = offset,
      Limit = limit,
      Total = total,
      TableColumns = @class == null ? null : GetTableColumns(@class),
      Objects = objects == null ? null : objects.Select(o => ObjectViewModelFactory.Create(httpContext, o)).ToList()
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

  private static IEnumerable<TableTagHelper.Column> GetTableColumns(Class @class)
  {
    List<TableTagHelper.Column> tableColumns = @class.GetVisibleInListMembers().Select(
      m => new TableTagHelper.Column(m.Name, GetMemberSortingName(m))
    ).ToList();

    return tableColumns;
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