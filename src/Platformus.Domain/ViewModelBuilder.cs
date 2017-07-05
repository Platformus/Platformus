// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Dynamic;

namespace Platformus.Domain
{
  public class ViewModelBuilder
  {
    private ExpandoObject viewModel;

    public ViewModelBuilder()
    {
      this.viewModel = new ExpandoObject();
    }

    public ViewModelBuilder(dynamic viewModel)
    {
      this.viewModel = (ExpandoObject)viewModel;
    }

    public ViewModelBuilder BuildId(int id)
    {
      (this.viewModel as IDictionary<string, dynamic>).Add("Id", id);
      return this;
    }

    public ViewModelBuilder BuildProperty(string key, dynamic value)
    {
      (this.viewModel as IDictionary<string, dynamic>).Add(key, value);
      return this;
    }

    public dynamic Build()
    {
      return this.viewModel;
    }
  }
}