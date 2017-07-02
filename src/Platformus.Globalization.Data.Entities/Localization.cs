// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Globalization.Data.Entities
{
  public class Localization : IEntity
  {
    public int Id { get; set; }
    public int DictionaryId { get; set; }
    public int CultureId { get; set; }
    public string Value { get; set; }

    public virtual Dictionary Dictionary { get; set; }
    public virtual Culture Culture { get; set; }
  }
}