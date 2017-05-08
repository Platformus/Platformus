// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.Abstractions
{
  public interface ISerializedObjectRepository : IRepository
  {
    SerializedObject WithKey(int cultureId, int objectId);
    SerializedObject WithCultureIdAndUrlPropertyStringValue(int cultureId, string urlPropertyStringValue);
    IEnumerable<SerializedObject> FilteredByCultureId(int cultureId);
    IEnumerable<SerializedObject> FilteredByClassId(int classId);
    IEnumerable<SerializedObject> Primary(int cultureId, int objectId);
    IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId);
    IEnumerable<SerializedObject> Foreign(int cultureId, int objectId);
    IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId);
    void Create(SerializedObject serializedObject);
    void Edit(SerializedObject serializedObject);
    void Delete(int cultureId, int objectId);
    void Delete(SerializedObject serializedObject);
  }
}