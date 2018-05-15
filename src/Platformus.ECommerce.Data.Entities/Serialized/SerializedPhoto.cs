// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.ECommerce.Data.Entities
{
  public class SerializedPhoto
  {
    public string Filename { get; set; }
    public bool IsCover { get; set; }
    public int? Position { get; set; }
  }
}