// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities
{
  /// <summary>
  /// Represents an endpoint.
  /// The endpoints are used to specify which request processor should process requests with a given URL template.
  /// </summary>
  public class Endpoint : IEntity<int>
  {
    /// <summary>
    /// Gets or sets the unique identifier of the endpoint.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the endpoint name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URL template which is used by the endpoint resolver to select the endpoint
    /// to process each particular request.
    /// </summary>
    public string UrlTemplate { get; set; }

    /// <summary>
    /// Gets or sets the endpoint position. Position is used to sort the endpoints (smallest to largest).
    /// The endpoint resolvers check the endpoints one by one using their positions too.
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the anonymous requests should be processed by the endpoint or not.
    /// </summary>
    public bool DisallowAnonymous { get; set; }

    /// <summary>
    /// Gets or sets the URL anonymous requests will be redirected to (in case when <see cref="DisallowAnonymous"/> flag is set).
    /// </summary>
    public string SignInUrl { get; set; }

    /// <summary>
    /// Gets or sets the name (including namespace) of the request processor C# class which will be instantiated each time
    /// request comes.
    /// </summary>
    public string RequestProcessorCSharpClassName { get; set; }

    /// <summary>
    /// Gets or sets the parameters (key=value pairs separated by commas) for the request processor C# class instances.
    /// </summary>
    public string RequestProcessorParameters { get; set; }

    /// <summary>
    /// Gets or sets the name (including namespace) of the optional response cache C# class.
    /// </summary>
    public string ResponseCacheCSharpClassName { get; set; }

    /// <summary>
    /// Gets or sets the parameters (key=value pairs separated by commas) for the response cache C# class instances.
    /// </summary>
    public string ResponseCacheParameters { get; set; }

    public virtual ICollection<EndpointPermission> EndpointPermissions { get; set; }
    public virtual ICollection<DataSource> DataSources { get; set; }
  }
}