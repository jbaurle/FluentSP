// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.SharePoint;

namespace FluentSP.Tests
{
	// NOTE: Visual Studio 2010 integrated Test project type cannot be used because we can not
	// change the target framework to .NET 3.5.

#pragma warning disable 0162
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				// NOTE: You need to adjust the queries so they will work in your SharePoint environment
				// using your lists. You also need to change the server name below.

				using(SPSite site = new SPSite("http://win-31fru40oq82"))
				{
					// ------------------------------------------------------------------------------

					if(false)
					{
						int c;

						// Outputs titles of all lists of the root web where the list title starts with T
						site.Use().RootWeb().Lists().Where(l => l.Title.StartsWith("T")).ForEach(l => Console.WriteLine(l.Title));

						// Outputs titles of all lists of the root web where the list title ends with a ts (using RegEx)
						site.Use().RootWeb().Lists("ts$").ForEach(l => Console.WriteLine(l.Title)).Count(out c);

						// Outputs titles of all lists of the root web in ascending order where the starts with T
						site.Use().RootWeb().Lists().Where(l => l.Title.StartsWith("T")).OrderBy(l => l.Title).ForEach(l => Console.WriteLine(l.Title));

						// Outputs titles of all lists of the root web in descending order where the starts with T
						site.Use()
								.RootWeb()
								  .Lists()
								  .Where(l => l.Title.StartsWith("T"))
								  .OrderByDescending(l => l.Title)
								  .ForEach(l => Console.WriteLine(l.Title));
					}

					// ------------------------------------------------------------------------------

					if(false)
					{
						// Queries the Home web by using an overrite of the ForEach method that is combining a Where and interator
						site.Use()
								.Web("Home")
								  .Lists()
								  .ForEach(l => l.Title.StartsWith("F"),
											  l => Console.WriteLine(l.Title));
					}

					// ------------------------------------------------------------------------------

					if(false)
					{
						// Delete all items in the Members list, then add 7 new members and then select and output 
						// the titles of a few of the newly created items
						site.Use()
								.RootWeb()
								  .List("Members")
								  .Do(w => Console.WriteLine("Deleting all members..."))
									 .Items()
									 .Delete()
								  .End()
								  .Do(w => Console.WriteLine("Adding all members..."))
								  .AddItems(7, (i, c) => i["Title"] = "Member " + c)
									 .Items()
									 .Skip(2)
									 .TakeUntil(i => ((string)i["Title"]).EndsWith("6"))
									 .ForEach(i => Console.WriteLine(i["Title"]));
					}

					// ------------------------------------------------------------------------------

					if(false)
					{
						// Search for lists that are created by specific a user and depending on the results
						// displays different messages by calling the IfAny or IfEmpty methods
						site.Use()
								.RootWeb()
								  .Lists()
								  .ThatAreCreatedBy("Unknown User")
								  .IfAny(f => f.ForEach(l => Console.WriteLine(l.Title)))
								  .IfAny(l => l.Title.StartsWith("M"), f => Console.WriteLine("Lists found that starts with M*"))
								  .IfEmpty(f => Console.WriteLine("No lists found for user"))
								.End()
								.Do(w => Console.WriteLine("---"))
								  .Lists()
								  .ThatAreCreatedBy("System Account")
								  .IfAny(f => f.ForEach(l => Console.WriteLine(l.Title)));
					}

					// ------------------------------------------------------------------------------

					if(false)
					{
						var items = new List<SPListItem>();

						// Query with Skip and TakeUnitl methods
						site.Use().RootWeb().List("Members").Items().Skip(2).TakeUntil(i => i.Title.EndsWith("5")).ForEach(i => { items.Add(i); Console.WriteLine(i.Title); });

						// Query with Skip and TakeWhile methods
						site.Use()
								.RootWeb()
								  .List("Members")
									 .Items()
									 .Skip(2)
									 .TakeWhile(i => i.Title.StartsWith("Member"))
									 .ForEach(i => { items.Add(i); Console.WriteLine(i.Title); })
								  .End()
									 .Items()
									 .Where(i => i.Title == "XYZ")
									 .ForEach(i => { items.Add(i); Console.WriteLine(i.Title); });

					}

					// ------------------------------------------------------------------------------

					if(false)
					{
						// Adds new items using the Do method with the passed facade object
						site.Use()
								.RootWeb()
								.AllowUnsafeUpdates()
								  .List("Members")
								  .Do((f, l) => {
									  for(int c = 1; c <= 5; c++)
										  f.AddItem(i => i["Title"] = "Standard Member #" + c);
								  })
								  .AddItem(i => i["Title"] = "Premium Member")
									 .Items()
										.OrderBy(i => i.Title)
										.ForEach(i => Console.WriteLine(i["Title"]));
					}

					// ------------------------------------------------------------------------------

					if(false)
					{
						int c;

						site.Use().RootWeb().Webs().Count(out c).ForEach(w => Console.WriteLine(w.Title));
						site.Use().RootWeb().Webs().Count(n => Console.WriteLine(n)).ForEach(w => Console.WriteLine(w.Title));

						site.Use()
								.RootWeb()
								  .List("Members")
									 .Items()
									 .Take(2)
									 .ForEach(i => Console.WriteLine(i["Title"]),	// Called action for all   items
												 i => Console.WriteLine("START"),		// Called action for first item
												 i => Console.WriteLine("END"));			// Called action for last  item
					}

					// ------------------------------------------------------------------------------

					if(true)
					{
						// Returning all lists of the root web where the list title matches the regular expression
						site.Use()
								.RootWeb()
								  .Lists("s$") // RegEx
								  .ForEach(l => Console.WriteLine(l.Title));

						// The Get method always returns the underlying data item, in this case the SPSite instance
						var s = site.Use().Get();
					}

					// ------------------------------------------------------------------------------

					if(false)
					{
						// Sample using the End method and its resulting parent facade object

						var f1 = site.Use()
											.AllWebs()
										 .End();					// Returns SPSiteFacade as parent facade

						var f2 = site.Use()
											.RootWeb()
											  .Site()
											.End();				// Returns SPWebFacade  as parent facade

						var f3 = site.Use()
											.RootWeb()
											  .Site()
											.End()				// Returns SPWebFacade  as parent facade
											  .Site()
											.End()				// Returns SPWebFacade  as parent facade
										 .End();					// Returns SPSiteFacade as parent facade

						var f4 = site.Use()
											.RootWeb()
											  .Site()
											.End()				// Returns SPWebFacade  as parent facade
										 .End()					// Returns SPSiteFacade as parent facade
									  .End();					// Returns null
					}

					// ------------------------------------------------------------------------------

					if(true)
					{
						// This sample is using the ThatAreCreatedBy extension method defined in Extensions.cs 
						// to show how to extend the fluent API
						site.Use()
								.RootWeb()
								  .Lists()
								  .ThatAreCreatedBy("System Account", "jbaurle")
								  .Count(c => Console.WriteLine("Lists found: {0}", c))
								  .ForEach(l => Console.WriteLine(l.Title));

						// This sample uses the new SPWebApplicationFacade extenion defined in SPwebApplicationFacade.cs
						// to show how to extend the fluent API
						site.WebApplication.Use()
													.Sites()
													.ForEach(i => Console.WriteLine(i.Url));

						// This sample uses an alternative implementation for SPSiteFacade defined in SPSiteFacadeAlternate.cs
						// to show how to extend the fluent API
						site.WebApplication.Use().WithFirstSite().DoSomething();
						site.Use<SPSiteFacadeAlternate<BaseFacade>>().DoSomething();
					}

					// ------------------------------------------------------------------------------
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}

			if(Debugger.IsAttached)
			{
				Console.Write("Press any key to continue . . .");
				Console.ReadKey();
			}
		}
	}
}
