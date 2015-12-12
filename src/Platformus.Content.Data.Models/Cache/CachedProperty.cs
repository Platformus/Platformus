// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;

namespace Platformus.Content.Data.Models
{
  public class CachedProperty : IEntity
  {
    public int PropertyId { get; set; }
    public string MemberCode { get; set; }
    public string Html { get; set; }
  }
}