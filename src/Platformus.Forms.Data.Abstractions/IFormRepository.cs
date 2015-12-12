// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.Abstractions
{
  public interface IFormRepository : IRepository
  {
    Form WithKey(int id);
    Form WithCode(string code);
    IEnumerable<Form> All();
    void Create(Form form);
    void Edit(Form form);
    void Delete(int id);
    void Delete(Form form);
  }
}