using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IEmployeeDAO
    {
        public Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeByUserId(Guid userId);
        Task<Guid> CreateEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(Guid userId);
    }
}
