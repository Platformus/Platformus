// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class Culture : IModel<Data.Entities.Culture, CultureFilter>
{
  public string? Id { get; set; }
  public string? Name { get; set; }

  public Culture() { }

  public Culture(Data.Entities.Culture _culture)
  {
    this.Id = _culture.Id;
    this.Name = _culture.Name;
  }

  public Data.Entities.Culture ToEntity()
  {
    return new Data.Entities.Culture()
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}