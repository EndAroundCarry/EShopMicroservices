using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(order.ShippingAdress.FirstName, order.ShippingAdress.LastName, order.ShippingAdress.EmailAddress!, order.   ShippingAdress.AddressLine, order.ShippingAdress.Country, order.ShippingAdress.State, order.ShippingAdress.ZipCode),
                BillingAddress: new AddressDto(order.BillingAdress.FirstName, order.BillingAdress.LastName, order.BillingAdress.EmailAddress!, order.BillingAdress.AddressLine, order.BillingAdress.Country, order.BillingAdress.State, order.BillingAdress.ZipCode),
                Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
            ));
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            return DtoFromOrder(order);
        }

        private static OrderDto DtoFromOrder(Order order)
        {
            return new OrderDto(
                        Id: order.Id.Value,
                        CustomerId: order.CustomerId.Value,
                        OrderName: order.OrderName.Value,
                        ShippingAddress: new AddressDto(order.ShippingAdress.FirstName, order.ShippingAdress.LastName, order.ShippingAdress.EmailAddress!, order.ShippingAdress.AddressLine, order.ShippingAdress.Country, order.ShippingAdress.State, order.ShippingAdress.ZipCode),
                        BillingAddress: new AddressDto(order.BillingAdress.FirstName, order.BillingAdress.LastName, order.BillingAdress.EmailAddress!, order.BillingAdress.AddressLine, order.BillingAdress.Country, order.BillingAdress.State, order.BillingAdress.ZipCode),
                        Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                        Status: order.Status,
                        OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                    );
        }
    }
}
