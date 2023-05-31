namespace PearApi.Models;

public class OrderItem
{
   public long Id { get; set; }
   public long CustomerId { get; set; }
   public long TotalPrice { get; set; }
   public string userAddress { get; set; } = "";
   public DateTime OrderDate { get; set; }
   public string Status { get; set; } = "";
}