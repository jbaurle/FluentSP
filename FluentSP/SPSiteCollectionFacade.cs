// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FluentSP
{
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