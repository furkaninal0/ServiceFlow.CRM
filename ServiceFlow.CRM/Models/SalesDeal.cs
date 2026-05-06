namespace ServiceFlow.CRM.Models;

public class SalesDeal
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = "Yeni";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;
}