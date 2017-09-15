// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Dictionary"/> entities.
  /// </summary>
  public interface IDictionaryRepository : IRepository
  {
    Dictionary WithKey(int id);
    void Create(Dictionary dictionary);
    void Edit(Dictionary dictionary);
    void Delete(int id);
    void Delete(Dictionary dictionary);
  }
}