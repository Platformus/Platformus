// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/properties")]
[AuthenticatedOnly]
public class Property : IDto<Domain.Models.Property>
{
  public int Id { get; set; }
  public Object? Object { get; set; }
  public Member? Member { get; set; }
  public decimal? DecimalValue { get; set; }
  public string? StringValue { get; set; }
  public IEnumerable<LocalizedProperty>? LocalizedProperties { get; set; }

  public Property() { }

  public Property(Domain.Models.Property _property)
  {
    this.Id = _property.Id;
    this.Object = _property.Object == null ? null : new Object(_property.Object);
    this.Member = _property.Member == null ? null : new Member(_property.Member);
    this.DecimalValue = _property.DecimalValue;
    this.StringValue = _property.StringValue;
    this.LocalizedProperties = _property.LocalizedProperties?.Select(lp => new LocalizedProperty(lp)).ToList();
  }

  public Domain.Models.Property ToModel()
  {
    return new Domain.Models.Property()
    {
      Id = this.Id,
      Object = this.Object?.ToModel(),
      Member = this.Member?.ToModel(),
      DecimalValue = this.DecimalValue,
      StringValue = this.StringValue,
      LocalizedProperties = this.LocalizedProperties?.Select(lp => lp.ToModel())
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