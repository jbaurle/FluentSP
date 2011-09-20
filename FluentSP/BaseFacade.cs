// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using Microsoft.SharePoint.Security;

namespace FluentSP
{
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
	public abstract class BaseFacade 
	{
	}

	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true)]
	public abstract class BaseFacade<TCurrentFacade, TParentFacade> : BaseFacade
		where TCurrentFacade : BaseFacade
		where TParentFacade : BaseFacade
	{
		protected virtual TParentFacade ParentFacade { get; set; }

		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public BaseFacade(TParentFacade parentFacade)
		{
			ParentFacade = parentFacade;
		}

		public TCurrentFacade GetCurrentFacade()
		{
			return (TCurrentFacade)(BaseFacade)this;
		}

		public TParentFacade GetParentFacade()
		{
			return ParentFacade;
		}

		public virtual TParentFacade End()
		{
			return GetParentFacade();
		}
	}
}