// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="PaymentMethod"/> entities.
  /// </summary>
  public interface IPaymentMethodRepository : IRepository
  {
    /// <summary>
    /// Gets the payment method by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the payment method.</param>
    /// <returns>Found payment method with the given identifier.</returns>
    PaymentMethod WithKey(int id);

    /// <summary>
    /// Gets the payment method by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the payment method.</param>
    /// <returns>Found payment method with the given code.</returns>
    PaymentMethod WithCode(string code);

    /// <summary>
    /// Gets all the payment methods using sorting by position (ascending).
    /// </summary>
    /// <returns>Found payment methods.</returns>
    IEnumerable<PaymentMethod> All();

    /// <summary>
    /// Gets all the payment methods using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The payment method property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of payment methods that should be skipped.</param>
    /// <param name="take">The number of payment methods that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found payment methods using the given filtering, sorting, and paging.</returns>
    IEnumerable<PaymentMethod> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the payment method.
    /// </summary>
    /// <param name="paymentMethod">The payment method to create.</param>
    void Create(PaymentMethod paymentMethod);

    /// <summary>
    /// Edits the payment method.
    /// </summary>
    /// <param name="paymentMethod">The payment method to edit.</param>
    void Edit(PaymentMethod paymentMethod);

    /// <summary>
    /// Deletes the payment method specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the payment method to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the payment method.
    /// </summary>
    /// <param name="paymentMethod">The payment method to delete.</param>
    void Delete(PaymentMethod paymentMethod);

    /// <summary>
    /// Counts the number of the payment methods with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of payment methods found.</returns>
    int Count(string filter);
  }
}