// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared;

public static class PositionsViewModelFactory
{
  public static PositionsViewModel Create(IEnumerable<Position> positions, string partialViewName, string additionalCssClass)
  {
    return new PositionsViewModel()
    {
      Positions = positions.Select(PositionViewModelFactory.Create),
      PartialViewName = partialViewName ?? "_Positions",
      AdditionalCssClass = additionalCssClass
    };
  }
}