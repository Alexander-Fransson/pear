using Xunit;
using PearApi.Controllers;
using PearApi.Models;
using PearApi.Test.TestDB;

namespace PearApi.Test;

[Collection("Database")]
public class OptionGroupsApiTest : DatabaseTestCase
{
   private readonly OptionGroupController controller;
   public OptionGroupsApiTest(DatabaseFixture databaseFixture)
       : base(databaseFixture)
   {
      controller = new OptionGroupController(DbContext);
   }

   public async Task<List<OptionGroup>> SeedTestDbWithTwoOptionGroups()
   {
      var OptionGroups = new List<OptionGroup>{
            new OptionGroup{
                Type="Cpu",
                Product= new Product{
                    Name="Johan",
                    Description="Tree",
                    Price=16,
                    Discount=0.8m,
                    Category="Humunculus",
                    AmountInStorage=42,
                    Weight=88,
                    Dimensions="1 x 1 x 3"
            }},
            new OptionGroup{
                Type="Mouse",
                Product= new Product{
                    Name="Mimi",
                    Description="Pest",
                    Price=20000,
                    Discount=0.0001m,
                    Category="Pet",
                    AmountInStorage=200,
                    Weight=1,
                    Dimensions="0.1 x 0.1 * 0.2"
            }},
        };

      DbContext.RemoveRange(DbContext.OptionGroups);
      await DbContext.OptionGroups.AddRangeAsync(OptionGroups);
      await DbContext.SaveChangesAsync();

      return OptionGroups;
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetOptionGroups_GetsAllOptionGroups()
   {
      await SeedTestDbWithTwoOptionGroups();
      var Result = await controller.GetOptionGroups();
      Assert.Equal(2, Result.Value!.Count());
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetOptionGroup_GetsSpecifiedOptionGroup()
   {
      await SeedTestDbWithTwoOptionGroups();
      var Result = await controller.GetOptionGroup(1);
      Assert.Equal(1, Result.Value!.Id);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PutOptionGroup_UpdatesOptionGroup()
   {
      var ChangeingOptionGroup = new OptionGroup
      {
         Type = "Mouse",
         Product = new Product
         {
            Name = "Mimi",
            Description = "Pest",
            Price = 20000,
            Discount = 0.0001m,
            Category = "Pet",
            AmountInStorage = 200,
            Weight = 1,
            Dimensions = "0.1 x 0.1 * 0.2"
         }
      };
      DbContext.RemoveRange(DbContext.OptionGroups);
      await DbContext.AddRangeAsync(new List<OptionGroup> { ChangeingOptionGroup });
      await DbContext.SaveChangesAsync();

      ChangeingOptionGroup.Type = "Chimera";
      await controller.PutOptionGroup(1, ChangeingOptionGroup);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetOptionGroup(1);
      Assert.Equal(ChangeingOptionGroup, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PostOptionGroup_PostsAnOptionGroup()
   {
      var NewOptionGroup = new OptionGroup
      {
         Type = "Mouse",
         Product = new Product
         {
            Name = "Mimi",
            Description = "Pest",
            Price = 20000,
            Discount = 0.0001m,
            Category = "Pet",
            AmountInStorage = 200,
            Weight = 1,
            Dimensions = "0.1 x 0.1 * 0.2"
         }
      };

      DbContext.RemoveRange(DbContext.OptionGroups);
      await controller.PostOptionGroup(NewOptionGroup);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetOptionGroup(1);
      Assert.Equal(NewOptionGroup, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void DeleteOptionGroup_DeletesOptionGroup()
   {
      await SeedTestDbWithTwoOptionGroups();

      await controller.DeleteOptionGroup(2);

      var Result = await controller.GetOptionGroups();
      Assert.Single(Result.Value!);
   }
}