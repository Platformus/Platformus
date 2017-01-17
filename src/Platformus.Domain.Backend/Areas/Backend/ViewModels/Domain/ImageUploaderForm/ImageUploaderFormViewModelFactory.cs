// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Domain
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