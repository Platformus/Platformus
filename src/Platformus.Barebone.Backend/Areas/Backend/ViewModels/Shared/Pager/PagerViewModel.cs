// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class PagerViewModel : ViewModelBase
  {
    public int Skip { get; set; }
    public int Take { get; set; }
    public int Total { get; set; }
  }
}