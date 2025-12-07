using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusDAO _statusDAO;

        public StatusService(IStatusDAO statusDAO)
        {
            _statusDAO = statusDAO;
        }

        public async Task<IEnumerable<StatusResponse>> GetAllStatusResponses()
        {
            List<Status> statuses = await _statusDAO.GetStatuses();

            IEnumerable<StatusResponse> response = statuses
                .Select(status => new StatusResponse
                    (
                        status.GetId(),
                        status.GetTitle()
                    )
                );

            return response;
        }

        public async Task<Status> GetStatusById(int id)
        {
            Status status = await _statusDAO.GetStatusById(id);

            return status;
        }

        public async Task<IEnumerable<StatusResponse>> GetStatusResponsesByIds(List<int> ids)
        {
            List<Status> statuses = await _statusDAO.GetStatusByIds(ids);

            IEnumerable<StatusResponse> response = statuses
                .Select(status => new StatusResponse
                    (
                        status.GetId(),
                        status.GetTitle()
                    )
                );

            return response;
        }

        public async Task<StatusResponse> CreateNewStatus(StatusRequest request)
        {
            Status newStatus = new Status
            (
                request.Title
            );
            
            Status responseStatus = await _statusDAO.CreateStatus(newStatus);

            return new StatusResponse(responseStatus.GetId(), responseStatus.GetTitle());
        }

        public async Task UpdateStatusById(int id, StatusRequest request)
        {
            Status updateStatus = new Status
            (
                id,
                request.Title
            );

            await _statusDAO.UpdateStatus(updateStatus);
        }

        public async Task DeleteStatusById(int id)
        {
            await _statusDAO.DeleteStatus(id);
        }
    }
}