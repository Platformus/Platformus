// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="DeliveryMethod"/> entities.
  /// </summary>
  public interface IDeliveryMethodRepository : IRepository
  {
    /// <summary>
    /// Gets the delivery method by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the delivery method.</param>
    /// <returns>Found delivery method with the given identifier.</returns>
    DeliveryMethod WithKey(int id);

    /// <summary>
    /// Gets the delivery method by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the delivery method.</param>
    /// <returns>Found delivery method with the given code.</returns>
    DeliveryMethod WithCode(string code);

    /// <summary>
    /// Gets all the delivery methods using sorting by position (ascending).
    /// </summary>
    /// <returns>Found delivery methods.</returns>
    IEnumerable<DeliveryMethod> All();

    /// <summary>
    /// Gets all the delivery methods using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The delivery method property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of delivery methods that should be skipped.</param>
    /// <param name="take">The number of delivery methods that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found delivery methods using the given filtering, sorting, and paging.</returns>
    IEnumerable<DeliveryMethod> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the delivery method.
    /// </summary>
    /// <param name="deliveryMethod">The delivery method to create.</param>
    void Create(DeliveryMethod deliveryMethod);

    /// <summary>
    /// Edits the delivery method.
    /// </summary>
    /// <param name="deliveryMethod">The delivery method to edit.</param>
    void Edit(DeliveryMethod deliveryMethod);

    /// <summary>
    /// Deletes the delivery method specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the delivery method to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the delivery method.
    /// </summary>
    /// <param name="deliveryMethod">The delivery method to delete.</param>
    void Delete(DeliveryMethod deliveryMethod);

    /// <summary>
    /// Counts the number of the delivery methods with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of delivery methods found.</returns>
    int Count(string filter);
  }
}