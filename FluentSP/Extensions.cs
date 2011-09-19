// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using Microsoft.SharePoint;

namespace FluentSP
{
	public static class Extensions
	{
		#region SPSite

		public static SPSiteFacade<BaseFacade> Use(this SPSite site)
		{
			if(site == null)
				throw new ArgumentNullException("site");

			return new SPSiteFacade<BaseFacade>(null, site);
		}

		public static TFacade Use<TFacade>(this SPSite site) where TFacade : BaseFacade
		{
			if(site == null)
				throw new ArgumentNullException("site");

			return (TFacade)Activator.CreateInstance(typeof(TFacade), new object[] { null, site });
		}

		#endregion

		#region SPWeb

		public static SPWebFacade<BaseFacade> Use(this SPWeb web)
		{
			if(web == null)
				throw new ArgumentNullException("web");

			return new SPWebFacade<BaseFacade>(null, web);
		}

		public static TFacade Use<TFacade>(this SPWeb web) where TFacade : BaseFacade
		{
			if(web == null)
				throw new ArgumentNullException("web");

			return (TFacade)Activator.CreateInstance(typeof(TFacade), new object[] { null, web });
		}

		#endregion

		#region SPWebCollection

		public static SPWebCollectionFacade<BaseFacade> Use(this SPWebCollection webs)
		{
			if(webs == null)
				throw new ArgumentNullException("webs");

			return new SPWebCollectionFacade<BaseFacade>(null, webs);
		}

		public static TFacade Use<TFacade>(this SPWebCollection webs) where TFacade : BaseFacade
		{
			if(webs == null)
				throw new ArgumentNullException("webs");

			return (TFacade)Activator.CreateInstance(typeof(TFacade), new object[] { null, webs });
		}

		#endregion

		#region SPListCollection

		public static SPListCollectionFacade<BaseFacade> Use(this SPListCollection lists)
		{
			if(lists == null)
				throw new ArgumentNullException("lists");

			return new SPListCollectionFacade<BaseFacade>(null, lists);
		}

		public static TFacade Use<TFacade>(this SPListCollection lists) where TFacade : BaseFacade
		{
			if(lists == null)
				throw new ArgumentNullException("lists");

			return (TFacade)Activator.CreateInstance(typeof(TFacade), new object[] { null, lists });
		}

		#endregion

		#region IEnumerable<TSource>

		public static IEnumerable<TSource> TakeUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if(source == null)
				throw new ArgumentNullException("source");
			if(predicate == null)
				throw new ArgumentNullException("predicate");

			foreach(TSource item in source)
			{
				if(predicate(item))
					break;

				yield return item;
			}
		}

		public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if(source == null)
				throw new ArgumentNullException("source");
			if(predicate == null)
				throw new ArgumentNullException("predicate");

			foreach(TSource item in source)
			{
				if(!predicate(item))
					break;

				yield return item;
			}
		}

		#endregion
	}
}