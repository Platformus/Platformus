// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus
{
  public interface ICache
  {
    T Get<T>(string key);
    T GetWithDefaultValue<T>(string key, T defaultValue);
    void Set<T>(string key, T value);
    void Remove(string key);
  }
}