# RandomActs5
ASP.NET 5 version of Random Acts project. Updated for RC1.

This project is a demonstration project created using RC1 of ASP.NET 5, MVC 6, and Entity Framework 7. 
it was developed by creating a new ASP.NET 5 project in Visual Studio 2015 and migrating pieces of the original 
plitwin/RandomActs project (developed with ASP.NET 4.5, MVC 5, and Entity Framework 6) into it.

I originally created the RandomActs web application for a talk on unit testing I gave a few times in 2013 and 2014. Random Acts is a fictitious site for tracking random acts of kindness. It’s sort of like EventBrite for community “do good” projects. You use it to create Acts (events), Actors (volunteers), and match actors to acts. 

The data is stored in a SQL Server database of three tables, RandomActs, RandomActors, and RandomActActors.

RandomActs goes a little further than some bare bones demo projects in that it uses repository classes and the controllers are hooked up to the repository classes using dependency injection. Also, it makes uses of Entity Framework’s lazy loading feature to count the number of actors in an act and the number of acts that a given actor is associated with. It also allows supports the concept of a waiting list when the number of actors for an act exceeds the maximum number of actors allowed.

