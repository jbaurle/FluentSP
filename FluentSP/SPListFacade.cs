using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPListFacade : BaseItemFacade<SPListFacade, SPListCollectionFacade, SPList>
	{
		public SPListFacade(SPList list)
			: base(null, list)
		{
		}

	}
}