using RESTServer.Models;

namespace RESTServer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateMany(List<Customer> customers);
        Task<bool> Exists(int id);
        Task<List<Customer>> GetAll();
    }
}
