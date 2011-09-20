// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel=true)]
	public abstract class BaseCollectionFacade<TCurrentFacade, TParentFacade, TCollection, TCollectionItem> : BaseItemFacade<TCurrentFacade, TParentFacade, TCollection>
		where TCurrentFacade : BaseFacade
		where TParentFacade : BaseFacade
		where TCollection : SPBaseCollection
		where TCollectionItem : class
	{
		protected virtual IEnumerable<TCollectionItem> Collection { get; set; }

		public BaseCollectionFacade(TCollection collection)
			: this(null, collection)
		{
		}

		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public BaseCollectionFacade(TParentFacade parentFacade, TCollection collection)
			: base(parentFacade, collection)
		{
			if(collection == null)
				throw new ArgumentNullException("collection");

			Collection = collection.Cast<TCollectionItem>();
		}

		public IEnumerable<TCollectionItem> GetCollection()
		{
			return Collection;
		}

		public void Set(IEnumerable<TCollectionItem> collection)
		{
			if(collection == null)
				throw new ArgumentNullException("collection");

			Collection = collection;
		}

		public virtual TCurrentFacade Count(out int count)
		{
			count = Collection.Count();
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade Count(Action<int> action)
		{
			action(Collection.Count());
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade Where(Func<TCollectionItem, bool> predicate)
		{
			if(predicate == null)
				throw new ArgumentNullException("predicate");

			Collection = Collection.Where(item => predicate(item));
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade Take(int count)
		{
			if(count < 0)
				throw new ArgumentOutOfRangeException("count");

			Collection = Collection.Take(count);
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade TakeUntil(Func<TCollectionItem, bool> predicate)
		{
			if(predicate == null)
				throw new ArgumentNullException("predicate");

			Collection = Collection.TakeUntil(predicate);
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade TakeWhile(Func<TCollectionItem, bool> predicate)
		{
			if(predicate == null)
				throw new ArgumentNullException("predicate");

			Collection = Collection.TakeWhile(predicate);
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade Skip(int count)
		{
			if(count < 0)
				throw new ArgumentOutOfRangeException("count");

			Collection = Collection.Skip(count);
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade OrderBy<TKey>(Func<TCollectionItem, TKey> keySelector)
		{
			if(keySelector == null)
				throw new ArgumentNullException("keySelector");

			Collection = Collection.OrderBy(keySelector);
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade OrderByDescending<TKey>(Func<TCollectionItem, TKey> keySelector)
		{
			if(keySelector == null)
				throw new ArgumentNullException("keySelector");

			Collection = Collection.OrderByDescending(keySelector);
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade WithFirst(Action<TCollectionItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			action(Collection.FirstOrDefault());
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade WithFirstThat(Func<TCollectionItem, bool> predicate, Action<TCollectionItem> action)
		{
			if(predicate == null)
				throw new ArgumentNullException("predicate");
			if(action == null)
				throw new ArgumentNullException("action");

			action(Collection.FirstOrDefault(predicate));
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade WithLast(Action<TCollectionItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			action(Collection.LastOrDefault());
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade WithLastThat(Func<TCollectionItem, bool> predicate, Action<TCollectionItem> action)
		{
			if(predicate == null)
				throw new ArgumentNullException("predicate");
			if(action == null)
				throw new ArgumentNullException("action");

			action(Collection.LastOrDefault(predicate));
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade ForEach(Action<TCollectionItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			foreach(TCollectionItem item in Collection)
				action(item);

			return GetCurrentFacade();
		}

		public virtual TCurrentFacade ForEach(Func<TCollectionItem, bool> predicate, Action<TCollectionItem> action)
		{
			if(predicate == null)
				throw new ArgumentNullException("predicate");
			if(action == null)
				throw new ArgumentNullException("action");

			Where(predicate);

			foreach(TCollectionItem item in Collection)
				action(item);

			return GetCurrentFacade();
		}

		public virtual TCurrentFacade ForEach(Action<TCollectionItem> action, Action<TCollectionItem> firstItemAction, Action<TCollectionItem> lastItemAction)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			bool firstCalled = false;
			TCollectionItem lastItem = null;

			foreach(TCollectionItem item in Collection)
			{
				if(!firstCalled && firstItemAction != null)
				{
					firstItemAction(item);
					firstCalled = true;
				}

				action(item);

				lastItem = item;
			}

			if(firstCalled && lastItemAction != null)
				lastItemAction(lastItem);

			return GetCurrentFacade();
		}

		public virtual TCurrentFacade IfAny(Action<TCurrentFacade> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			if(Collection.Count() > 0)
				action((TCurrentFacade)(BaseFacade)this);

			return GetCurrentFacade();
		}

		public virtual TCurrentFacade IfAny(Func<TCollectionItem, bool> predicate, Action<TCurrentFacade> action)
		{
			if(predicate == null)
				throw new ArgumentNullException("predicate");
			if(action == null)
				throw new ArgumentNullException("action");

			foreach(TCollectionItem item in Collection)
			{
				if(predicate(item))
				{
					action((TCurrentFacade)(BaseFacade)this);
					break;
				}
			}

			return GetCurrentFacade();
		}

		public virtual TCurrentFacade IfEmpty(Action<TCurrentFacade> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			if(Collection.Count() == 0)
				action((TCurrentFacade)(BaseFacade)this);

			return GetCurrentFacade();
		}
	}
}