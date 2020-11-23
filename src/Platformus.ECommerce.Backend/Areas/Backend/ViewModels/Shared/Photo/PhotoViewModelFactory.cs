﻿// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core;
using Platformus.ECommerce.Data.Entities;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class PhotoViewModelFactory : ViewModelFactoryBase
  {
    public PhotoViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public PhotoViewModel Create(Photo photo)
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