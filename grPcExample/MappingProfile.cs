using AutoMapper;
using grPcExample.Models;
using OrderServiceGrpc;

namespace grPcExample
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
            // Gerekirse ters yönde bir harita da ekleyebilirsiniz:
            CreateMap<Order, CreateOrderRequest>();
        }
    }
}
