// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Linq;
using Microsoft.SharePoint.Administration;

namespace FluentSP.Tests
{
	static class Extensions
	{
		#region SPListCollectionFacade<TParentFacade>

		public static SPListCollectionFacade<TParentFacade> ThatAreCreatedBy<TParentFacade>(this SPListCollectionFacade<TParentFacade> facade, params string[] names)
			where TParentFacade : BaseFacade
		{
			// NOTE: This sample uses the GetCollection method of the given facade instance to retrieve the current 
			// collection and adds the its query (see LINQ Deferred Execution). The Set method updates the 
			// underlying collection. The GetCurrentFacade method will then return the current facade to allow 
			// method chaining.

			if(names.Length > 0)
				facade.Set(facade.GetCollection().Where(i => names.Contains(i.Author.Name)));

			return facade.GetCurrentFacade();
		}

		#endregion

		#region SPWebApplication

		public static SPwebApplicationFacade<BaseFacade> Use(this SPWebApplication webApplication)
		{
			if(webApplication == null)
				throw new ArgumentNullException("webApplication");

			return new SPwebApplicationFacade<BaseFacade>(null, webApplication);
		}

		#endregion
	}
}
