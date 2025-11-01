using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersRequest>;
    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
}
