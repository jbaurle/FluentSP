// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using Microsoft.SharePoint.Administration;

namespace FluentSP
{
	public class SPwebApplicationFacade<TParentFacade> : BaseItemFacade<SPwebApplicationFacade<TParentFacade>, TParentFacade, SPWebApplication>
		where TParentFacade : BaseFacade
	{
		public SPwebApplicationFacade(TParentFacade parentFacade, SPWebApplication webApplication)
			: base(parentFacade, webApplication)
		{
		}

		public SPSiteCollectionFacade<SPwebApplicationFacade<TParentFacade>> Sites()
		{
			return new SPSiteCollectionFacade<SPwebApplicationFacade<TParentFacade>>(this, DataItem.Sites);
		}

		public SPSiteFacadeAlternate<SPwebApplicationFacade<TParentFacade>> WithFirstSite()
		{
			return new SPSiteFacadeAlternate<SPwebApplicationFacade<TParentFacade>>(this, DataItem.Sites[0]);
		}
	}
}