// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class Form : IModel<Data.Entities.Form, FormFilter>
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public IEnumerable<Field>? Fields { get; set; }

  public Form() { }

  public Form(Data.Entities.Form form) : this(form, mapFields: true) { }

  public Form(Data.Entities.Form form, bool mapFields = true)
  {
    this.Id = form.Id;
    this.Name = form.Name;
    this.Fields = mapFields ? form.Fields?.Select(f => new Field(f, mapForm: false)).ToList() : null;
  }

  public Data.Entities.Form ToEntity()
  {
    return new Data.Entities.Form
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}