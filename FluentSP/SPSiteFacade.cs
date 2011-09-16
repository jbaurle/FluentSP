using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPSiteFacade : BaseItemFacade<SPSiteFacade, SPSiteCollectionFacade, SPSite>
	{
		public SPSiteFacade(SPSite site)
			: base(null, site)
		{
		}

		public SPWebFacade RootWeb()
		{
			return new SPWebFacade(this, Item.RootWeb);
		}

		///// <summary>
		///// Set story state to Delivered for all stories that has a Finished state
		///// </summary>
		///// <returns>This</returns>
		//public ProjectFacade DeliverAllFinishedStories()
		//{
		//   var lStoryRepo = new Repository.PivotalStoryRepository(this.RootFacade.Token);
		//   lStoryRepo.DeliverAllFinishedStories(this.Item.Id);
		//   return this;
		//}

		//public ProjectFacade AcceptAllDeliveredStories()
		//{
		//   this.Stories().Filter("state:delivered").UpdateAll(s => {
		//      s.CurrentState = StoryStateEnum.Accepted;
		//   });

		//   return this;
		//}
	}
}