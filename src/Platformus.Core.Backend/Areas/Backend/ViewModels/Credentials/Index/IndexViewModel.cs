// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Filters;

namespace Platformus.Core.Backend.ViewModels.Credentials;

public class IndexViewModel : ViewModelBase
{
  public CredentialFilter Filter { get; set; }
  public UserViewModel User { get; set; }
  public string Sorting { get; set; }
  public int Offset { get; set; }
  public int Limit { get; set; }
  public int Total { get; set; }
  public IEnumerable<CredentialViewModel> Credentials { get; set; }
}