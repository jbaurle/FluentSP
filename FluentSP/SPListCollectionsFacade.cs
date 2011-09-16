using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPListCollectionFacade : BaseCollectionFacade<SPListCollectionFacade, SPWebFacade, SPListCollection, SPList>
	{
		public SPListCollectionFacade(SPListCollection lists)
			: base(null, lists)
		{
		}

		public SPListCollectionFacade(SPWebFacade parent, SPListCollection lists)
			: base(parent, lists)
		{
		}


	}
}