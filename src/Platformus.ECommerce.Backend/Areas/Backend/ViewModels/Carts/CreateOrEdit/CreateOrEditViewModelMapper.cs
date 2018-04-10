// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Cart Map(CreateOrEditViewModel createOrEdit)
    {
      Cart cart = new Cart();

      if (createOrEdit.Id != null)
        cart = this.RequestHandler.Storage.GetRepository<ICartRepository>().WithKey((int)createOrEdit.Id);

      else cart.Created = DateTime.Now;

      cart.ClientSideId = createOrEdit.ClientSideId;
      return cart;
    }
  }
}