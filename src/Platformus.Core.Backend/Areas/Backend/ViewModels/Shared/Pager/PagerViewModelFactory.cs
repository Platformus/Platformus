// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class PagerViewModelFactory : ViewModelFactoryBase
  {
    public PagerViewModel Create(int skip, int take, int total)
    {
      return new PagerViewModel()
      {
        Skip = skip,
        Take = take,
        Total = total
      };
    }
  }
}