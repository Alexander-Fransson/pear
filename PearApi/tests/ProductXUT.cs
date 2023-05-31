using Xunit;
using PearApi.Controllers;
using PearApi.Models;
using PearApi.Test.TestDB;

namespace PearApi.Test;

[Collection("Database")]
public class ProductApiTest : DatabaseTestCase
{
   private readonly ProductController controller;
   public ProductApiTest(DatabaseFixture databaseFixture)
        : base(databaseFixture)
   {
      controller = new ProductController(DbContext);
   }

   public async Task<List<Product>> SeedingTestDbWithTwoProducts()
   {
      var Products = new List<Product>{
            new Product
            {
                Name="Johan",
                Description="Tree",
                Price=16,
                Discount=0.8m,
                Category="Humunculus",
                AmountInStorage=42,
                Weight=88,
                Dimensions="1 x 1 x 3"
            },
            new Product
            {
                Name="Mimi",
                Description="Pest",
                Price=20000,
                Discount=0.0001m,
                Category="Pet",
                AmountInStorage=200,
                Weight=1,
                Dimensions="0.1 x 0.1 * 0.2"
            }
        };

      DbContext.RemoveRange(DbContext.Products);
      await DbContext.AddRangeAsync(Products);
      await DbContext.SaveChangesAsync();

      return Products;
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetProducts_GetsAllProducts()
   {
      var Products = await SeedingTestDbWithTwoProducts();
      var Result = await controller.GetProducts();
      Assert.Equal(Products, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void GetProduct_GetsSpecificProduct()
   {
      await SeedingTestDbWithTwoProducts();
      var Result = await controller.GetProduct(1);
      Assert.Equal(1, Result.Value!.Id);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PutProduct_UpdatesProduct()
   {
      var ChangeingProduct = new Product
      {
         Name = "Mimi",
         Description = "Pest",
         Price = 20000,
         Discount = 0.0001m,
         Category = "Pet",
         AmountInStorage = 200,
         Weight = 1,
         Dimensions = "0.1 x 0.1 * 0.2"
      };
      DbContext.RemoveRange(DbContext.Products);
      await DbContext.AddRangeAsync(new List<Product> { ChangeingProduct });
      await DbContext.SaveChangesAsync();

      ChangeingProduct.Name = "Oranges";
      await controller.PutProduct(1, ChangeingProduct);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetProduct(1);
      Assert.Equal(ChangeingProduct, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void PostProduct_PostsProduct()
   {
      var NewProduct = new Product
      {
         Name = "Mimi",
         Description = "Pest",
         Price = 20000,
         Discount = 0.0001m,
         Category = "Pet",
         AmountInStorage = 200,
         Weight = 1,
         Dimensions = "0.1 x 0.1 * 0.2"
      };
      DbContext.RemoveRange(DbContext.Products);
      await controller.PostProduct(NewProduct);
      await DbContext.SaveChangesAsync();

      var Result = await controller.GetProduct(1);
      Assert.Equal(NewProduct, Result.Value!);
   }

   [Fact(Skip = "Save time when testing other tests")]
   public async void DeleteProduct_DeletesTheGivenProduct()
   {
      await SeedingTestDbWithTwoProducts();

      await controller.DeleteProduct(1);

      var Result = await controller.GetProducts();
      Assert.Single(Result.Value!);
   }
}