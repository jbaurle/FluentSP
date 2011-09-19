// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;

namespace FluentSP
{
	public class SPWebFacade<TParentFacade> : BaseItemFacade<SPWebFacade<TParentFacade>, TParentFacade, SPWeb>
		where TParentFacade : BaseFacade
	{
		public SPWebFacade(TParentFacade parentFacade, SPWeb web)
			: base(parentFacade, web)
		{
		}

		public SPWebFacade<TParentFacade> AllowUnsafeUpdates()
		{
			DataItem.AllowUnsafeUpdates = true;
			return GetCurrentFacade();
		}

		public SPWebCollectionFacade<SPWebFacade<TParentFacade>> Webs()
		{
			return Webs(l => true);
		}

		public SPWebCollectionFacade<SPWebFacade<TParentFacade>> Webs(string namePattern)
		{
			if(string.IsNullOrEmpty(namePattern))
				throw new ArgumentNullException(namePattern);

			return Webs(l => new Regex(namePattern).IsMatch(l.Title));
		}

		public SPWebCollectionFacade<SPWebFacade<TParentFacade>> Webs(Func<SPWeb, bool> predicate)
		{
			return new SPWebCollectionFacade<SPWebFacade<TParentFacade>>(this, DataItem.Webs).Where(predicate);
		}

		public SPListFacade<SPWebFacade<TParentFacade>> List(string name)
		{
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException(name);

			SPList list = null;

			DataItem.Lists.Use()
					  .Where(l => l.Title == name)
					  .IfEmpty(f => { throw new InvalidOperationException(string.Format("List with name '{0}' not found", name)); })
					  .WithFirst(l => { list = l; });

			return new SPListFacade<SPWebFacade<TParentFacade>>(this, list);
		}

		public SPListCollectionFacade<SPWebFacade<TParentFacade>> Lists()
		{
			return Lists(l => true);
		}

		public SPListCollectionFacade<SPWebFacade<TParentFacade>> Lists(string namePattern)
		{
			if(string.IsNullOrEmpty(namePattern))
				throw new ArgumentNullException(namePattern);

			return Lists(l => new Regex(namePattern).IsMatch(l.Title));
		}

		public SPListCollectionFacade<SPWebFacade<TParentFacade>> Lists(Func<SPList, bool> predicate)
		{
			return new SPListCollectionFacade<SPWebFacade<TParentFacade>>(this, DataItem.Lists).Where(predicate);
		}

		public SPSiteFacade<SPWebFacade<TParentFacade>> Site()
		{
			return new SPSiteFacade<SPWebFacade<TParentFacade>>(this, DataItem.Site);
		}
	}
}