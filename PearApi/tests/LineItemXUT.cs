using Xunit;
using PearApi.Controllers;
using PearApi.Models;
using PearApi.Test.TestDB;

namespace PearApi.Test;

[Collection("Database")]
public class LineItemApiTest : DatabaseTestCase
{
   private readonly LineItemController controller;
   public LineItemApiTest(DatabaseFixture databaseFixture)
       : base(databaseFixture)
   {
      controller = new LineItemController(DbContext);
   }

   public async Task<List<LineItem>> SeedTestDbWithTwoLineItems()
   {
      var LineItems = new List<LineItem>{
            new LineItem {OrderId=1, FullProduct="Bag of corn", Uri="../"},
            new LineItem {OrderId=1, FullProduct="Bowl of oranges", Uri="../"}
        };

      DbContext.RemoveRange(DbContext.LineItems);
      await DbContext.LineItems.AddRangeAsync(LineItems);
      await DbContext.SaveChangesAsync();

      return LineItems;
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetLineItem_GetsAllLineItemsWhenNoParameter()
   {
      var LineItems = await SeedTestDbWithTwoLineItems();
      var Result = await controller.GetLineItem();
      Assert.Equal(LineItems, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetLineItem_GetsSpecificineItemWhenIdIsParam()
   {
      var LineItems = await SeedTestDbWithTwoLineItems();
      var Result = await controller.GetLineItem(2);
      Assert.Equal(2, Result.Value!.Id);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PutLineItem_UpdatesLineItem()
   {
      var ChangeingLineItem = new LineItem { OrderId = 1, FullProduct = "Bag of corn", Uri = "../" };
      DbContext.RemoveRange(DbContext.LineItems);
      await DbContext.LineItems.AddRangeAsync(new List<LineItem> { ChangeingLineItem });
      await DbContext.SaveChangesAsync();

      ChangeingLineItem.FullProduct = "Chimp Buttler";
      await controller.PutLineItem(1, ChangeingLineItem);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetLineItem(1);
      Assert.Equal(ChangeingLineItem, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PostLineItem_AddsALineItem()
   {
      var NewLineItem = new LineItem { OrderId = 1, FullProduct = "Bag of corn", Uri = "../" };

      DbContext.RemoveRange(DbContext.LineItems);
      await controller.PostLineItem(NewLineItem);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetLineItem(1);
      Assert.Equal(NewLineItem, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void DeleteLineItem_DeletesLineItem()
   {
      await SeedTestDbWithTwoLineItems();
      await controller.DeleteLineItem(1);

      var Result = await controller.GetLineItem();
      Assert.Single(Result.Value!);
   }
}