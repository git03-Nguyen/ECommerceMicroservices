using MediatR;

namespace Basket.Service.Features.Queries.BasketQueries.GetBasketsOfACustomer;

public class GetBasketOfACustomerQuery : IRequest<GetBasketOfACustomerResponse>
{
    public GetBasketOfACustomerQuery(GetBasketOfACustomerRequest payload)
    {
        Payload = payload;
    }

    public GetBasketOfACustomerRequest Payload { get; set; }
}