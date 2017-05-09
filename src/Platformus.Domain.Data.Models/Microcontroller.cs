// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;

namespace Platformus.Domain.Data.Models
{
  public class Microcontroller : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlTemplate { get; set; }
    public string ViewName { get; set; }
    public string CSharpClassName { get; set; }
    public int? Position { get; set; }

    public virtual ICollection<DataSource> DataSources { get; set; }
  }
}