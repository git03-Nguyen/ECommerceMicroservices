using MediatR;

namespace User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

public class GetSellerByEmailQuery : IRequest<GetSellerByEmailResponse>
{
    public GetSellerByEmailRequest Payload { get; set; }
    
    public GetSellerByEmailQuery(GetSellerByEmailRequest payload)
    {
        Payload = payload;
    }
    
}