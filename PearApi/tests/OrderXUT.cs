using Xunit;
using PearApi.Controllers;
using PearApi.Models;
using PearApi.Test.TestDB;

namespace PearApi.Test;

[Collection("Database")]
public class OrderApiTests : DatabaseTestCase
{
   private readonly OrderController controller;
   public OrderApiTests(DatabaseFixture databaseFixture)
       : base(databaseFixture)
   {
      controller = new OrderController(DbContext);
   }

   //At the moment when I created this file the Api whas full of bugs but hopefully it works now so that you can creeate the tests for
   //the OrderController.
}