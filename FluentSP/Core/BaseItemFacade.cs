using System;

namespace FluentSP
{
	public abstract class BaseItemFacade<TCurrent, TParent, TItem> : BaseFacade<TCurrent, TParent>
		where TCurrent : BaseFacade
		where TParent : BaseFacade
		where TItem : class
	{
		protected virtual TItem Item { get; set; }

		public BaseItemFacade(TParent parent, TItem item)
			: base(parent)
		{
			if(item == null)
				throw new ArgumentNullException("item");

			Item = item;
		}

		public virtual TCurrent Do(Action<TItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			action(Item);

			return (TCurrent)(BaseFacade)this;
		}

		public virtual TCurrent Do(Action<TCurrent, TItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			action((TCurrent)(BaseFacade)this, Item);

			return (TCurrent)(BaseFacade)this;
		}
	}
}