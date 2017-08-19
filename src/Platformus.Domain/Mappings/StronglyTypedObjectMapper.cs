// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtCore.Events;
using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Events;

namespace Platformus.Domain
{
  public class StronglyTypedObjectMapper
  {
    private IRequestHandler requestHandler;
    private IClassRepository classRepository;
    private IObjectRepository objectRepository;

    public StronglyTypedObjectMapper(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
      this.classRepository = this.requestHandler.Storage.GetRepository<IClassRepository>();
      this.objectRepository = this.requestHandler.Storage.GetRepository<IObjectRepository>();
    }

    public T WithKey<T>(int id)
    {
      Class @class = this.GetValidatedClass<T>();
      Object @object = this.GetValidatedObject(@class, id);
      StronglyTypedObjectBuilder<T> stronglyTypedObjectBuilder = new StronglyTypedObjectBuilder<T>();

      new ObjectDirector(this.requestHandler).ConstructObject(stronglyTypedObjectBuilder, @object);
      return stronglyTypedObjectBuilder.Build();
    }

    public IEnumerable<T> All<T>()
    {
      Class @class = this.GetValidatedClass<T>();
      IEnumerable<Object> objects = this.objectRepository.FilteredByClassId(@class.Id);
      ObjectDirector objectDirector = new ObjectDirector(this.requestHandler);

      return objects.Select(
        o =>
        {
          StronglyTypedObjectBuilder<T> stronglyTypedObjectBuilder = new StronglyTypedObjectBuilder<T>();

          objectDirector.ConstructObject(stronglyTypedObjectBuilder, o);
          return stronglyTypedObjectBuilder.Build();
        }
      );
    }

    public void Create<T>(T obj)
    {
      Class @class = this.GetValidatedClass<T>();
      ObjectManipulator objectManipulator = new ObjectManipulator(this.requestHandler);

      objectManipulator.BeginCreateTransaction<T>();

      foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        objectManipulator.SetPropertyValue(propertyInfo.Name, propertyInfo.GetValue(obj));

      int objectId = objectManipulator.CommitTransaction();
      Object @object = this.objectRepository.WithKey(objectId);

      Event<IObjectCreatedEventHandler, IRequestHandler, Object>.Broadcast(this.requestHandler, @object);
    }

    public void Edit<T>(T obj)
    {
      Class @class = this.GetValidatedClass<T>();
      Object @object = this.GetValidatedObject(@class, this.GetId<T>(obj));
      ObjectManipulator objectManipulator = new ObjectManipulator(this.requestHandler);

      objectManipulator.BeginEditTransaction<T>(@object.Id);

      foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        objectManipulator.SetPropertyValue(propertyInfo.Name, propertyInfo.GetValue(@object));

      objectManipulator.CommitTransaction();

      Event<IObjectEditedEventHandler, IRequestHandler, Object>.Broadcast(this.requestHandler, @object);
    }

    public void Delete<T>(T obj)
    {
      Class @class = this.GetValidatedClass<T>();
      Object @object = this.GetValidatedObject(@class, this.GetId<T>(obj));

      this.requestHandler.Storage.GetRepository<IObjectRepository>().Delete(@object);
      this.requestHandler.Storage.Save();
      Event<IObjectDeletedEventHandler, IRequestHandler, Object>.Broadcast(this.requestHandler, @object);
    }

    private int GetId<T>(T @object)
    {
      int id = 0;
      PropertyInfo idPropertyInfo = typeof(T).GetProperty("Id");

      if (idPropertyInfo != null)
      {
        try
        {
          id = (int)idPropertyInfo.GetValue(@object);
        }

        catch { }
      }

      return id;
    }

    private Class GetValidatedClass<T>()
    {
      Class @class = this.classRepository.WithCode(typeof(T).Name);

      if (@class == null)
        throw new System.ArgumentException("Class code is not valid.");

      return @class;
    }

    private Object GetValidatedObject(Class @class, int id)
    {
      Object @object = this.objectRepository.WithKey(id);

      if (@object == null)
        throw new System.ArgumentException("Object identifier is not valid.");

      if (@object.ClassId != @class.Id)
        throw new System.ArgumentException("Object identifier doesn't match given class code.");

      return @object;
    }
  }
}