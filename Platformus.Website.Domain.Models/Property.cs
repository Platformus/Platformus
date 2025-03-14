// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class Property : IModel<Data.Entities.Property, PropertyFilter>
{
  public int Id { get; set; }
  public Object? Object { get; set; }
  public Member? Member { get; set; }
  public decimal? DecimalValue { get; set; }
  public string? StringValue { get; set; }
  public IEnumerable<LocalizedProperty>? LocalizedProperties { get; set; }

  public Property() { }

  public Property(Data.Entities.Property _property) : this(_property, mapObject: true, mapMember: true, mapLocalizedProperties: true) { }

  public Property(Data.Entities.Property _property, bool mapObject = true, bool mapMember = true, bool mapLocalizedProperties = true)
  {
    this.Id = _property.Id;
    this.Object = mapObject ? _property.Object == null ? new Object { Id = _property.ObjectId } : new Object(_property.Object, mapProperties: false) : null;
    this.Member = mapMember ? _property.Member == null ? new Member { Id = _property.MemberId } : new Member(_property.Member, mapProperties: false) : null;
    this.DecimalValue = _property.DecimalValue;
    this.StringValue = _property.StringValue;
    this.LocalizedProperties = mapLocalizedProperties ? _property.LocalizedProperties?.Select(lp => new LocalizedProperty(lp, mapProperty: false)).ToList() : null;
  }

  public Data.Entities.Property ToEntity()
  {
    return new Data.Entities.Property()
    {
      Id = this.Id,
      ObjectId = this.Object?.Id ?? 0,
      MemberId = this.Member?.Id ?? 0,
      DecimalValue = this.DecimalValue,
      StringValue = this.StringValue,
      LocalizedProperties = this.LocalizedProperties?.Select(lp => lp.ToEntity()).ToList()
    };
  }

  public object? GetValue(Member? member = null)
  {
    member ??= this.Member;

    if (member!.MemberType!.Id == Domain.MemberTypes.TextBox)
      return member.IsLocalizable ? this.Localized().StringValue : this.StringValue;

    return this.DecimalValue;
  }

  public LocalizedProperty Localized()
  {
    return this.Localized(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
  }

  public LocalizedProperty Localized(string cultureId)
  {
    return this.LocalizedProperties?.FirstOrDefault(lp => lp.Culture!.Id == cultureId) ?? new LocalizedProperty();
  }
}