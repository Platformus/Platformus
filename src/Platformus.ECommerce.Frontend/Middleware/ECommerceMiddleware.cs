using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

      else await this.next(httpContext);
    }

    private async Task<IActionResult> GetCatalogActionResultAsync(HttpContext httpContext)
    {
      Category category = (await httpContext.GetStorage().GetRepository<int, Category, CategoryFilter>().GetAllAsync(
        new CategoryFilter(url: httpContext.Request.GetUrl()),
        inclusions: new Inclusion<Category>[]
        {
          new Inclusion<Category>(c => c.Name.Localizations),
          new Inclusion<Category>(c => c.Description.Localizations),
          new Inclusion<Category>(c => c.Title.Localizations),
          new Inclusion<Category>(c => c.MetaDescription.Localizations),
          new Inclusion<Category>(c => c.MetaKeywords.Localizations)
        }
      )).FirstOrDefault();

      if (category == null)
        return null;

      CategoryPageViewModel catalogPageViewModel = await CategoryPageViewModelFactory.CreateAsync(httpContext, category);

      return new ViewResult()
      {
        ViewName = "CategoryPage",
        ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = catalogPageViewModel }
      };
    }

    private async Task<IActionResult> GetProductActionResultAsync(HttpContext httpContext)
    {
      Product product = (await httpContext.GetStorage().GetRepository<int, Product, ProductFilter>().GetAllAsync(
        new ProductFilter(url: httpContext.Request.GetUrl()),
        inclusions: new Inclusion<Product>[]
        {
          new Inclusion<Product>(p => p.Category.Name.Localizations),
          new Inclusion<Product>(p => p.Name.Localizations),
          new Inclusion<Product>(p => p.Description.Localizations),
          new Inclusion<Product>(p => p.Units.Localizations),
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
  }
}