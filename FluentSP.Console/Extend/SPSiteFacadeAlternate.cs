// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPSiteFacadeAlternate<TParentFacade> : SPSiteFacade<TParentFacade>
		where TParentFacade : BaseFacade
	{
		public SPSiteFacadeAlternate(TParentFacade parentFacade, SPSite site)
			: base(parentFacade, site)
		{
		}

		new public SPSiteFacadeAlternate<TParentFacade> GetCurrentFacade()
		{
			return this;
		}

		public SPSiteFacadeAlternate<TParentFacade> DoSomething()
		{
			// TODO: Do what you need to do here!

			return GetCurrentFacade();
		}
	}
}