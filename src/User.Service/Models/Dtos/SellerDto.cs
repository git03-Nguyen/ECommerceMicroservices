using User.Service.Data.Models;

namespace User.Service.Models.Dtos;

public class SellerDto
{
    public int Id { get; set; }

    public Guid AccountId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;
    
    public SellerDto(Seller seller)
    {
        Id = seller.SellerId;
        AccountId = seller.AccountId;
        UserName = seller.Account.UserName;
        FullName = seller.FullName;
        Email = seller.Account.Email;
        PhoneNumber = seller.PhoneNumber;
        Address = seller.Address;
        PaymentMethod = seller.PaymentMethod;
    }
}