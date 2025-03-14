// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities;

public class Variable : IEntity<int>
{
  public int Id { get; set; }
  public int ConfigurationId { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public string? Value { get; set; }
  public int? Position { get; set; }

  public virtual Configuration? Configuration { get; set; }
}