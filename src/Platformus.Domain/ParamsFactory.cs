// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain
{
  // TODO: name should be changed
  public class ParamsFactory
  {
    private IRequestHandler requestHandler;

    public ParamsFactory(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public Params Create(string filteringQuery = null, int? sortingMemberClassId = null, string sortingMemberCode = null, string sortingDirection = null, int? pagingSkip = null, int? pagingTake = null)
    {
      Member sortingMember = null;

      if (sortingMemberClassId != null && !string.IsNullOrEmpty(sortingMemberCode))
        sortingMember = this.requestHandler.GetService<IDomainManager>().GetMemberByClassIdAndCodeInlcudingParent((int)sortingMemberClassId, sortingMemberCode);

      return this.Create(filteringQuery, sortingMember?.Id, sortingDirection, pagingSkip, pagingTake);
    }

    public Params Create(string filteringQuery = null, int? sortingMemberId = null, string sortingDirection = null, int? pagingSkip = null, int? pagingTake = null)
    {
      Filtering filtering = null;

      if (!string.IsNullOrEmpty(filteringQuery))
        filtering = new Filtering(filteringQuery);

      Sorting sorting = null;

      if (sortingMemberId != null && !string.IsNullOrEmpty(sortingDirection))
      {
        IDomainManager domainManager = this.requestHandler.GetService<IDomainManager>();
        Member member = domainManager.GetMember((int)sortingMemberId);
        DataType dataType = domainManager.GetDataType((int)member.PropertyDataTypeId);

        sorting = new Sorting(dataType.StorageDataType, member.Id, sortingDirection);
      }

      Paging paging = null;

      if (pagingSkip != null && pagingTake != null)
        paging = new Paging(pagingSkip, pagingTake);

      return new Params(filtering, sorting, paging);
    }
  }
}