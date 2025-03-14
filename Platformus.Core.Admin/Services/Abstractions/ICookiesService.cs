// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Admin.Services.Abstractions;

public interface ICookiesService
{
  Task<string?> GetCookieAsync(string name);
  Task SetCookieAsync(string name, string value, int days);
  Task DeleteCookieAsync(string name);
}