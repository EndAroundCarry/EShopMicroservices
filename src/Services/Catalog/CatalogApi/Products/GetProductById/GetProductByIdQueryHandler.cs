
namespace CatalogApi.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        async Task<GetProductByIdResult> IRequestHandler<GetProductByIdQuery, GetProductByIdResult>.Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var prod = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if(prod is null)
            {
                throw new ProductNotFoundException(query.Id);
            }

            return new GetProductByIdResult(prod);
        }
    }
}
