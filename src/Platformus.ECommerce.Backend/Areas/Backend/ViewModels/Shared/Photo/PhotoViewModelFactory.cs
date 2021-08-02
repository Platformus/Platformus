// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public static class PhotoViewModelFactory
  {
    public static PhotoViewModel Create(Photo photo)
    {
      return new PhotoViewModel()
      {
        Id = photo.Id,
        Filename = photo.Filename,
        IsCover = photo.IsCover,
        Position = photo.Position
      };
    }
  }
}