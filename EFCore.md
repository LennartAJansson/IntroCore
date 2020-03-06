Litet exempel på hur man arbetar med EF Core med hjälp av GenericRepository och UnitOfWork.  
Med hjälp av Microsofts Middleware teknik samt implementation av IUnitOfWork/UnitOfWork samt IGenericRepository/GenericRepository patterns så uppnår man full isolation genom Loose ♂Coupling, Encapsulation och Abstraction.  
Just a small sample of how to add EF Core to a Web Api using GenericRepository, UnitOfWork and ServiceClass. Also using the Middleware method that NET Core is built upon to achieve loose coupling. It is not supposed to be a perfect sample from architectural point of view.  

When solution is loaded and built you must run update-database to create the actual database:  
* Make sure WebApiEF is your startup project.  
* Open a Nuget Package Manager Console in WebApiEF.Db project.  
* Run the command Update-Database.  

The application DbSeeder is using the WebApi to add some sample data to test with.