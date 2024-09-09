using User.Service.Data.Models;

namespace User.Service.Models.Dtos;

public class CustomerDto
{
    public int Id { get; set; }

    public Guid AccountId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;
    
    public CustomerDto(Customer customer)
    {
        Id = customer.CustomerId;
        AccountId = customer.AccountId;
        UserName = customer.Account.UserName;
        Email = customer.Account.Email;
        FullName = customer.FullName;
        PhoneNumber = customer.PhoneNumber;
        Address = customer.Address;
        PaymentMethod = customer.PaymentMethod;
    }
    
}