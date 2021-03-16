// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Platformus.ECommerce.Services.Abstractions
{
  public interface ICartManager
  {
    bool IsEmpty { get; }
    bool TryGetClientSideId(out Guid clientSideId);
    Task<int> GetQuantityAsync();
    Task<decimal> GetTotalAsync();
  }
}