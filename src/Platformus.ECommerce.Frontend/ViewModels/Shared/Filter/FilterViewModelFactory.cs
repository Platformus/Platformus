// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Platformus.Barebone;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.ProductProviders;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class FilterViewModelFactory : ViewModelFactoryBase
  {
    public FilterViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public FilterViewModel Create(string additionalCssClass)
    {
      string url = string.Format("/{0}", this.RequestHandler.HttpContext.GetRouteValue("url"));
      Catalog catalog = this.RequestHandler.Storage.GetRepository<ICatalogRepository>().WithUrl(url);
      IProductProvider productProvider = StringActivator.CreateInstance<IProductProvider>(catalog.CSharpClassName);
      IEnumerable<SerializedAttribute> serializedAttributes = null;

      if (productProvider != null)
        serializedAttributes = productProvider.GetAttributes(this.RequestHandler, catalog);

      List<FeatureViewModel> features = new List<FeatureViewModel>();

      foreach (SerializedAttribute serializedAttribute in serializedAttributes)
      {
        SerializedAttribute.Feature serializedFeature = JsonConvert.DeserializeObject<SerializedAttribute.Feature>(serializedAttribute.SerializedFeature);
        FeatureViewModel feature = features.FirstOrDefault(f => f.Code == serializedFeature.Code);

        if (feature == null)
        {
          feature = new FeatureViewModelFactory(this.RequestHandler).Create(serializedFeature);
          feature.Attributes = new List<AttributeViewModel>();
          features.Add(feature);
        }

        (feature.Attributes as List<AttributeViewModel>).Add(new AttributeViewModelFactory(this.RequestHandler).Create(serializedAttribute));
      }

      return new FilterViewModel()
      {
        Features = features,
        AdditionalCssClass = additionalCssClass
      };
    }
  }
}