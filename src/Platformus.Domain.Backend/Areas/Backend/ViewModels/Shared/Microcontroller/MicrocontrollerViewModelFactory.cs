// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class MicrocontrollerViewModelFactory : ViewModelFactoryBase
  {
    public MicrocontrollerViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MicrocontrollerViewModel Create(Microcontroller microcontroller)
    {
      return new MicrocontrollerViewModel()
      {
        Id = microcontroller.Id,
        Name = microcontroller.Name,
        Position = microcontroller.Position
      };
    }
  }
}