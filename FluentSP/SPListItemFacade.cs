// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using Microsoft.SharePoint;

namespace FluentSP
{
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