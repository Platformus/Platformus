// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Routing.Data.Entities
{
  /// <summary>
  /// Represents a data source. The data sources are used by the endpoints to load (or generate) data
  /// that is required to process the requests (to generate response).
  /// </summary>
  public class DataSource : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the data source.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the endpoint identifier this data source belongs to.
    /// </summary>
    public int EndpointId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the data source. It is set by the user and might be used for the data source retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the name (including namespace) of the C# class which will be instantiated each time
    /// when the data source is requested to provide data.
    /// </summary>
    public string CSharpClassName { get; set; }

    /// <summary>
    /// Gets or sets the parameters (key=value pairs separated by commas) for the C# class instances.
    /// </summary>
    public string Parameters { get; set; }

    public virtual Endpoint Endpoint { get; set; }
  }
}