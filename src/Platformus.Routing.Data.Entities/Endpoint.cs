// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Routing.Data.Entities
{
  /// <summary>
  /// Represents an endpoint. The endpoints are used to combine URL template with corresponding C# handler class.
  /// </summary>
  public class Endpoint : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlTemplate { get; set; }
    public int? Position { get; set; }
    public bool DisallowAnonymous { get; set; }
    public string SignInUrl { get; set; }
    public string CSharpClassName { get; set; }
    public string Parameters { get; set; }

    public virtual ICollection<DataSource> DataSources { get; set; }
  }
}