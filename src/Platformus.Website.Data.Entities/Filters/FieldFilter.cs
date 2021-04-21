// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Website.Filters
{
  public class FieldFilter : IFilter
  {
    public int? Id { get; set; }
    public FormFilter Form { get; set; }
    public FieldTypeFilter FieldType { get; set; }
    public string Code { get; set; }

    [FilterShortcut("Name.Localizations[]")]
    public LocalizationFilter Name { get; set; }

    public FieldFilter() { }

    public FieldFilter(int? id = null, FormFilter form = null, FieldTypeFilter fieldType = null, string code = null, LocalizationFilter name = null)
    {
      Id = id;
      Form = form;
      FieldType = fieldType;
      Code = code;
      Name = name;
    }
  }
}