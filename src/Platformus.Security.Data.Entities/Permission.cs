// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Security.Data.Entities
{
  /// <summary>
  /// Represents a permission. The permissions are used to restrict access to the different web application resources.
  /// They can be grouped using the groups, and then assigned to a user.
  /// </summary>
  public class Permission : IEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int? Position { get; set; }
  }
}