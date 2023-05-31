namespace PearApi.Models;

public class Product
{
   public long Id { get; set; }

   public string Name { get; set; } = "";
   public string Description { get; set; } = "";
   public int Price { get; set; }
   public decimal Discount { get; set; }
   public string Category { get; set; } = "";
   public int AmountInStorage { get; set; }
   public int Weight { get; set; }
   public string Dimensions { get; set; } = "";

   public virtual List<OptionGroup> OptionGroups { get; set; } = null!; // virtual required?

}
// dotnet-aspnet-codegenerator controller -name ProductController -async -api -m Product -dc ProductContext -outDir Controllers
// dotnet-aspnet-codegenerator controller -name OptionGroupController -async -api -m OptionGroup -dc ProductContext -outDir Controllers
// dotnet-aspnet-codegenerator controller -name OptionController -async -api -m Option -dc ProductContext -outDir Controllers