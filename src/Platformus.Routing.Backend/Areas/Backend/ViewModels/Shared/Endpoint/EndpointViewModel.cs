// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Routing.Backend.ViewModels.Shared
{
  public class EndpointViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlTemplate { get; set; }
    public int? Position { get; set; }
  }
}