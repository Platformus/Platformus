// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Forms.Data.Entities
{
  public class SerializedForm : IEntity
  {
    public int CultureId { get; set; }
    public int FormId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string SerializedFields { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Form Form { get; set; }
  }
}