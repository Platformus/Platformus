// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.Pages;
public abstract class ListPageBase<TKey, TDto, TFilter> : ComponentBase, IDisposable
  where TDto : class, IDto, new()
  where TFilter : class, IFilter, new()
{
  [Parameter, SupplyParameterFromQuery] public string? Sorting { get; set; }
  [Parameter, SupplyParameterFromQuery] public int? Offset { get; set; }
  [Parameter, SupplyParameterFromQuery] public int? Limit { get; set; }

  [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
  [Inject] protected IFilterMapper FilterMapper { get; set; } = default!;
  [Inject] protected IApiClient1K<TKey, TDto, TFilter> ApiClient { get; set; } = default!;
  [Inject] protected IStringLocalizer<Resources.Core> CoreLocalizer { get; set; } = default!;

  public void Dispose()
  {
    this.NavigationManager.LocationChanged -= OnLocationChanged;
  }

  protected override async Task OnInitializedAsync()
  {
    await LoadAsync();
    NavigationManager.LocationChanged += OnLocationChanged;
  }

  protected override async Task OnParametersSetAsync()
  {
    await LoadAsync();
    await base.OnParametersSetAsync();
  }

  private async void OnLocationChanged(object? sender, LocationChangedEventArgs e)
  {
    await LoadAsync();
  }

  protected virtual Task LoadAsync()
  {
    return Task.CompletedTask;
  }
}
