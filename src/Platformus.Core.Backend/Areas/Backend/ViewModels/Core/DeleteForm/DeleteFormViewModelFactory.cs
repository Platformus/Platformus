﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend.ViewModels.Core
{
  public static class DeleteFormViewModelFactory
  {
    public static DeleteFormViewModel Create(string targetUrl)
    {
      return new DeleteFormViewModel()
      {
        TargetUrl = targetUrl
      };
    }
  }
}