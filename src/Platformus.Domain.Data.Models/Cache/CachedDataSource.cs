// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Domain.Data.Models
{
  public class CachedDataSource : IEntity
  {
    public int DataSourceId { get; set; }
    public string Code { get; set; }
    public string CSharpClassName { get; set; }
    public string Parameters { get; set; }
  }
}