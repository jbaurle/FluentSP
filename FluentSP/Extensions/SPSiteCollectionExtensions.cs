using System;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace FluentSP
{
	public static class SPListCollectionExtensions
	{
		public static SPListCollectionFacade WorkWith(this SPListCollection lists)
		{
			if(lists == null)
				throw new ArgumentNullException("lists");

			return new SPListCollectionFacade(lists);
		}
	}
}