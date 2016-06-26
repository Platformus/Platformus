// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Content
{
  public class ImageUploaderFormViewModelBuilder : ViewModelBuilderBase
  {
    public ImageUploaderFormViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public ImageUploaderFormViewModel Build()
    {
      return new ImageUploaderFormViewModel()
      {
      };
    }
  }
}