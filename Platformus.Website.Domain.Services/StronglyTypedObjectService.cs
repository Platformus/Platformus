// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using Magicalizer.Domain;
using Magicalizer.Domain.Services.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.EntityFrameworkCore;
using Platformus.Website.Domain.Models;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Services;

public class StronglyTypedObjectService<T> where T : class, new()
{
  private readonly IService<int, Class, ClassFilter> classService;
  private readonly ObjectService objectService;

  public StronglyTypedObjectService(DbContext dbContext, IService<int, Class, ClassFilter> classService, IService<int, Models.Object, ObjectFilter> objectService)
  {
    this.classService = classService;
    this.objectService = (ObjectService)objectService;
  }

  public async Task<T?> GetByIdAsync(int id)
  {
    Class? @class = await this.GetClass();

    if (@class == null) throw new ArgumentException();

    Models.Object? @object = await this.objectService.GetByIdAsync(id, new InclusionBuilder<Models.Object>().Add(o => o.Properties).ThenAdd(p => p.LocalizedProperties).Build());

    if (@object == null) return null;

    return this.MapFromModel(@class, @object);
  }

  public async Task<IEnumerable<T>> GetAllAsync(ObjectFilter? filter = null, IEnumerable<ISorting<Models.Object>>? sortings = null, int? offset = null, int? limit = null)
  {
    Class? @class = await this.GetClass();

    if (@class == null) throw new ArgumentException();

    IEnumerable<Models.Object> objects = await this.objectService.GetAllAsync(filter, sortings, offset, limit, new InclusionBuilder<Models.Object>().Add(o => o.Properties).ThenAdd(p => p.LocalizedProperties).Build());
    List<T> results = [];

    foreach (Models.Object @object in objects)
      results.Add(this.MapFromModel(@class, @object));

    return results;
  }

  private async Task<Class?> GetClass()
  {
    return (await this.classService.GetAllAsync(
      new ClassFilter { CSharpName = new StringFilter { Equals = typeof(T).Name } },
      inclusions: new Inclusion<Class>(c => c.Members)
    )).SingleOrDefault();
  }

  private T MapFromModel(Class @class, Models.Object @object)
  {
    T result = new T();

    foreach (Member member in @class!.Members!)
    {
      Property? property = @object.Properties?.SingleOrDefault(p => p.Member!.Id == member.Id);

      if (property == null) continue;

      PropertyInfo? propertyInfo = typeof(T).GetProperty(member.CSharpName!, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

      if (propertyInfo == null) continue;

      propertyInfo.SetValue(result, property.GetValue(member));
    }

    return result;
  }
}