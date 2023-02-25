// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities;

/// <summary>
/// Represents a data source. The data sources are used by the endpoints to load (or generate) data
/// that is required to process the requests (to generate response).
/// </summary>
public class DataSource : IEntity<int>
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
  /// Gets or sets the name (including namespace) of the data provider C# class which will be instantiated each time
  /// when the data source is requested to provide data.
  /// </summary>
  public string DataProviderCSharpClassName { get; set; }

  /// <summary>
  /// Gets or sets the parameters (key=value pairs separated by commas) for the data provider C# class instances.
  /// </summary>
  public string DataProviderParameters { get; set; }

  public virtual Endpoint Endpoint { get; set; }
}