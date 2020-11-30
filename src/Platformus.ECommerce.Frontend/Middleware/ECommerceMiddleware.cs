using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Platformus.Core.Extensions;
using Platformus.ECommerce.Data.Entities;
using Platformus.ECommerce.Filters;
using Platformus.ECommerce.Frontend.ViewModels.ECommerce;

namespace Platformus.ECommerce.Frontend.Middleware
{
  public class ECommerceMiddleware
  {
    private readonly RequestDelegate next;

    public ECommerceMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      IActionResult actionResult = await this.GetCatalogActionResultAsync(httpContext);

      if (actionResult == null)
        actionResult = await this.GetProductActionResultAsync(httpContext);

      if (actionResult != null)
        await httpContext.ExecuteResultAsync(actionResult);

      await this.next(httpContext);
    }

    private async Task<IActionResult> GetCatalogActionResultAsync(HttpContext httpContext)
    {
      Catalog catalog = (await httpContext.GetStorage().GetRepository<int, Catalog, CatalogFilter>().GetAllAsync(
        new CatalogFilter() { Url = httpContext.Request.GetUrl() },
        inclusions: new Inclusion<Catalog>[]
        {
          new Inclusion<Catalog>(c => c.Name.Localizations)
        }
      )).FirstOrDefault();

      if (catalog == null)
        return null;

      CatalogPageViewModel catalogPageViewModel = await new CatalogPageViewModelFactory().CreateAsync(httpContext, catalog);

      return new ViewResult()
      {
        ViewName = "CatalogPage",
        ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = catalogPageViewModel }
      };
    }

    private async Task<IActionResult> GetProductActionResultAsync(HttpContext httpContext)
    {
      Product product = (await httpContext.GetStorage().GetRepository<int, Product, ProductFilter>().GetAllAsync(
        new ProductFilter() { Url = httpContext.Request.GetUrl() },
        inclusions: new Inclusion<Product>[]
        {
          new Inclusion<Product>(p => p.Category.Name.Localizations),
          new Inclusion<Product>(p => p.Name.Localizations),
          new Inclusion<Product>(p => p.Description.Localizations),
          new Inclusion<Product>(p => p.Title.Localizations),
          new Inclusion<Product>(p => p.MetaDescription.Localizations),
          new Inclusion<Product>(p => p.MetaKeywords.Localizations),
          new Inclusion<Product>(p => p.Photos)
        }
      )).FirstOrDefault();

      if (product == null)
        return null;

      ProductPageViewModel productPageViewModel = new ProductPageViewModelFactory().Create(httpContext, product);

      return new ViewResult()
      {
        ViewName = "ProductPage",
        ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = productPageViewModel }
      };
    }

    private async Task<byte[]> GetResponseBodyAsync(HttpContext httpContext, IActionResult actionResult)
    {
      Stream responseBody = httpContext.Response.Body;

      using (MemoryStream buffer = new MemoryStream())
      {
        try
        {
          httpContext.Response.Body = buffer;
          await httpContext.ExecuteResultAsync(actionResult);
        }

        finally
        {
          httpContext.Response.Body = responseBody;
        }

        return buffer.ToArray();
      }
    }
  }
}