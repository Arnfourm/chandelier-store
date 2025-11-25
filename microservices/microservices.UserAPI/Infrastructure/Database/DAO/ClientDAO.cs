using System.Net;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Infrastructure.Database.Contexts;
using microservices.UserAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.UserAPI.Infrastructure.Database.DAO
{
    public class ClientDAO : IClientDAO
    {
        private readonly UserDbContext _userDbContext;

        public ClientDAO(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<List<Client>> GetClients()
        {
            return await _userDbContext.Clients
                .Select(clientEntity => new Client
                (
                    clientEntity.UserId,
                    clientEntity.DeliveryAddressCountry,
                    clientEntity.DeliveryAddressDistrict,
                    clientEntity.DeliveryAddressCity,
                    clientEntity.DeliveryAddressStreet,
                    clientEntity.DeliveryAddressHouse,
                    clientEntity.DeliveryAddressPostalIndex
                ))
                .ToListAsync();
        }

        public async Task<Client> GetClientByUserId(Guid userId)
        {
            var clientEntity = await _userDbContext.Clients.FindAsync(userId);

            if (clientEntity == null)
            {
                throw new Exception($"Client with id {userId} not found");
            }

            return new Client(
                clientEntity.UserId,
                clientEntity.DeliveryAddressCountry,
                clientEntity.DeliveryAddressDistrict,
                clientEntity.DeliveryAddressCity,
                clientEntity.DeliveryAddressStreet,
                clientEntity.DeliveryAddressHouse,
                clientEntity.DeliveryAddressPostalIndex
            );
        }

        public async Task<Guid> CreateClient(Client client)
        {
            var clientEntity = new ClientEntity
            {
                UserId = client.GetUserId(),
                DeliveryAddressCountry = client.GetDeliveryAddressCountry(),
                DeliveryAddressDistrict = client.GetDeliveryAddressDistrict(),
                DeliveryAddressCity = client.GetDeliveryAddressCity(),
                DeliveryAddressStreet = client.GetDeliveryAddressStreet(),
                DeliveryAddressHouse = client.GetDeliveryAddressHouse(),
                DeliveryAddressPostalIndex = client.GetDeliveryAddressPostalIndex(),
            };

            await _userDbContext.Clients.AddAsync(clientEntity);

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new client {ex.Message}", ex);
            }

            return clientEntity.UserId;
        }

        public async Task UpdateClient(Client client)
        {
            await _userDbContext.Clients
                .Where(clientEntity => clientEntity.UserId == client.GetUserId())
                .ExecuteUpdateAsync(clientSetters => clientSetters
                    .SetProperty(clientSetters => clientSetters.DeliveryAddressCountry, client.GetDeliveryAddressCountry())
                    .SetProperty(clientSetters => clientSetters.DeliveryAddressDistrict, client.GetDeliveryAddressDistrict())
                    .SetProperty(clientSetters => clientSetters.DeliveryAddressCity, client.GetDeliveryAddressCity())
                    .SetProperty(clientSetters => clientSetters.DeliveryAddressStreet, client.GetDeliveryAddressStreet())
                    .SetProperty(clientSetters => clientSetters.DeliveryAddressHouse, client.GetDeliveryAddressHouse())
                    .SetProperty(clientSetters => clientSetters.DeliveryAddressPostalIndex, client.GetDeliveryAddressPostalIndex())
                );

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update client's info. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteClient(Guid userId)
        {
            await _userDbContext.Clients
                .Where(clientEntity => clientEntity.UserId == userId)
                .ExecuteDeleteAsync();

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete client. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
