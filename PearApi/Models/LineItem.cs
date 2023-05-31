namespace PearApi.Models;

public class LineItem
{
   public long Id { get; set; }
   public long OrderId { get; set; }
   public string FullProduct { get; set; } = "";
   public string Uri { get; set; } = "";
}