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

namespace Platformus.ECommerce.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IPaymentMethodRepository"/> interface and represents the repository
  /// for manipulating the <see cref="PaymentMethod"/> entities in the context of SQLite database.
  /// </summary>
  public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
  {
    /// <summary>
    /// Gets the payment method by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the payment method.</param>
    /// <returns>Found payment method with the given identifier.</returns>
    public PaymentMethod WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the payment method by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the payment method.</param>
    /// <returns>Found payment method with the given code.</returns>
    public PaymentMethod WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(os => string.Equals(os.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the payment methods using sorting by position (ascending).
    /// </summary>
    /// <returns>Found payment methods.</returns>
    public IEnumerable<PaymentMethod> All()
    {
      return this.dbSet.OrderBy(pm => pm.Position);
    }

    /// <summary>
    /// Gets all the payment methods using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The payment method property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of payment methods that should be skipped.</param>
    /// <param name="take">The number of payment methods that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found payment methods using the given filtering, sorting, and paging.</returns>
    public IEnumerable<PaymentMethod> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredPaymentMethods(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the payment method.
    /// </summary>
    /// <param name="paymentMethod">The payment method to create.</param>
    public void Create(PaymentMethod paymentMethod)
    {
      this.dbSet.Add(paymentMethod);
    }

    /// <summary>
    /// Edits the payment method.
    /// </summary>
    /// <param name="paymentMethod">The payment method to edit.</param>
    public void Edit(PaymentMethod paymentMethod)
    {
      this.storageContext.Entry(paymentMethod).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the payment method specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the payment method to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the payment method.
    /// </summary>
    /// <param name="paymentMethod">The payment method to delete.</param>
    public void Delete(PaymentMethod paymentMethod)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TEMP TABLE TempDictionaries (Id INT PRIMARY KEY);
          INSERT INTO TempDictionaries SELECT NameId FROM PaymentMethods WHERE Id = {0};
          DELETE FROM PaymentMethods WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM TempDictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM TempDictionaries);
        ",
        paymentMethod.Id
      );
    }

    /// <summary>
    /// Counts the number of the payment methods with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of payment methods found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredPaymentMethods(dbSet, filter).Count();
    }

    private IQueryable<PaymentMethod> GetFilteredPaymentMethods(IQueryable<PaymentMethod> paymentMethods, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return paymentMethods;

      return paymentMethods.Where(pm => pm.Code.Contains(filter.ToLower()));
    }
  }
}