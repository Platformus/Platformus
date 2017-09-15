// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedObject"/> entities.
  /// </summary>
  public interface ISerializedObjectRepository : IRepository
  {
    SerializedObject WithKey(int cultureId, int objectId);
    SerializedObject WithCultureIdAndUrlPropertyStringValue(int cultureId, string urlPropertyStringValue);
    IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId);
    IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId, Params @params);
    IEnumerable<SerializedObject> FilteredByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params);
    IEnumerable<SerializedObject> Primary(int cultureId, int objectId);
    IEnumerable<SerializedObject> Primary(int cultureId, int objectId, Params @params);
    IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId);
    IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId, Params @params);
    IEnumerable<SerializedObject> Foreign(int cultureId, int objectId);
    IEnumerable<SerializedObject> Foreign(int cultureId, int objectId, Params @params);
    IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId);
    IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId, Params @params);
    void Create(SerializedObject serializedObject);
    void Edit(SerializedObject serializedObject);
    void Delete(int cultureId, int objectId);
    void Delete(SerializedObject serializedObject);
    int CountByCultureIdAndClassId(int cultureId, int classId, Params @params);
    int CountByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params);
  }
}