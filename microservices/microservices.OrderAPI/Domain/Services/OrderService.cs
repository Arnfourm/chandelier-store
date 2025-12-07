using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.Infrastructure.Database.DAO;

namespace microservices.OrderAPI.Domain.Services
{
    public class OrderService
    {
        private readonly IOrderDAO _orderDAO;

        private readonly IDeliveryTypeService _deliveryTypeService;
        private readonly IStatusService _statusService;

        public OrderService(
            IOrderDAO orderDAO, 
            IDeliveryTypeService deliveryTypeService,
            IStatusService statusService
        )
        {
            _orderDAO = orderDAO;

            _deliveryTypeService = deliveryTypeService;
            _statusService = statusService;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrderResponse()
        {
            List<Order> orders = await _orderDAO.GetOrders();

            List<int> statusIds = orders.Select(order => order.GetStatusId()).ToList();
            List<int> deliveryTypeIds = orders.Select(order => order.GetDeliveryTypeId()).ToList();

            IEnumerable<StatusResponse> statuses = await _statusService.GetStatusResponsesByIds(statusIds);
            IEnumerable<DeliveryTypeResponse> deliveryTypes = await _deliveryTypeService.GetDeliveryTypeResponseByIds(deliveryTypeIds);

            var statusDict = statuses.ToDictionary(statuse => statuse.Id);
            var deliveryTypeDict = deliveryTypes.ToDictionary(deliveryType => deliveryType.Id);

            IEnumerable<OrderResponse> response = orders
                .Select(order =>
                    {
                        StatusResponse statusResponse = statusDict[order.GetStatusId()];
                        DeliveryTypeResponse deliveryTypeResponse = deliveryTypeDict[order.GetDeliveryTypeId()];

                        return new OrderResponse
                        (
                                order.GetId(),
                                order.GetUserId(),
                                order.GetTotalAmount(),
                                statusResponse,
                                deliveryTypeResponse,
                                order.GetCreationDate()
                        );
                    }
                );

            return response;
        }

        public async Task<Order> GetOrderById(Guid Id)
        {
            Order order = await _orderDAO.GetOrderById(Id);

            return order;
        }

        //public async Task<OrderResponse> CreateNewOrder(OrderRequest request)
        //{
        //    // Check is user exist

        //    Order newOrder = new Order
        //    (
        //        // UserId
        //        request.TotalAmount,
        //        request.StatusId,
        //        request.DeliveryTypeId,
        //        new DateTime(DateTime.Now)
        //    );
        //}
    }
}
