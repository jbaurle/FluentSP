// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Security.Permissions;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	public class SPWebCollectionFacade<TParentFacade> : BaseCollectionFacade<SPWebCollectionFacade<TParentFacade>, TParentFacade, SPWebCollection, SPWeb>
		where TParentFacade : BaseFacade
	{
		public SPWebCollectionFacade(TParentFacade parentFacade, SPWebCollection webs)
			: base(parentFacade, webs)
		{
		}

		// TODO: Add more useful methods
	}
}