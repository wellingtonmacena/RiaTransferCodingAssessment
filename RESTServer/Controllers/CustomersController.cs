using Microsoft.AspNetCore.Mvc;
using RESTServer.Models;

namespace RESTServer.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomersController : Controller
    {
        private CustomerArraySingleton singleton = CustomerArraySingleton.Instance;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (singleton.Customers.Length == 0)
                return NoContent();
            else
                return Ok(singleton.Customers);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMany([FromBody] List<Customer> postCustomers)
        {
            foreach (var customer in postCustomers)
            {
                if (customer.AreAllFieldSupplied() && !customer.IsUnderAge())
                {
                    int id = customer.Id;
                    if (singleton.Customers.ToList().Exists(item => item.Id.Equals(customer.Id)))
                        id = singleton.Customers.ToList().Max(item => item.Id) + 1;

                    customer.Id = id;
                    singleton.Customers = InsertNewItem(customer);
                }
            }

            singleton.SaveData();
            return Ok();
        }

        private Customer[] InsertNewItem(Customer newItem)
        {
            Customer[] customers = singleton.Customers;
            // Find the index to insert item
            int insertIndex = Array.BinarySearch(customers, newItem);

            // If BinarySearch returns a negative value, convertit to the index where the item should be inserted
            if (insertIndex < 0)
            {
                insertIndex = ~insertIndex;
            }

            // Resize the array
            Array.Resize(ref customers, customers.Length + 1);

            // Shift elements to make space for the new item
            for (int i = customers.Length - 1; i > insertIndex; i--)
            {
                customers[i] = customers[i - 1];
            }

            // Insert the new item at the correct position
            customers[insertIndex] = newItem;

            return customers;
        }
    }
}
