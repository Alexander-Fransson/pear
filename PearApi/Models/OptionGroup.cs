using System.ComponentModel.DataAnnotations;

namespace PearApi.Models;

public class OptionGroup
{
   public long Id { get; set; }

   public string Type { get; set; } = "";

   public Product? Product { get; set; }

   public virtual ICollection<OptionRelation>? OptionRelations { get; set; } = null!;


}