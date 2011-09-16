using System;
using System.Diagnostics;
using Microsoft.SharePoint;

namespace FluentSP.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			using(SPSite site = new SPSite("http://win-31fru40oq82"))
			{
				int c = 0;

				//site.WorkWith().RootWeb().Lists().Where(l => l.Title.StartsWith("T")).ForEach(l => Console.WriteLine(l.Title));
				//site.WorkWith().RootWeb().Lists("Tasks").ForEach(l => Console.WriteLine(l.Title)).Count(out c);
				//site.WorkWith().RootWeb().Lists().Where(l => l.Title.StartsWith("T")).OrderBy(l => l.Title).ForEach(l => Console.WriteLine(l.Title));
				//site.WorkWith().RootWeb().Lists().Where(l => l.Title.StartsWith("T")).OrderByDescending(l => l.Title).ForEach(l => Console.WriteLine(l.Title));

				//FluentSP.CurrentSite().


			}

			//SPWebInfo

			if(!Debugger.IsAttached)
			{
				Console.Write("Press a key to exit...");
				Console.Read();
			}
		}
	}
}
