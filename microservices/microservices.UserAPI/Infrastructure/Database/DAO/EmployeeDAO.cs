using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Infrastructure.Database.Contexts;
using microservices.UserAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.UserAPI.Infrastructure.Database.DAO
{
    public class EmployeeDAO : IEmployeeDAO
    {
        private readonly UserDbContext _userDbContext;

        public EmployeeDAO(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _userDbContext.Employees
                .Select(employeeEntity => new Employee
                (
                    employeeEntity.UserId,
                    employeeEntity.Code,
                    employeeEntity.ResidenceAddressCountry,
                    employeeEntity.ResidenceAddressDistrict,
                    employeeEntity.ResidenceAddressCity,
                    employeeEntity.ResidenceAddressStreet,
                    employeeEntity.ResidenceAddressHouse,
                    employeeEntity.ResidenceAddressPostalIndex
                ))
                .ToListAsync();
        }
        public async Task<Employee> GetEmployeeByUserId(Guid userId) 
        {
            var employeeEntity = await _userDbContext.Employees.FindAsync(userId);

            if (employeeEntity == null)
            {
                throw new Exception($"Employee with id {userId} not found");
            }

            return new Employee(
                employeeEntity.UserId,
                employeeEntity.Code,
                employeeEntity.ResidenceAddressCountry,
                employeeEntity.ResidenceAddressDistrict,
                employeeEntity.ResidenceAddressCity,
                employeeEntity.ResidenceAddressStreet,
                employeeEntity.ResidenceAddressHouse,
                employeeEntity.ResidenceAddressPostalIndex
            );
        }

        public async Task<Guid> CreateEmployee(Employee employee) 
        {
            var employeeEntity = new EmployeeEntity
            {
                UserId = employee.GetUserId(),
                Code = employee.GetCode(),
                ResidenceAddressCountry = employee.GetResidenceAddressCountry(),
                ResidenceAddressDistrict = employee.GetResidenceAddressDistrict(),
                ResidenceAddressCity = employee.GetResidenceAddressCity(),
                ResidenceAddressStreet = employee.GetResidenceAddressStreet(),
                ResidenceAddressHouse = employee.GetResidenceAddressHouse(),
                ResidenceAddressPostalIndex = employee.GetResidenceAddressPostalCode()
            };

            await _userDbContext.Employees.AddAsync(employeeEntity);

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new employee {ex.Message}", ex);
            }

            return employeeEntity.UserId;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _userDbContext.Employees
                .Where(employeeEntity => employeeEntity.UserId == employee.GetUserId())
                .ExecuteUpdateAsync(employeeSetters => employeeSetters
                    .SetProperty(e => e.Code, employee.GetCode())
                    .SetProperty(e => e.ResidenceAddressCountry, employee.GetResidenceAddressCountry())
                    .SetProperty(e => e.ResidenceAddressDistrict, employee.GetResidenceAddressDistrict())
                    .SetProperty(e => e.ResidenceAddressCity, employee.GetResidenceAddressCity())
                    .SetProperty(e => e.ResidenceAddressStreet, employee.GetResidenceAddressStreet())
                    .SetProperty(e => e.ResidenceAddressHouse, employee.GetResidenceAddressHouse())
                    .SetProperty(e => e.ResidenceAddressPostalIndex, employee.GetResidenceAddressPostalCode())
                );

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update employee's info. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteEmployee(Guid userId)
        {
            await _userDbContext.Employees
                .Where(employeeEntity => employeeEntity.UserId == userId)
                .ExecuteDeleteAsync();

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete employee. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
