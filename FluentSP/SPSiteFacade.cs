// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPSiteFacade<TParentFacade> : BaseItemFacade<SPSiteFacade<TParentFacade>, TParentFacade, SPSite>
		where TParentFacade : BaseFacade
	{
		public SPSiteFacade(TParentFacade parentFacade, SPSite site)
			: base(parentFacade, site)
		{
		}

		public SPWebFacade<SPSiteFacade<TParentFacade>> Web(string name)
		{
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException(name);

			SPWeb web = null;

			DataItem.AllWebs.Use()
					  .Where(w => w.Title == name)
					  .IfEmpty(f => { throw new InvalidOperationException(string.Format("Web with name '{0}' not found", name)); })
					  .WithFirst(l => { web = l; });

			return new SPWebFacade<SPSiteFacade<TParentFacade>>(this, web);
		}

		public SPWebFacade<SPSiteFacade<TParentFacade>> RootWeb()
		{
			return new SPWebFacade<SPSiteFacade<TParentFacade>>(this, DataItem.RootWeb);
		}

		public SPWebCollectionFacade<SPSiteFacade<TParentFacade>> AllWebs()
		{
			return new SPWebCollectionFacade<SPSiteFacade<TParentFacade>>(this, DataItem.AllWebs);
		}

		// TODO: Add more useful methods
	}
}