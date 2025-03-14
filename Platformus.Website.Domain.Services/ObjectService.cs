// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;
using System.Data.Common;
using FluentValidation;
using Magicalizer.Domain.Models.Abstractions;
using Magicalizer.Domain.Services;
using Magicalizer.Domain.Services.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Platformus.Website.Domain.Models;
using Platformus.Website.Domain.Services.Extensions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Services;

public class ObjectService : Service<int, Data.Entities.Object, Models.Object, ObjectFilter>
{
  private class TempProperty : Data.Entities.Property
  {
    public int ClientId { get; set; }

    public TempProperty(Data.Entities.Property property)
    {
      this.Id = property.Id;
      this.ObjectId = property.ObjectId;
      this.MemberId = property.MemberId;
      this.DecimalValue = property.DecimalValue;
      this.StringValue = property.StringValue;
    }
  }

  private readonly PropertyService propertyService;

  public ObjectService(DbContext dbContext, IServiceProvider serviceProvider, IService<int, Property, PropertyFilter> propertyService) : base(dbContext, serviceProvider)
  {
    this.propertyService = (PropertyService)propertyService;
  }

  public override async Task<Models.Object> CreateAsync(Models.Object @object)
  {
    this.validator?.ValidateAndThrow(@object);

    Data.Entities.Object _object = (@object as IModel<Data.Entities.Object>)!.ToEntity();

    dbContext.Add(_object);
    await dbContext.SaveChangesAsync();
    return this.EntityToModel(_object)!;
  }

  public override async Task EditAsync(Models.Object @object)
  {
    IEnumerable<Property>? properties = @object.Properties?.ToList();

    @object.Properties = null;

    await base.EditAsync(@object);

    if (properties != null)
    {
      foreach (Property property in properties)
        property.Object = @object;

      await this.MergePropertiesAsync(properties!);
    } 
  }

  public async Task MergePropertiesAsync(IEnumerable<Property> properties)
  {
    IDictionary<int, IEnumerable<LocalizedProperty>> localizedPropertiesByClientIds = new Dictionary<int, IEnumerable<LocalizedProperty>>();
    IEnumerable<TempProperty> tempProperties = properties.Select((property, index) => {
      if (property.LocalizedProperties != null)
        localizedPropertiesByClientIds[index] = property.LocalizedProperties.ToList();

      return new TempProperty(property.ToEntity()) { ClientId = index };
    }).ToList();

    IEnumerable<SqlParameter> parameters =
    [
      new SqlParameter("@Properties", tempProperties.ToDataTable()) { TypeName = "PropertiesType" }
    ];

    IDictionary<int, int> generatedIdsByClientIds = new Dictionary<int, int>();
    DbConnection connection = this.dbContext.Database.GetDbConnection();

    try
    {
      if (connection.State != ConnectionState.Open)
        await connection.OpenAsync();

      using (DbCommand command = connection.CreateCommand())
      {
        command.CommandText = "EXEC MergeProperties @Properties";
        command.Parameters.AddRange(parameters.ToArray());

        using (DbDataReader reader = await command.ExecuteReaderAsync())
          while (await reader.ReadAsync())
            generatedIdsByClientIds[reader.GetInt32("ClientId")] = reader.GetInt32("Id");
      }
    }

    finally
    {
      if (connection.State != ConnectionState.Closed)
        await connection.CloseAsync();
    }

    List<LocalizedProperty> localizedProperties = [];

    foreach (TempProperty tempProperty in tempProperties)
    {
      int propertyId = tempProperty.Id == 0 ? generatedIdsByClientIds[tempProperty.ClientId] : tempProperty.Id;
      IEnumerable<LocalizedProperty> tempLocalizedProperties = localizedPropertiesByClientIds[tempProperty.ClientId];

      foreach (LocalizedProperty tempLocalizedProperty in tempLocalizedProperties)
        tempLocalizedProperty.Property = new Property { Id = propertyId };

      localizedProperties.AddRange(tempLocalizedProperties);
    }

    await this.propertyService.MergeLocalizedPropertiesAsync(localizedProperties);
  }
}