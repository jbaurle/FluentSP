// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPListCollectionFacade<TParentFacade> : BaseCollectionFacade<SPListCollectionFacade<TParentFacade>, TParentFacade, SPListCollection, SPList>
		where TParentFacade : BaseFacade
	{
		public SPListCollectionFacade(TParentFacade parentFacade, SPListCollection lists)
			: base(parentFacade, lists)
		{
		}

		public virtual SPListCollectionFacade<TParentFacade> ThatAreCreatedBy(string name)
		{
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Where(l => l.Author.Name == name);
			return GetCurrentFacade();
		}

		// TODO: Add more useful methods
	}
}