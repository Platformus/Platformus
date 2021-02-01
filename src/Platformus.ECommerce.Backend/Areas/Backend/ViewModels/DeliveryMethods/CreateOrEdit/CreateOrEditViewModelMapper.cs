// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Backend.ViewModels.DeliveryMethods
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public DeliveryMethod Map(DeliveryMethod deliveryMethod, CreateOrEditViewModel createOrEdit)
    {
      deliveryMethod.Code = createOrEdit.Code;
      deliveryMethod.Position = createOrEdit.Position;
      return deliveryMethod;
    }
  }
}