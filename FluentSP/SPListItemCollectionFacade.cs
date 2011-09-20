// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Linq;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
	public class SPListItemCollectionFacade<TParentFacade> : BaseCollectionFacade<SPListItemCollectionFacade<TParentFacade>, TParentFacade, SPListItemCollection, SPListItem>
		where TParentFacade : BaseFacade
	{
		public SPListItemCollectionFacade(TParentFacade parentFacade, SPListItemCollection listItems)
			: base(parentFacade, listItems)
		{
		}

		public virtual SPListItemCollectionFacade<TParentFacade> Add(Action<SPListItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			SPListItem li = DataItem.Add();
			action(li);
			li.Update();
			return GetCurrentFacade();
		}

		public virtual SPListItemCollectionFacade<TParentFacade> Delete()
		{
			var collection = Collection.ToList();
	
			foreach(var item in collection)
				Get().DeleteItemById(item.ID);

			return GetCurrentFacade();
		}

		// TODO: Add more useful methods
	}
}