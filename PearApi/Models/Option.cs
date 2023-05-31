namespace PearApi.Models;

public class Option
{
   public long Id { get; set; }

   public string Type { get; set; } = "";
   public string Name { get; set; } = "";

   public int Price { get; set; }

   public ICollection<OptionRelation>? OptionRelations { get; set; } = null!; // Needed?
}