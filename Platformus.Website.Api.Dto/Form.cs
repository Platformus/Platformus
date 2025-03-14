// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/forms")]
[AuthenticatedOnly]
public class Form : IDto<Domain.Models.Form>
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public IEnumerable<Field>? Fields { get; set; }

  public Form() { }

  public Form(Domain.Models.Form _form)
  {
    this.Id = _form.Id;
    this.Name = _form.Name;
    this.Fields = _form.Fields?.Select(f => new Field(f)).ToList();
  }

  public Domain.Models.Form ToModel()
  {
    return new Domain.Models.Form()
    {
      Id = Id,
      Name = Name
    };
  }
}
