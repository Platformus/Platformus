// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.FileManager.Backend.ViewModels.Shared
{
  public class FileViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
  }
}