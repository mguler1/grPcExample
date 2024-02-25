using AutoMapper;
using grPcExample.Models;
using OrderServiceGrpc;

namespace grPcExample
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<Order, CreateOrderResponse>();

            CreateMap<EditOrderRequest, Order>();
            CreateMap<Order, EditOrderResponse>();

            CreateMap<Order, DeleteOrderResponse>();

            CreateMap<Order, GetOrderResponse>();
        }
    }
}
