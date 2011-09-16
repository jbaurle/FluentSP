using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public static class FluentSP
	{
		public static SPSiteFacade CurrentSite()
		{
			return new SPSiteFacade(GetCurrentContext().Site);
		}

		public static SPWebFacade CurrentWeb()
		{
			return new SPWebFacade(GetCurrentContext().Web);
		}

		public static SPListCollectionFacade CurrentLists()
		{
			return new SPListCollectionFacade(GetCurrentContext().Web.Lists);
		}

		public static SPListCollectionFacade RootWebLists()
		{
			return new SPListCollectionFacade(GetCurrentContext().Site.RootWeb.Lists);
		}

		#region Helper Methods

		static SPContext GetCurrentContext()
		{
			if(SPContext.Current == null)
				throw new InvalidOperationException("SPContext not available");

			return SPContext.Current;
		}

		#endregion
	}
}