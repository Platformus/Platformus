// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using BlazorBootstrap;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Admin.Extensions;

public static class ConfirmDialogExtensions
{
  public static async Task<bool> ShowDeleteConfirmationAsync(this ConfirmDialog dialog, IStringLocalizer localizer)
  {
    return await dialog.ShowAsync(
      localizer["DeleteConfirmation.Title"],
      localizer["DeleteConfirmation.Message"],
      new ConfirmDialogOptions
      {
        YesButtonText = localizer["Yes"],
        YesButtonColor = ButtonColor.Danger,
        NoButtonText = localizer["No"]
      }
    );
  }
}