using Contracts.Interfaces;

namespace User.Service.Data.Models;

public class Seller : ISoftDelete
{
    public int SellerId { get; set; }

    // Snapshot
    public Guid AccountId { get; set; }
    public Account Account { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    // TODO: Add PaymentMethod property
    public string PaymentMethod { get; set; } = string.Empty;


    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? DeletedAt { get; set; }
}