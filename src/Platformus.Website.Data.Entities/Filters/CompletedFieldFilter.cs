// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Website.Filters;

public class CompletedFieldFilter : IFilter
{
  public int? Id { get; set; }
  public CompletedFormFilter CompletedForm { get; set; }
  public FieldFilter Field { get; set; }
  public StringFilter Value { get; set; }

  public CompletedFieldFilter() { }

  public CompletedFieldFilter(int? id = null, CompletedFormFilter completedForm = null, FieldFilter field = null, StringFilter value = null)
  {
    Id = id;
    CompletedForm = completedForm;
    Field = field;
    Value = value;
  }
}