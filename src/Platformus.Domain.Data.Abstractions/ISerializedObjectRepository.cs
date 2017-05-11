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
    IEnumerable<SerializedObject> FilteredByClassId(int cultureId, int classId);

    // TODO: must be changed!
    IEnumerable<SerializedObject> FilteredByClassId(int cultureId, int classId, string storageDataType, int orderByMemberId, string direction);
    IEnumerable<SerializedObject> Primary(int cultureId, int objectId);

    // TODO: must be changed!
    IEnumerable<SerializedObject> Primary(int cultureId, int objectId, string storageDataType, int orderByMemberIdy, string direction);
    IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId);

    // TODO: must be changed!
    IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId, string storageDataType, int orderByMemberId, string direction);
    IEnumerable<SerializedObject> Foreign(int cultureId, int objectId);
    
    // TODO: must be changed!
    IEnumerable<SerializedObject> Foreign(int cultureId, int objectId, string storageDataType, int orderByMemberId, string direction);
    IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId);
    
    // TODO: must be changed!
    IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId, string storageDataType, int orderByMemberId, string direction);
    void Create(SerializedObject serializedObject);
    void Edit(SerializedObject serializedObject);
    void Delete(int cultureId, int objectId);
    void Delete(SerializedObject serializedObject);
  }
}