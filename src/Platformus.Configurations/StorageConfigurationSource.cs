// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Platformus.Configurations
{
  public class StorageConfigurationSource : IConfigurationSource
  {
    private IStorage storage;

    public StorageConfigurationSource(IStorage storage)
    {
      this.storage = storage;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
      return new StorageConfigurationProvider(this.storage);
    }
  }
}