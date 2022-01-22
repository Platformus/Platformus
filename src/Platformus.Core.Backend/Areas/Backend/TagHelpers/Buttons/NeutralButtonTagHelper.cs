// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Backend
{
  public class NeutralButtonTagHelper : ButtonTagHelperBase
  {
    public Icons? Icon { get; set; }

    protected override string GetClass()
    {
      if (this.Icon == null)
        return base.GetClass() + " button--neutral";

      return base.GetClass() + $" button--neutral button--icon icon icon--{this.Icon.ToString().ToLower()}";
    }
  }
}