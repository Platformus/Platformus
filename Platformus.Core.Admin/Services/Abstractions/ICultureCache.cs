// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Api.Dto;

namespace Platformus.Core.Admin.Services.Abstractions;

public interface ICultureCache
{
  Task<IEnumerable<Culture>> GetCulturesAsync();
}