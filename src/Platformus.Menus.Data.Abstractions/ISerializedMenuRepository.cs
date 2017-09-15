// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedMenu"/> entities.
  /// </summary>
  public interface ISerializedMenuRepository : IRepository
  {
    SerializedMenu WithKey(int cultureId, int menuId);
    SerializedMenu WithCultureIdAndCode(int cultureId, string code);
    void Create(SerializedMenu serializedMenu);
    void Edit(SerializedMenu serializedMenu);
    void Delete(int cultureId, int menuId);
    void Delete(SerializedMenu serializedMenu);
  }
}