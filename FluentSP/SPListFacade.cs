// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
	public class SPListFacade<TParentFacade> : BaseItemFacade<SPListFacade<TParentFacade>, TParentFacade, SPList>
		where TParentFacade : BaseFacade
	{
		public SPListFacade(TParentFacade parentFacade, SPList list)
			: base(parentFacade, list)
		{
		}

		public virtual SPListItemFacade<SPListFacade<TParentFacade>> Item(int id)
		{
			if(id < 0)
				throw new ArgumentOutOfRangeException("id");

			return new SPListItemFacade<SPListFacade<TParentFacade>>(this, DataItem.GetItemById(id));
		}

		public virtual SPListItemCollectionFacade<SPListFacade<TParentFacade>> Items()
		{
			return new SPListItemCollectionFacade<SPListFacade<TParentFacade>>(this, DataItem.GetItems());
		}

		public virtual SPListItemCollectionFacade<SPListFacade<TParentFacade>> Items(SPQuery query)
		{
			if(query == null)
				throw new ArgumentNullException("query");

			return new SPListItemCollectionFacade<SPListFacade<TParentFacade>>(this, DataItem.GetItems(query));
		}

		public virtual SPListFacade<TParentFacade> AddItems(int count, Action<SPListItem, int> action)
		{
			if(count < 1)
				throw new ArgumentOutOfRangeException("count");

			for(int c = 1; c <= count; c++)
			{
				SPListItem li = DataItem.AddItem();
				action(li, c);
				li.Update();
			}

			return GetCurrentFacade();
		}

		public virtual SPListFacade<TParentFacade> AddItem(Action<SPListItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			SPListItem li = DataItem.AddItem();
			action(li);
			li.Update();
			return GetCurrentFacade();
		}

		public virtual SPListFacade<TParentFacade> Update()
		{
			DataItem.Update();
			return GetCurrentFacade();
		}

		// TODO: Add more useful methods
	}
}