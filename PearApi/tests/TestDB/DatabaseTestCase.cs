using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PearApi.Test.TestDB;

//Creating a clone of the Database Fixture context

public abstract class DatabaseTestCase : IDisposable
{
   protected DatabaseTestCase(DatabaseFixture databaseFixture)
   {
      string Id = Guid.NewGuid().ToString().Replace("-", "");
      string DbName = $"my_db_template_nr_{Id}";

      // Open connection to database and createing clone of the template
      using (var TemplateConnection = new NpgsqlConnection(databaseFixture.Connection))
      {
         TemplateConnection.Open();

         using (var cmd = new NpgsqlCommand(
             $"CREATE DATABASE {DbName} WITH TEMPLATE {databaseFixture.TemplateDatabaseName}", TemplateConnection
         ))
         {
            cmd.ExecuteNonQuery();
         }
      }
      var Connection = $"Host=localhost;Database={DbName};Username=postgres;Password=docker";
      var DbContextOption = new DbContextOptionsBuilder<PearContext>().UseNpgsql(Connection);
      DbContext = new PearContext(DbContextOption.Options);
   }
   public PearContext DbContext { get; }

   public void Dispose()
   {
      DbContext.Database.EnsureDeleted();
   }
}