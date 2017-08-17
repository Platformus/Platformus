// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Routing.Data.Entities
{
  /// <summary>
  /// Represents a data source. The data sources are used by the endpoints to load (or generate) data
  /// that is required to process the requests.
  /// </summary>
  public class DataSource : IEntity
  {
    public int Id { get; set; }
    public int EndpointId { get; set; }
    public string Code { get; set; }
    public string CSharpClassName { get; set; }
    public string Parameters { get; set; }

    public virtual Endpoint Endpoint { get; set; }
  }
}