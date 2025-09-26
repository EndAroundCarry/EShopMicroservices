
using Basket.API.Basket.GetBasket;

namespace Basket.API.Basket.DeleteBasket
{
    //not needed
    //public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteBasketCommand(userName);
                var result = await sender.Send(command, cancellationToken);
                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("DeleteBasket")
            .WithDescription("Delete Basket"); ;
        }
    }
}
