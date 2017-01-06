// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Api.Controllers
{
  [AllowAnonymous]
  [Route("api/v1/{classCode}/objects")]
  public class ApiController : Platformus.Barebone.Controllers.ControllerBase
  {
    private IConfigurationRoot configurationRoot;

    public ApiController(IStorage storage)
      : base(storage)
    {
    }

    [HttpGet]
    public IEnumerable<dynamic> Get(string classCode)
    {
      Class @class = this.Storage.GetRepository<IClassRepository>().WithCode(classCode);
      IEnumerable<Object> objects = this.Storage.GetRepository<IObjectRepository>().FilteredByClassId(@class.Id);
      ObjectDirector objectDirector = new ObjectDirector(this);

      return objects.Select(
        o => {
          DynamicObjectBuilder objectBuilder = new DynamicObjectBuilder();

          objectDirector.ConstructObject(objectBuilder, o);
          return objectBuilder.Build();
        }
      );
    }

    [HttpGet("{id}")]
    public dynamic Get(string classCode, int id)
    {
      Object @object = this.Storage.GetRepository<IObjectRepository>().WithKey(id);
      DynamicObjectBuilder objectBuilder = new DynamicObjectBuilder();

      new ObjectDirector(this).ConstructObject(objectBuilder, @object);
      return objectBuilder.Build();
    }

    [HttpPost]
    public void Post(string classCode, [FromBody]string value)
    {
    }

    [HttpPut("{id}")]
    public void Put(string classCode, int id, [FromBody]string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(string classCode, int id)
    {
    }
  }
}