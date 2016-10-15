// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class TakeSelectorViewModelFactory : ViewModelFactoryBase
  {
    public TakeSelectorViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public TakeSelectorViewModel Create(int take)
    {
      return new TakeSelectorViewModel()
      {
        TakeOptions = new Option[] {
          new Option("By 10", "10"),
          new Option("By 25", "25"),
          new Option("By 50", "50"),
          new Option("By 100", "100")
        }
      };
    }
  }
}