// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend.Metadata
{
  public class DashboardWidget
  {
    public string ViewComponentName { get; set; }
    public int Position { get; set; }

    public DashboardWidget(string viewComponentName, int position)
    {
      this.ViewComponentName = viewComponentName;
      this.Position = position;
    }
  }
}