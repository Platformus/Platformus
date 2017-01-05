// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Platformus.Configurations
{
  public static class StorageConfigurationExtensions
  {
    public static IConfigurationBuilder AddStorage(this IConfigurationBuilder configurationBuilder, IStorage storage)
    { 
      configurationBuilder.Add(new StorageConfigurationSource(storage)); 
      return configurationBuilder; 
    }
  }
}