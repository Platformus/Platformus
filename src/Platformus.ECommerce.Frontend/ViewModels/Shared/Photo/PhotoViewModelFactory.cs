// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core;
using Platformus.ECommerce.Data.Entities;
using Platformus.Core.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
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
        Filename = photo.Filename
      };
    }
  }
}