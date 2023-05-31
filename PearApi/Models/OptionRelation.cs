namespace PearApi.Models;

public class OptionRelation
{
   public long Id { get; set; }

   public long? OptionGroupId { get; set; }
   public OptionGroup? OptionGroup { get; set; } = null!;

   public long? OptionId { get; set; }
   public Option? Option { get; set; } = null!;
}