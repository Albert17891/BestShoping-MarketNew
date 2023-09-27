namespace MarketPlace.Core.Entities.Admin.Report;
public class TransactionReport
{
    public string? TransactionOwnerName { get; set; }   
    public double TransactionPrice { get; set; }
    public DateTime TransactionTime { get; set; }
    public bool IsUsedVaucer { get; set; }
    public double VaucerPrice { get; set; }
}
