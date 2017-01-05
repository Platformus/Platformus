// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class PagerViewModelFactory : ViewModelFactoryBase
  {
    public PagerViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

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