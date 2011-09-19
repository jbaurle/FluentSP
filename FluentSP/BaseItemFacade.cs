// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;

namespace FluentSP
{
	public abstract class BaseItemFacade<TCurrentFacade, TParentFacade, TDataItem> : BaseFacade<TCurrentFacade, TParentFacade>
		where TCurrentFacade : BaseFacade
		where TParentFacade : BaseFacade
		where TDataItem : class
	{
		protected virtual TDataItem DataItem { get; set; }

		public BaseItemFacade(TParentFacade parentFacade, TDataItem dataItem)
			: base(parentFacade)
		{
			if(dataItem == null)
				throw new ArgumentNullException("dataItem");

			DataItem = dataItem;
		}

		public TDataItem Get()
		{
			return DataItem;
		}

		public virtual TCurrentFacade Do(Action<TDataItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			action(DataItem);
			return GetCurrentFacade();
		}

		public virtual TCurrentFacade Do(Action<TCurrentFacade, TDataItem> action)
		{
			if(action == null)
				throw new ArgumentNullException("action");

			action(GetCurrentFacade(), DataItem);
			return GetCurrentFacade();
		}
	}
}