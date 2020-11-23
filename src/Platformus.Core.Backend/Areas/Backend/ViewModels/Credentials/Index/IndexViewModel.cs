// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.ViewModels.Credentials
{
  public class IndexViewModel : ViewModelBase
  {
    public CredentialFilter Filter { get; set; }
    public GridViewModel Grid { get; set; }
  }
}