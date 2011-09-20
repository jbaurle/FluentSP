// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using Microsoft.SharePoint.Security;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
	public abstract class BaseItemFacade<TCurrentFacade, TParentFacade, TDataItem> : BaseFacade<TCurrentFacade, TParentFacade>
		where TCurrentFacade : BaseFacade
		where TParentFacade : BaseFacade
		where TDataItem : class
	{
		protected virtual TDataItem DataItem { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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