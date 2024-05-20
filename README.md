# WebScraperLeet
An example of .NET scraper that fetches html-pages and saves to a local filepath. 

The architecture utilizes parallelism and asynchronous programming with a typed HttpClient as well as dependency injection
in a mediator pattern.

Clone the project, open it in an editor of your choice (preferrably in Visual Studio or Rider)
and run the application. 
Note!: 
If you would like to save the files in another directory, make sure to change
the variable localFilePath in Program.cs.
Note!: 
If you would like to scrape another website, make sure to change the base address of the typed HttpClient in program.cs