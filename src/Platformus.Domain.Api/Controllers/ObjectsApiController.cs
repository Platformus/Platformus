// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Net;
using ExtCore.Data.Abstractions;
using ExtCore.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Events;
using Platformus.Globalization;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Domain.Api.Controllers
{
  [AllowAnonymous]
  [Route("api/v1/{classCode}/objects")]
  public class ObjectsApiController : Platformus.Barebone.Controllers.ControllerBase
  {
    public ObjectsApiController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public IEnumerable<dynamic> Get(string classCode, string filteringQuery, string sortingMemberCode, string sortingDirection, int? pagingSkip, int? pagingTake)
    {
      ISerializedObjectRepository serializedObjectRepository = this.Storage.GetRepository<ISerializedObjectRepository>();
      Class @class = this.GetValidatedClass(classCode);
      Culture frontendDefaultCulture = this.GetService<ICultureManager>().GetFrontendDefaultCulture();
      Params @params = new ParamsFactory(this).Create(filteringQuery, @class.Id, sortingMemberCode, sortingDirection, pagingSkip, pagingTake);
      IEnumerable<SerializedObject> serializedObjects = serializedObjectRepository.FilteredByCultureIdAndClassId(
        frontendDefaultCulture.Id, @class.Id, @params
      );

      ObjectDirector objectDirector = new ObjectDirector(this);

      this.Response.Headers.Add("Filtering-Query", WebUtility.UrlEncode(filteringQuery));
      this.Response.Headers.Add("Sorting-Member-Code", sortingMemberCode);
      this.Response.Headers.Add("Sorting-Direction", sortingDirection);
      this.Response.Headers.Add("Paging-Skip", pagingSkip.ToString());
      this.Response.Headers.Add("Paging-Take", pagingTake.ToString());
      this.Response.Headers.Add("Paging-Total", serializedObjectRepository.CountByCultureIdAndClassId(frontendDefaultCulture.Id, @class.Id, @params).ToString());
      return serializedObjects.Select(
        so =>
        {
          DynamicObjectBuilder objectBuilder = new DynamicObjectBuilder();

          objectDirector.ConstructObject(objectBuilder, so);
          return objectBuilder.Build();
        }
      );
    }

    [HttpGet("{id}")]
    public dynamic Get(string classCode, int id)
    {
      Class @class = this.GetValidatedClass(classCode);
      SerializedObject serializedObject = this.GetValidatedSerializedObject(@class, id);
      DynamicObjectBuilder objectBuilder = new DynamicObjectBuilder();

      new ObjectDirector(this).ConstructObject(objectBuilder, serializedObject);
      return objectBuilder.Build();
    }

    [HttpPost]
    public void Post(string classCode, [FromBody]JObject obj)
    {
      Class @class = this.GetValidatedClass(classCode);
      ObjectManipulator objectManipulator = new ObjectManipulator(this);

      objectManipulator.BeginCreateTransaction(classCode);

      foreach (JProperty property in obj.Properties())
      {
        try
        {
          objectManipulator.SetPropertyValue(property.Name, property.Value);
        }

        catch (System.ArgumentException e)
        {
          throw new HttpException(400, e.Message);
        }
      }

      int objectId = objectManipulator.CommitTransaction();
      Object @object = this.Storage.GetRepository<IObjectRepository>().WithKey(objectId);

      Event<IObjectCreatedEventHandler, IRequestHandler, Object>.Broadcast(this, @object);
    }

    [HttpPut("{id}")]
    public void Put(string classCode, int id, [FromBody]JObject obj)
    {
      Class @class = this.GetValidatedClass(classCode);
      Object @object = this.GetValidatedObject(@class, id);
      ObjectManipulator objectManipulator = new ObjectManipulator(this);

      objectManipulator.BeginEditTransaction(classCode, id);

      foreach (JProperty property in obj.Properties())
      {
        try
        {
          objectManipulator.SetPropertyValue(property.Name, property.Value);
        }

        catch (System.ArgumentException e)
        {
          throw new HttpException(400, e.Message);
        }
      }

      objectManipulator.CommitTransaction();
      Event<IObjectEditedEventHandler, IRequestHandler, Object>.Broadcast(this, @object);
    }

    [HttpDelete("{id}")]
    public void Delete(string classCode, int id)
    {
      Class @class = this.GetValidatedClass(classCode);
      Object @object = this.GetValidatedObject(@class, id);

      this.Storage.GetRepository<IObjectRepository>().Delete(@object);
      this.Storage.Save();
      Event<IObjectDeletedEventHandler, IRequestHandler, Object>.Broadcast(this, @object);
    }

    private Class GetValidatedClass(string classCode)
    {
      Class @class = this.GetService<IDomainManager>().GetClass(classCode);

      if (@class == null)
        throw new HttpException(400, "Class code is not valid.");

      return @class;
    }

    private Object GetValidatedObject(Class @class, int id)
    {
      Object @object = this.Storage.GetRepository<IObjectRepository>().WithKey(id);

      if (@object == null)
        throw new HttpException(400, "Object identifier is not valid.");

      if (@object.ClassId != @class.Id)
        throw new HttpException(400, "Object identifier doesn't match given class code.");

      return @object;
    }

    private SerializedObject GetValidatedSerializedObject(Class @class, int id)
    {
      SerializedObject serializedObject = this.Storage.GetRepository<ISerializedObjectRepository>().WithKey(this.GetService<ICultureManager>().GetFrontendDefaultCulture().Id, id);

      if (serializedObject == null)
        throw new HttpException(400, "Object identifier is not valid.");

      if (serializedObject.ClassId != @class.Id)
        throw new HttpException(400, "Object identifier doesn't match given class code.");

      return serializedObject;
    }
  }
}