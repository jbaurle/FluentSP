
namespace FluentSP
{
	public abstract class BaseFacade
	{
	}

	public abstract class BaseFacade<TCurrent, TParent> : BaseFacade
		where TCurrent : BaseFacade
		where TParent : BaseFacade
	{
		protected virtual TParent Parent { get; set; }

		public BaseFacade(TParent parent)
			: base()
		{
			Parent = parent;
		}

		public virtual TParent End()
		{
			return Parent;
		}
	}
}