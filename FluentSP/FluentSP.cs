// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public static class FluentSP 
	{
		public static SPSiteFacade<BaseFacade> CurrentSite()
		{
			return GetCurrentContext().Site.Use();
		}

		public static SPWebFacade<BaseFacade> CurrentWeb()
		{
			return GetCurrentContext().Web.Use();
		}

		public static SPListCollectionFacade<BaseFacade> CurrentLists()
		{
			return GetCurrentContext().Web.Lists.Use();
		}

		public static SPListCollectionFacade<BaseFacade> RootWebLists()
		{
			return GetCurrentContext().Site.RootWeb.Lists.Use();
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