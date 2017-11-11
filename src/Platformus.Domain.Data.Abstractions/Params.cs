// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Domain.Data.Abstractions
{
  // TODO: name should be changed
  public class Params
  {
    public Filtering Filtering { get; set; }
    public Sorting Sorting { get; set; }
    public Paging Paging { get; set; }

    public Params(Filtering filtering = null, Sorting sorting = null, Paging paging = null)
    {
      this.Filtering = filtering;
      this.Sorting = sorting;
      this.Paging = paging;
    }
  }

  public class Filtering
  {
    public string Query { get; set; }

    public Filtering(string query)
    {
      this.Query = query;
    }
  }

  public class Sorting
  {
    public string StorageDataType { get; set; }
    public int MemberId { get; set; }
    public string Direction { get; set; }

    public Sorting(string storageDataType, int memberId, string direction)
    {
      this.StorageDataType = storageDataType;
      this.MemberId = memberId;
      this.Direction = direction;
    }
  }

  public class Paging
  {
    public int? Skip { get; set; }
    public int? Take { get; set; }

    public Paging(int? skip, int? take)
    {
      this.Skip = skip;
      this.Take = take;
    }
  }
}