// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Security;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	public class SPSiteCollectionFacade<TParentFacade> : BaseCollectionFacade<SPSiteCollectionFacade<TParentFacade>, TParentFacade, SPSiteCollection, SPSite>
		where TParentFacade : BaseFacade
	{
		public SPSiteCollectionFacade(TParentFacade parentFacade, SPSiteCollection sites)
			: base(parentFacade, sites)
		{
		}

		// TODO: Add more useful methods
	}
}