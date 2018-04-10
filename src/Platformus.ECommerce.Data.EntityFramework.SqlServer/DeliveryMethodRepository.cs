// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IDeliveryMethodRepository"/> interface and represents the repository
  /// for manipulating the <see cref="DeliveryMethod"/> entities in the context of SQL Server database.
  /// </summary>
  public class DeliveryMethodRepository : RepositoryBase<DeliveryMethod>, IDeliveryMethodRepository
  {
    /// <summary>
    /// Gets the delivery method by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the delivery method.</param>
    /// <returns>Found delivery method with the given identifier.</returns>
    public DeliveryMethod WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the delivery method by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the delivery method.</param>
    /// <returns>Found delivery method with the given code.</returns>
    public DeliveryMethod WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(os => string.Equals(os.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the delivery methods using sorting by position (ascending).
    /// </summary>
    /// <returns>Found delivery methods.</returns>
    public IEnumerable<DeliveryMethod> All()
    {
      return this.dbSet.OrderBy(dm => dm.Position);
    }

    /// <summary>
    /// Gets all the delivery methods using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The delivery method property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of delivery methods that should be skipped.</param>
    /// <param name="take">The number of delivery methods that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found delivery methods using the given filtering, sorting, and paging.</returns>
    public IEnumerable<DeliveryMethod> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredDeliveryMethods(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the delivery method.
    /// </summary>
    /// <param name="deliveryMethod">The delivery method to create.</param>
    public void Create(DeliveryMethod deliveryMethod)
    {
      this.dbSet.Add(deliveryMethod);
    }

    /// <summary>
    /// Edits the delivery method.
    /// </summary>
    /// <param name="deliveryMethod">The delivery method to edit.</param>
    public void Edit(DeliveryMethod deliveryMethod)
    {
      this.storageContext.Entry(deliveryMethod).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the delivery method specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the delivery method to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the delivery method.
    /// </summary>
    /// <param name="deliveryMethod">The delivery method to delete.</param>
    public void Delete(DeliveryMethod deliveryMethod)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT NameId FROM DeliveryMethods WHERE Id = {0};
          DELETE FROM DeliveryMethods WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        deliveryMethod.Id
      );
    }

    /// <summary>
    /// Counts the number of the delivery methods with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of delivery methods found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredDeliveryMethods(dbSet, filter).Count();
    }

    private IQueryable<DeliveryMethod> GetFilteredDeliveryMethods(IQueryable<DeliveryMethod> deliveryMethods, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return deliveryMethods;

      return deliveryMethods.Where(dm => dm.Code.ToLower().Contains(filter.ToLower()));
    }
  }
}