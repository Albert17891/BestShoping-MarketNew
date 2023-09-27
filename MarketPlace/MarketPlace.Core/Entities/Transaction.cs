using Microsoft.AspNetCore.SignalR;

namespace MarketPlace.Core.Entities;

public class Transaction
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ReceiverUserId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public bool IsUsedVaucer { get; set; } = false;
    public double VaucerPrice { get; set; }

    public double TransactionPrice { get; set; }
    public DateTime TransactionTime { get; set; }   
    
    public AppUser AppUser { get; set; }
}
