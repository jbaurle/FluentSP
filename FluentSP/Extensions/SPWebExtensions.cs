using System;
using Microsoft.SharePoint;

namespace FluentSP
{
	public static class SPWebExtensions
	{
		public static SPWebFacade WorkWith(this SPWeb web)
		{
			if(web == null)
				throw new ArgumentNullException("web");

			return new SPWebFacade(web);
		}
	}
}