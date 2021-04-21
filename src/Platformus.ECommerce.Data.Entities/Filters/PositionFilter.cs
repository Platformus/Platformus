// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.ECommerce.Filters
{
  public class PositionFilter : IFilter
  {
    public int? Id { get; set; }
    public CartFilter Cart { get; set; }
    public OrderFilter Order { get; set; }
    public ProductFilter Product { get; set; }
    public DecimalFilter Price { get; set; }
    public DecimalFilter Quantity { get; set; }
    public DecimalFilter Subtotal { get; set; }

    public PositionFilter() { }

    public PositionFilter(int? id = null, CartFilter cart = null, OrderFilter order = null, ProductFilter product = null, DecimalFilter price = null, DecimalFilter quantity = null, DecimalFilter subtotal = null)
    {
      Id = id;
      Cart = cart;
      Order = order;
      Product = product;
      Price = price;
      Quantity = quantity;
      Subtotal = subtotal;
    }
  }
}