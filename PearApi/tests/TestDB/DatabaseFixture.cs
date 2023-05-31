using System;
using Microsoft.EntityFrameworkCore;

namespace PearApi.Test.TestDB;

/*
    The TestDB is based on this tutorial https://dev.to/davidkudera/creating-new-postgresql-db-for-every-xunit-test-2h73
    read it for a further understanding of the concept. The short verition is that it creates a template Database that it
    than clones for use in every test file. While speeed is relative I consider this solution quite slow and not verry 
    scalable now when I use it on over twenty tests. So I recomend you read the textwall in AuthXUT.cs for my 
    recomendations on how to make it faster

    Notes:

    1) Creates a globally unique global identifyer for unique database names. For more information click the link below
    https://learn.microsoft.com/en-us/dotnet/api/system.guid.newguid?view=net-7.0

    2) This sets the settings on the webb app for connction to the the Postgres Database. It is the connectionstring 
    represented by the Connection variable that tells the system wher and how it shall connect to it. There are also 
    other functions in this class that can connect f.ex entity framework core (EFC). For more information follow ->
    https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontextoptionsbuilder?view=efcore-7.0

    3) Method that deletes the database that take affect at the end of using statements or the context "{}" where they 
    are defined -> https://learn.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-7.0
*/

public class DatabaseFixture : IDisposable
{
   private readonly PearContext _context;
   public DatabaseFixture()
   {
      string Id = Guid.NewGuid().ToString().Replace("-", "");//1)
      TemplateDatabaseName = $"my_db_template_nr_{Id}";
      Connection = $"Host=localhost;Database={TemplateDatabaseName};Username=postgres;Password=docker";

      var DbContextOption = new DbContextOptionsBuilder<PearContext>().UseNpgsql(Connection);//2)
      _context = new PearContext(DbContextOption.Options);
      _context.Database.EnsureCreated();
      _context.Database.CloseConnection();
   }
   public string TemplateDatabaseName { get; }
   public string Connection { get; }

   public void Dispose()//3)
   {
      _context.Database.EnsureDeleted();
   }
}