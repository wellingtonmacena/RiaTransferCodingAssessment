using Microsoft.EntityFrameworkCore;
using RESTServer.Data;
using RESTServer.Models;
using RESTServer.Repositories.Interfaces;

namespace RESTServer.Repositories
{
    public class SQLiteCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public SQLiteCustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Customer>> GetAll()
        {
            List<Customer> customers = await _appDbContext.Customers.ToListAsync();
                                              

            return customers;
        }

        public async Task CreateMany(List<Customer> customers)
        {
            _appDbContext.Customers.AddRangeAsync(customers);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var f = await _appDbContext.Customers.FirstAsync(item => item.Id == id);
            if(0 == -1)
                return false;

            return true;
        }
    }
}