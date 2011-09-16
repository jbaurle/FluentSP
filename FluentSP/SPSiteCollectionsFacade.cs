using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FluentSP
{
	public class SPSiteCollectionFacade : BaseCollectionFacade<SPSiteCollectionFacade, SPSiteCollectionFacade /* SPWebApplication */, SPSiteCollection, SPSite>
	{
		public SPSiteCollectionFacade(SPSiteCollection sites)
			: base(null, sites)
		{
		}
	}
}