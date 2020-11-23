// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters
{
  public class UserFilter : IFilter
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public DateTimeFilter Created { get; set; }

    [FilterShortcut("Credentials[]")]
    public CredentialFilter Credential { get; set; }
  }
}