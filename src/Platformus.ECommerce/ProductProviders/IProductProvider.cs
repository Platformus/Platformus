// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platformus.Core;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.ProductProviders
{
  /// <summary>
  /// Describes a product provider. Product providers are selected by the users to provide products in categories.
  /// Product provider can have any logic of how (and where) to get the products, how to filter them, and how to sort them.
  /// Example: new products, best sellers, products of the week, or just products of the given category.
  /// </summary>
  public interface IProductProvider : IParameterized
  {
    /// <summary>
    /// Gets the products.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the required services from.</param>
    /// <param name="category">A category that uses the product provider.</param>
    Task<IEnumerable<Product>> GetProductsAsync(HttpContext httpContext, Category category);
  }
}