// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Domain.Data.Models
{
  public class DataSource : IEntity
  {
    public int Id { get; set; }
    public int ClassId { get; set; }
    public string Code { get; set; }
    public string CSharpClassName { get; set; }
    public string Parameters { get; set; }

    public virtual Class Class { get; set; }
  }
}