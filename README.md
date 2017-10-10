# FluentSP

**This project is not maintained anymore.**

FluentSP implements a modern fluent interface around the classic SharePoint 2010 API.

_What is a fluent API?_

Checkout this CodeProject article [A Look at Fluent APIs](http://www.codeproject.com/KB/WPF/fluentAPI.aspx) and Wikipedia [Fluent interface](http://en.wikipedia.org/wiki/Fluent_interface)

To start into the fluent API you call the **Use()** method on _SPSite_, _SPWeb_, _SPWebCollection_ or _SPListCollection_. The Use() method is implemented as an extension method that will return the entry facade object (see facade table below). Another entry point to the fluent API is the static class **SP** with its static methods CurrentSite, CurrentWeb, CurrentLists or RootWebLists.

```cs
SPContext.Current.Site.Use()...    // => Returns the SPSiteFacade as entry point

// OR:
SP.CurrentSite()...                // => Returns the SPSiteFacade as entry point 
```

Using the entry facade instance you can start chaining the available facade methods as follows: 

```cs
SP.CurrentSite().Web("Home").List("Tasks").Items().ForEach(i => // Do something with the item i of type SPListItem...);

// OR:
SP.CurrentSite()
     .Web("Home")
       .List("Tasks")
         .Items()
         .ForEach(i => // Do something with...);
```

Each facade object is actually wrapping an underlying data item, for instance the SPSiteFacade class is the fluent wrapper of the SPSite class. Depending on what kind of facade methods you are calling the method is returning either the current facade instance (e.g., ForEach() or Where()) or the method is returning a new child facade object (e.g. Items()). During the process of chaining methods in such a way you will build up a tree or hierarchy of facade instances. In order to step back to the parent or previous facade instance you need to call the End() method:

```cs
site.Use()
       .RootWeb()
         .Site()
       .End()		// Returns SPWebFacade  as parent facade
         .Site()
       .End()		// Returns SPWebFacade  as parent facade
     .End();		// Returns SPSiteFacade as parent facade
```

FluentSP is currently missing a number of possible useful methods, but you can easily extend the FluentSP API with custom facade classes and extension methods, see below and source code for implementation examples. 

For more details about FluentSP and other SharePoint related work feel free to visit my website at [http://www.parago.net](http://www.parago.net).

**Samples:**

```cs
SPSite site = SPContext.Current.Site;

// ----------------------------

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

// ----------------------------

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
      .AddItems(7, (i, c) => i["Title"](_Title_) = "Member " + c)
       .Items()
       .Skip(2)
       .TakeUntil(i => ((string)i["Title"](_Title_)).EndsWith("6"))
       .ForEach(i => Console.WriteLine(i["Title"](_Title_)));

// ----------------------------

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

// ----------------------------

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

// ----------------------------

// Adds new items using the Do method with the passed facade object
site.Use()
    .RootWeb()
    .AllowUnsafeUpdates()
      .List("Members")
      .Do((f, l) => {
        for(int c = 1; c <= 5; c++)
          f.AddItem(i => i["Title"](_Title_) = "Standard Member #" + c);
      })
      .AddItem(i => i["Title"](_Title_) = "Premium Member")
       .Items()
        .OrderBy(i => i.Title)
        .ForEach(i => Console.WriteLine(i["Title"](_Title_)));
```

**Extensibility Samples**

```cs
// This sample is using the ThatAreCreatedBy extension method defined in Extensions.cs to show how to extend the fluent API
site.Use()
        .RootWeb()
          .Lists()
          .ThatAreCreatedBy("System Account", "jbaurle")
          .Count(c => Console.WriteLine("Lists found: {0}", c))
          .ForEach(l => Console.WriteLine(l.Title));

// This sample uses the new SPWebApplicationFacade extenion defined in SPwebApplicationFacade.cs to show how to extend the fluent API
site.WebApplication.Use()
              .Sites()
              .ForEach(i => Console.WriteLine(i.Url));

// This sample uses an alternative implementation for SPSiteFacade defined in SPSiteFacadeAlternate.cs to show how to extend the fluent API
site.WebApplication.Use().WithFirstSite().DoSomething();
site.Use<SPSiteFacadeAlternate<BaseFacade>>().DoSomething();
```

The custom method ThatAreCreatedBy which is used in the first query of the extensibility samples is implemented as follows:

```cs
static class Extensions
{
  public static SPListCollectionFacade<TParentFacade> ThatAreCreatedBy<TParentFacade>(this SPListCollectionFacade<TParentFacade> facade, params string[]() names)
    where TParentFacade : BaseFacade
  {
    // NOTE: This sample uses the GetCollection method of the given facade instance to retrieve the current 
    // collection and adds the its query (see LINQ Deferred Execution). The Set method updates the 
    // underlying collection. The GetCurrentFacade method will then return the current facade to allow 
    // method chaining.

    if(names.Length > 0)
      facade.Set(facade.GetCollection().Where(i => names.Contains(i.Author.Name)));

    return facade.GetCurrentFacade();
  }
}
```

For more samples and details check out the source code you can download from this site.

**Built-In Facades and Methods**

|| Facade || Base Class || Methods || Description ||
| SPSiteFacade | BaseItemFacade | RootWeb | Returns a SPWebFacade instance for the root web of the given site |
| | | AllWebs | Returns a SPWebCollectionFacade instance with all site webs |
| SPWebFacade | BaseItemFacade | AllowUnsafeUpdate | Sets the SPWeb.AllowUnsafeUpdate property to true for the SPWeb object |
| | | List | Returns a SPListFacade instance with the selected list |
| | | Lists | Returns a SPListCollectionFacade instance with the selected lists |
| | | Site | Returns a SPSiteFacade instance with the associated parent web |
| | | Webs | Returns a SPWebCollectionFacade instance with the selected webs |
| SPListFacade | BaseItemFacade | AddItems | Adds a given number of items to the current list |
| | | AddItem| Adds a item to the current list |
| | | Item | Returns a SPListItemFacade instance with the selected item |
| | | Items | Returns a SPListItemCollectionFacade instance with the selected items |
| | | Update | Calls the Update() method of the underlying SPList object |
| SPListItemCollectionFacade | BaseCollectionFacade | Add | Adds a new item to the current list |
|  |  | Delete | Deletes all selected items |
| SPListCollectionFacade | BaseCollectionFacade | ThatAreCreatedBy | Filters by author |
| BaseCollectionFacade | BaseItemFacade | Count | Returns the number of items in the current filtered items list |
| | | ForEach | Calls an action to execute on each item |
| | | IfAny | Calls an action if there are any filtered items available |
| | | IfEmpty | Calls an action if there no filtered items available |
| | | OrderBy | Sorts the items in an ascending order |
| | | OrderByDescending | Sorts the items in an descending order |
| | | Set | Sets the collection (needed for extensibility) |
| | | Skip | Skips a given number of items |
| | | Take | Selects a given number of items |
| | | TakeUntil | Selects items until a given criteria is true |
| | | TakeWhile | Selects items as long as a given criteria is true |
| | | Where | Filters the items |
| | | WithFirst | Calls an action on the first item |
| | | WithFirstThat | Calls an action on the first item for that a given criteria matches |
| | | WithLast | Calls an action on the last item |
| | | WithLastThat | Calls an action on the last item for that a given criteria matches |
| BaseItemFacade | BaseFacade | Do | Calls a passed delegate action for the current item |
| | | Get| Returns the underlying object |
| BaseFacade | | GetCurrentFacade | Returns current facade instance |
| | | GetParentFacade | Returns parent facade instance |
| | | End| Returns parent facade instance to step back in the facade tree |
