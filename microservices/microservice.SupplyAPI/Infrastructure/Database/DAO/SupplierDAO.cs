using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Models;
using microservice.SupplyAPI.Infrastructure.Database.Contexts;
using microservice.SupplyAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservice.SupplyAPI.Infrastructure.Database.DAO
{
    public class SupplierDAO : ISupplierDAO
    {
        private readonly SupplyDbContext _supplyDbContext;

        public SupplierDAO(SupplyDbContext supplyDbContext)
        {
            _supplyDbContext = supplyDbContext;
        }

        public async Task<List<Supplier>> GetSuppliers()
        {
            return await _supplyDbContext.Suppliers
                .Select(supplierEntity => new Supplier
                (
                    supplierEntity.Id,
                    supplierEntity.Name,
                    supplierEntity.DeliveryTypeId
                )).ToListAsync();
        }

        public async Task<Supplier> GetSupplierById(Guid id)
        {
            var supplierEntity = await _supplyDbContext.Suppliers.FindAsync(id);

            if (supplierEntity == null)
            {
                throw new Exception($"Supplier with id {id} not found");
            }

            return new Supplier
                (
                    supplierEntity.Id,
                    supplierEntity.Name,
                    supplierEntity.DeliveryTypeId
                );
        }

        public async Task<List<Supplier>> GetSupplierByIds(List<Guid> ids)
        {
            return await _supplyDbContext.Suppliers
                .Where(supplierEntity => ids.Contains(supplierEntity.Id))
                .Select(supplierEntity => new Supplier
                (
                    supplierEntity.Id,
                    supplierEntity.Name,
                    supplierEntity.DeliveryTypeId
                )).ToListAsync();
        }

        public async Task CreateSupplier(Supplier supplier)
        {
            SupplierEntity supplierEntity = new SupplierEntity 
            { 
                Name = supplier.GetName(), 
                DeliveryTypeId = supplier.GetDeliveryTypeId() 
            };

            await _supplyDbContext.Suppliers.AddAsync(supplierEntity);
            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new supplier. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            await _supplyDbContext.Suppliers
                .Where(supplierEntity => supplierEntity.Id == supplier.GetId())
                .ExecuteUpdateAsync(supplierSetters => supplierSetters
                    .SetProperty(supplierEntity => supplierEntity.Name, supplier.GetName())
                    .SetProperty(supplierEntity => supplierEntity.DeliveryTypeId, supplier.GetDeliveryTypeId()));

            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update supplier. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteSupplierById(Guid id)
        {
            await _supplyDbContext.Suppliers.Where(supplierEntity => supplierEntity.Id == id).ExecuteDeleteAsync();

            try
            {
                await _supplyDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete supplier. Error message:\n{ex.Message}", ex);
            }
        }
    }
}