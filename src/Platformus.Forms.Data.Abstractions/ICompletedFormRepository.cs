// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  public interface ICompletedFormRepository : IRepository
  {
    CompletedForm WithKey(int id);
    IEnumerable<CompletedForm> Range(int formId, string orderBy, string direction, int skip, int take);
    void Create(CompletedForm completedForm);
    void Delete(int id);
    void Delete(CompletedForm completedForm);
    int Count(int formId);
  }
}