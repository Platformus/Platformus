// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public static class GridColumnViewModelFactory
  {
    public static GridColumnViewModel Create(string displayName, string sortingName = null)
    {
      return new GridColumnViewModel()
      {
        DisplayName = displayName,
        SortingName = string.IsNullOrEmpty(sortingName) ? null : sortingName.ToLower()
      };
    }

    public static GridColumnViewModel CreateEmpty()
    {
      return new GridColumnViewModel()
      {
        DisplayName = "&nbsp;"
      };
    }
  }
}