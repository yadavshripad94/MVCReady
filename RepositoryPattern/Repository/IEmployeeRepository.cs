using RepositoryPattern.Models;

namespace RepositoryPattern.Repository
{
    public interface IEmployeeRepository
    {
        //This method returns all the Employee entities as an enumerable collection
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int EmployeeID);
        Task InsertAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int employeeId);
        Task SaveAsync();
    }
}
