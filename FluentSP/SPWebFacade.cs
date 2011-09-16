using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPWebFacade : BaseItemFacade<SPWebFacade, SPSiteFacade, SPWeb>
	{
		public SPWebFacade(SPWeb web)
			: this(null, web)
		{
		}

		public SPWebFacade(SPSiteFacade parent, SPWeb web)
			: base(parent, web)
		{
		}

		public SPListCollectionFacade Lists()
		{
			return Lists(l => true);
		}

		public SPListCollectionFacade Lists(string name)
		{
			return Lists(l => l.Title == name);
		}

		public SPListCollectionFacade Lists(Func<SPList, bool> predicate)
		{
			return new SPListCollectionFacade(this, Item.Lists).Where(predicate);
		}

		public SPSiteFacade Site()
		{
			return new SPSiteFacade(Item.Site);
		}



		////TODO: Not tested
		///// <summary>
		///// Get a facade for the project iterations
		///// </summary>
		///// <returns>a facade that manages project iterations</returns>
		///// <remarks>Not yet implemented</remarks>
		//public IterationsFacade Iterations()
		//{
		//    throw new NotImplementedException();

		//}

		///// <summary>
		///// Get a facade to manage stories of this project
		///// </summary>
		///// <returns>a facade that will manage the stories</returns>
		//public StoriesProjectFacade Stories()
		//{
		//    var lFacade = new StoriesProjectFacade(this);
		//    return lFacade;
		//}

		///// <summary>
		///// Set story state to Delivered for all stories that has a Finished state
		///// </summary>
		///// <returns>This</returns>
		//public ProjectFacade DeliverAllFinishedStories()
		//{
		//    var lStoryRepo = new Repository.PivotalStoryRepository(this.RootFacade.Token);
		//    lStoryRepo.DeliverAllFinishedStories(this.Item.Id);
		//    return this;
		//}

		//public ProjectFacade AcceptAllDeliveredStories()
		//{
		//    this.Stories().Filter("state:delivered").UpdateAll(s =>
		//    {
		//        s.CurrentState = StoryStateEnum.Accepted;
		//    });

		//    return this;
		//}


	}
}