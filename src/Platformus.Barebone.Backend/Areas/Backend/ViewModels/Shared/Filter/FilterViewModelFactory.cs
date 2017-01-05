// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class FilterViewModelFactory : ViewModelFactoryBase
  {
    public FilterViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FilterViewModel Create(string filter)
    {
      return new FilterViewModel()
      {
        Filter = filter
      };
    }
  }
}