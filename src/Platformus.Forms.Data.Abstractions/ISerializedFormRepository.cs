// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedForm"/> entities.
  /// </summary>
  public interface ISerializedFormRepository : IRepository
  {
    SerializedForm WithKey(int cultureId, int formId);
    SerializedForm WithCultureIdAndCode(int cultureId, string code);
    void Create(SerializedForm serializedForm);
    void Edit(SerializedForm serializedForm);
    void Delete(int cultureId, int formId);
    void Delete(SerializedForm serializedForm);
  }
}