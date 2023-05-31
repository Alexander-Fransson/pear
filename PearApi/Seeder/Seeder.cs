using PearApi.Models;


public class Seeder
{


   private readonly PearContext _context;

   public Seeder(PearContext context)
   {
      this._context = context;
   }

   public void Seed()
   {
      SeedProducts();
      Console.WriteLine("Seeded Products");
      SeedOptions();
      Console.WriteLine("Seeded Options");
      SeedOptionGroups();
      Console.WriteLine("Seeded OptionGroups");
      SeedRelations();
   }

   private void SeedProducts()
   {
      if (_context.Products != null)
      {

         // Delete All existing Products
         _context.Products.RemoveRange(_context.Products);

         var products = new List<Product>() {
                    new Product() {
                        Name = "Computer",
                        Description = "This is a computer, it's great!",
                        Price = 15000,
                        Discount = 4,
                        Category = "computer",
                        AmountInStorage = 100,
                        Weight = 2500,
                        Dimensions = "40 x 15 x 4"
                    },
                    new Product() {
                        Name = "Pear Computer",
                        Description = "This is a Pear Computer, it's fine!",
                        Price = 999,
                        Discount = 35,
                        Category = "computer",
                        AmountInStorage = 50,
                        Weight = 550,
                        Dimensions = "30 x 12 x 4"
                    },
            };

         _context.Products.AddRange(products);
         _context.SaveChanges();
      }
   }
   private void SeedOptions()
   {
      if (_context.Options != null)
      {

         // Delete All existing Options
         _context.Options.RemoveRange(_context.Options);

         var posts = new List<Option>() {
                    new Option() {
                        Name = "Intel ICore 9000",
                        Type = "cpu",
                        Price = 1999
                    },
                    new Option() {
                        Name = "Logitech Mouse",
                        Type = "mouse",
                        Price = 499
                    },
                    new Option() {
                        Name = "ÖCore 1000",
                        Type = "cpu",
                        Price = 299
                    },
                    new Option() {
                        Name = "Ram memory deluxe",
                        Type = "ram",
                        Price = 2999
                    },
            };

         _context.Options.AddRange(posts);
         _context.SaveChanges();
      }
   }
   private void SeedOptionGroups()
   {
      if (_context.OptionGroups != null)
      {

         // Delete All existing OptionsGroups
         _context.OptionGroups.RemoveRange(_context.OptionGroups);

         var optionGroups = new List<OptionGroup>() {
                    new OptionGroup() {
                        Type = "cpu",
                        Product = _context.Products.First()!
                    },
                    new OptionGroup() {
                        Type = "mouse",
                        Product = _context.Products.First()!
                    },
                    new OptionGroup() {
                        Type = "cpu",
                        Product = _context.Products.OrderBy(p => p.Id).Last()!
                    },
                    new OptionGroup() {
                        Type = "ram",
                        Product = _context.Products.OrderBy(p => p.Id).Last()!
                    },
            };

         _context.OptionGroups.AddRange(optionGroups);
         _context.SaveChanges();
      }
   }

   private void SeedRelations()
   {
      if (_context.OptionRelations != null)
      {

         // Delete All existing OptionRelations
         _context.OptionRelations.RemoveRange(_context.OptionRelations);

         var optionRelations = new List<OptionRelation>() {

            // First Product
                // Connects the first cpu OptionGroup to the first cpu
                new OptionRelation() {
                    OptionGroup = _context.OptionGroups.Where(og => og.Type == "cpu").First()!,
                    Option = _context.Options.Where(o => o.Type == "cpu").First()!
                },
                // Connects the first cpu OptionGroup to the last cpu
                new OptionRelation() {
                    OptionGroup = _context.OptionGroups.Where(og => og.Type == "cpu").First()!,
                    Option = _context.Options.Where(o => o.Type == "cpu").OrderBy(o => o.Id).Last()!
                },
                // Connects the first mouse OptionGroup to the first mouse
                new OptionRelation() {
                    OptionGroup = _context.OptionGroups.Where(og => og.Type == "mouse").First()!,
                    Option = _context.Options.Where(o => o.Type == "mouse").OrderBy(o => o.Id).Last()!
                },

            // Second Product
                // First cpu to second/Last cpu optionGroup
                new OptionRelation() {
                    OptionGroup = _context.OptionGroups.Where(og => og.Type == "cpu").OrderBy(og => og.Id).Last()!,
                    Option = _context.Options.Where(o => o.Type == "cpu").First()!
                },
                // You get the point
                new OptionRelation() {
                    OptionGroup = _context.OptionGroups.Where(og => og.Type == "cpu").OrderBy(og => og.Id).Last()!,
                    Option = _context.Options.Where(o => o.Type == "cpu").OrderBy(o => o.Id).Last()!
                },
                // Adds the first ram to the first ram optionGroup
                new OptionRelation() {
                    OptionGroup = _context.OptionGroups.Where(og => og.Type == "ram").First()!,
                    Option = _context.Options.Where(o => o.Type == "ram").First()!
                },
            };

         _context.OptionRelations.AddRange(optionRelations);
         _context.SaveChanges();
      }
   }
}