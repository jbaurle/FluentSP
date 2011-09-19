// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

namespace FluentSP
{
	public abstract class BaseFacade
	{
	}

	public abstract class BaseFacade<TCurrentFacade, TParentFacade> : BaseFacade
		where TCurrentFacade : BaseFacade
		where TParentFacade : BaseFacade
	{
		protected virtual TParentFacade ParentFacade { get; set; }

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