// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Menus.Data.Entities
{
  public class SerializedMenu : IEntity
  {
    public int CultureId { get; set; }
    public int MenuId { get; set; }
    public string Code { get; set; }
    public string SerializedMenuItems { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Menu Menu { get; set; }
  }
}