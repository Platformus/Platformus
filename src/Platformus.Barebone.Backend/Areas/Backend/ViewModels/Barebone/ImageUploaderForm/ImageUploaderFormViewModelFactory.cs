// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Backend.ViewModels.Barebone
{
  public class ImageUploaderFormViewModelFactory : ViewModelFactoryBase
  {
    public ImageUploaderFormViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ImageUploaderFormViewModel Create()
    {
      return new ImageUploaderFormViewModel()
      {
      };
    }
  }
}