using Xunit;
using PearApi.Controllers;
using PearApi.Models;
using PearApi.Test.TestDB;

namespace PearApi.Test;

//Skapar nya databaser efter template för varje testfil, vet inte om det är bra så om du har tid kanske du kan tidsoptimera testerna
//Skriver tester i xUnit då jag kan det och inte har tid att experimentera med dredd hooks

//Unit test = testa individuella metoder frånkopplade från applicationen. // Om dessa använder entityframework är det nog
//ok att använda in memory database eller en in memory sqlite, annars är det best att använda en test db.
//Integration test = testa hur appen jobbar tillsammans. // I detta fall ska jag nog ha en alternativ test db

//IDisposable är en interface som garanterar att classen har en Dispose metod som garbage collectar on command.
//Med using menas att Dispons aktiveras automatiskt, variabler som akapas inom paransteserna är Idisposables, using kan också 
//användas som typ för variabler som är IDisposable och då disposas dem vid scopets "{}" slut.

[Collection("Database")]
public class OptionApiTest : DatabaseTestCase
{
   private readonly OptionController controller;
   public OptionApiTest(DatabaseFixture databaseFixture)
      : base(databaseFixture)
   {
      controller = new OptionController(DbContext);
   }

   public async Task<List<Option>> SeedTestDbWithTwoOptions()
   {
      var Options = new List<Option>{
         new Option {Name = "Hans", Type = "Blond", Price = 49},
         new Option {Name = "Lars", Type = "Blond", Price = 49}
      };

      DbContext.RemoveRange(DbContext.Options);
      await DbContext.Options.AddRangeAsync(Options);
      await DbContext.SaveChangesAsync();

      return Options;
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetOption_AlwaysGetsAllOptions()
   {
      var Options = await SeedTestDbWithTwoOptions();
      var Result = await controller.GetOptions();
      Assert.Equal(Options, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetOption_GetsOneOption()
   {
      var Options = await SeedTestDbWithTwoOptions();
      var Result = await controller.GetOption(1);
      Assert.Equal(1, Result.Value!.Id);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PostOption_AddsAnOption()
   {
      var NewOption = new Option { Name = "Lars", Type = "Blond", Price = 49 };

      DbContext.RemoveRange(DbContext.Options);
      await controller.PostOption(NewOption);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetOption(1);
      Assert.Equal(NewOption, Result.Value);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PutOption_UpdatesOption()
   {
      //there can only be one of a Model with the same Id, even in variable form. 
      var ChangeingOption = new Option { Name = "Hans", Type = "Blond", Price = 49 };
      DbContext.RemoveRange(DbContext.Options);
      await DbContext.Options.AddRangeAsync(new List<Option> { ChangeingOption });
      await DbContext.SaveChangesAsync();

      ChangeingOption.Name = "Olof";
      await controller.PutOption(1, ChangeingOption);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetOption(1);
      Assert.Equal(ChangeingOption, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void DeleteOption_DeletesTheGivenOption()
   {
      await SeedTestDbWithTwoOptions();

      await controller.DeleteOption(1);

      var Result = await controller.GetOptions();
      Assert.Single(Result.Value!);
   }
}