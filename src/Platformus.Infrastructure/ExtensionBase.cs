// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Infrastructure
{
  public abstract class ExtensionBase : ExtCore.Mvc.Infrastructure.ExtensionBase, IExtension
  {
    public virtual IBackendMetadata BackendMetadata
    {
      get
      {
        return null;
      }
    }

    public virtual IFrontendMetadata FrontendMetadata
    {
      get
      {
        return null;
      }
    }
  }
}