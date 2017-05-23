// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Objects
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id, int? classId, int? objectId)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          Class = new ClassViewModelFactory(this.RequestHandler).Create(
            this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey((int)classId)
          ),
          MembersByTabs = this.GetMembersByTabs(null, classId, objectId)
        };

      Object @object = this.RequestHandler.Storage.GetRepository<IObjectRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = @object.Id,
        Class = new ClassViewModelFactory(this.RequestHandler).Create(
          this.RequestHandler.Storage.GetRepository<IClassRepository>().WithKey(@object.ClassId)
        ),
        MembersByTabs = this.GetMembersByTabs(@object)
      };
    }

    private IDictionary<TabViewModel, IEnumerable<MemberViewModel>> GetMembersByTabs(Object @object, int? classId = null, int? objectId = null)
    {
      Dictionary<TabViewModel, IEnumerable<MemberViewModel>> membersByTabs = new Dictionary<TabViewModel, IEnumerable<MemberViewModel>>();

      membersByTabs.Add(new TabViewModel() { Name = "General" }, new List<MemberViewModel>());

      foreach (Tab tab in this.RequestHandler.Storage.GetRepository<ITabRepository>().FilteredByClassIdInlcudingParent(@object != null ? @object.ClassId : (int)classId).ToList())
        membersByTabs.Add(new TabViewModelFactory(this.RequestHandler).Create(tab), new List<MemberViewModel>());

      foreach (Member member in this.RequestHandler.Storage.GetRepository<IMemberRepository>().FilteredByClassIdInlcudingParent(@object != null ? @object.ClassId : (int)classId).ToList())
      {
        TabViewModel tab = null;

        if (member.TabId == null)
          tab = membersByTabs.Keys.FirstOrDefault(t => t.Id == 0);

        else tab = membersByTabs.Keys.FirstOrDefault(t => t.Id == (int)member.TabId);

        (membersByTabs[tab] as List<MemberViewModel>).Add(new MemberViewModelFactory(this.RequestHandler).Create(member, @object, objectId));
      }

      return membersByTabs;
    }
  }
}