// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class PositionsViewModelFactory : ViewModelFactoryBase
  {
    public PositionsViewModel Create(IEnumerable<Position> positions, string partialViewName, string additionalCssClass)
    {
      return new PositionsViewModel()
      {
        Positions = positions.Select(p => new PositionViewModelFactory().Create(p)),
        PartialViewName = partialViewName ?? "_Positions",
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}