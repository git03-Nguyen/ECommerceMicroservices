using MediatR;

namespace User.Service.Features.Queries.SellerQueries.GetSellerByEmail;

public class GetSellerByEmailQuery : IRequest<GetSellerByEmailResponse>
{
    public GetSellerByEmailQuery(GetSellerByEmailRequest payload)
    {
        Payload = payload;
    }

    public GetSellerByEmailRequest Payload { get; set; }
}