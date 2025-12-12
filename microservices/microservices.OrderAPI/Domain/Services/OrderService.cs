using System.Net.Http.Headers;
using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.DTO.Responses;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAO _orderDAO;

        private readonly IDeliveryTypeService _deliveryTypeService;
        private readonly IStatusService _statusService;
        private readonly ITokenService _tokenService;

        private readonly string _userService;

        public OrderService(
            IOrderDAO orderDAO, 
            IDeliveryTypeService deliveryTypeService,
            IStatusService statusService,
            ITokenService tokenService,
            IConfiguration config
        )
        {
            _orderDAO = orderDAO;

            _deliveryTypeService = deliveryTypeService;
            _statusService = statusService;
            _tokenService = tokenService;

            _userService = config["Microservices:UserMicroservice:Url"]
                ?? throw new ArgumentException("User microservice url is null");
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrderResponseAsync()
        {
            string token = await _tokenService.GetTokenAsync();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var userRequest = new HttpRequestMessage(HttpMethod.Get, $"{_userService}/User/");

                    userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);

                    if (!userResponse.IsSuccessStatusCode) 
                        throw new Exception($"Can't send request to user service");
                }   
            }

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
                                order.GetTotalAmount(),
                                statusResponse,
                                deliveryTypeResponse,
                                order.GetCreationDate()
                        );
                    }
                );

            return response;
        }

        public async Task<Order> GetOrderByIdAsync(Guid Id)
        {
            Order order = await _orderDAO.GetOrderById(Id);

            return order;
        }

        public async Task<OrderResponse> CreateNewOrderAsync(OrderRequest request)
        {
            string token = await _tokenService.GetTokenAsync();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var userRequest = new HttpRequestMessage(HttpMethod.Get, $"{_userService}/User/{request.UserId}");

                    userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);

                    if (!userResponse.IsSuccessStatusCode) 
                        throw new ArgumentException($"User with id {request.UserId} not found");
                }
            }

            Order newOrder = new Order
            (
                request.UserId,
                request.TotalAmount,
                request.StatusId,
                request.DeliveryTypeId,
                DateTime.UtcNow
            );

            Order order = await _orderDAO.CreateOrder(newOrder);

            StatusResponse statusResponse = await _statusService.GetStatusResponseByIdAsync(order.GetStatusId());
            DeliveryTypeResponse deliveryTypeResponse = await _deliveryTypeService.GetDeliveryTypeResponseByIdAsync(order.GetDeliveryTypeId());

            OrderResponse orderResponse = new OrderResponse
            (
                order.GetId(),
                order.GetTotalAmount(),
                statusResponse,
                deliveryTypeResponse,
                order.GetCreationDate()
            );

            return orderResponse;
        }

        public async Task UpdateSingleOrderByIdAsync(Guid id, OrderRequest request)
        {
            Order checkOrder = await _orderDAO.GetOrderById(id);

            if (checkOrder == null)
                throw new ArgumentException($"Order with id: {id} not found");

            if (request.UserId != checkOrder.GetUserId())
                throw new ArgumentException($"User id: {request.UserId} is uncorrect");

            string token = await _tokenService.GetTokenAsync();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var userRequest = new HttpRequestMessage(HttpMethod.Get, $"{_userService}/User/{request.UserId}");

                    userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage response = await httpClient.SendAsync(userRequest);

                    if (!response.IsSuccessStatusCode) 
                        throw new ArgumentException($"User with id {request.UserId} not found");
                }
            }

            Status checkStatus = await _statusService.GetStatusById(request.StatusId);
            
            if (checkStatus == null)
                throw new ArgumentException($"Status with id: {id} not found");
            
            DeliveryType checkDeliveryType = await _deliveryTypeService.GetSingleDeliveryTypeById(request.DeliveryTypeId);

            if (checkDeliveryType == null)
                throw new ArgumentException($"Delivery type with id: {id} not found");

            Order updateOrder = new Order
            (
                checkOrder.GetId(),
                checkOrder.GetUserId(),
                request.TotalAmount,
                checkStatus.GetId(),
                checkDeliveryType.GetId(),
                checkOrder.GetCreationDate()
            );

            await _orderDAO.UpdateOrder(updateOrder);
        }

        public async Task DeleteSingleOrderByIdAsync(Guid id)
        {
            await _orderDAO.DeleteOrder(id);
        }
    }
}
