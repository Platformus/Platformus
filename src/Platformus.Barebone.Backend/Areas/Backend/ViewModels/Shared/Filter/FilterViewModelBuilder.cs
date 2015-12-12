// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class FilterViewModelBuilder : ViewModelBuilderBase
  {
    public FilterViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public FilterViewModel Build(string filter)
    {
      return new FilterViewModel()
      {
        Filter = filter
      };
    }
  }
}