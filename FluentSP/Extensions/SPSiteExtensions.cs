using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public static class SPSiteExtensions
	{
		public static SPSiteFacade WorkWith(this SPSite site)
		{
			if(site == null)
				throw new ArgumentNullException("site");

			return new SPSiteFacade(site);
		}
	}
}