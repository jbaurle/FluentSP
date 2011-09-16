using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;

namespace FluentSP
{
	public abstract class BaseCollectionFacade<TCurrent, TParent, TCollection, TCollectionItem> : BaseFacade<TCurrent, TParent>
		where TCurrent : BaseFacade
		where TParent : BaseFacade
		where TCollection : SPBaseCollection
		where TCollectionItem : class
	{
		protected virtual IEnumerable<TCollectionItem> Collection { get; set; }

		public BaseCollectionFacade(TParent parent, TCollection collection)
			: base(parent)
		{
			if(collection == null)
				throw new ArgumentNullException("collection");

			Collection = collection.Cast<TCollectionItem>();
		}

		public virtual TCurrent Count(out int count)
		{
			count = Collection.Count();
			return (TCurrent)(BaseFacade)this;
		}

		public virtual TCurrent ForEach(Action<TCollectionItem> action)
		{
			return Execute(() => {
				foreach(TCollectionItem item in Collection)
					action(item);
			});
		}

		public virtual TCurrent OrderBy<TKey>(Func<TCollectionItem, TKey> keySelector)
		{
			return Execute(() => { Collection = Collection.OrderBy(keySelector); });
		}

		public virtual TCurrent OrderByDescending<TKey>(Func<TCollectionItem, TKey> keySelector)
		{
			return Execute(() => { Collection = Collection.OrderByDescending(keySelector); });
		}

		public virtual TCurrent Where(Func<TCollectionItem, bool> predicate)
		{
			return Execute(() => { Collection = Collection.Where(item => predicate(item)); });
		}

		#region Helper Methods

		protected TCurrent Execute(Action action)
		{
			action();
			return (TCurrent)(BaseFacade)this;
		}

		#endregion
	}
}