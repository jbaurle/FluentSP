// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
	public class SPListItemFacade<TParentFacade> : BaseItemFacade<SPListItemFacade<TParentFacade>, TParentFacade, SPListItem>
		where TParentFacade : BaseFacade
	{
		public SPListItemFacade(TParentFacade parentFacade, SPListItem listItem)
			: base(parentFacade, listItem)
		{
		}

		public virtual SPListItemFacade<TParentFacade> Update()
		{
			DataItem.Update();
			return GetCurrentFacade();
		}

		// TODO: Add more useful methods
	}
}