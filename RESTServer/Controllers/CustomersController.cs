using Microsoft.AspNetCore.Mvc;
using RESTServer.Models;
using RESTServer.Repositories.Interfaces;

namespace RESTServer.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomersController : Controller
    {
        List<>
        private ICustomerRepository _iCustomerRepository { get; set; }
        public CustomersController(ICustomerRepository iCustomerRepository)
        {
            _iCustomerRepository = iCustomerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Customer> customers = await _iCustomerRepository.GetAll();

            if (customers == null)
                return NotFound();
            else
                return Ok(new { data = customers });
        }

        [HttpPost]
        public async Task<ActionResult> CreateMany([FromBody] List<Customer> customers)
        {
            List<Customer> customersAux = new List<Customer>();
            foreach (var customer in customers)
            {
                if (customer.AllFieldAreComplete() && !customer.IsUnderAge() && ! await _iCustomerRepository.Exists(customer.Id))
                    customersAux.Add(customer);
            }
            if (customersAux.Count > 0)
            {
                await _iCustomerRepository.CreateMany(customersAux);

                return Ok();
            }
            else
            {
                return StatusCode(406, "Any of the customers is valid");
            }
        }
    }
}
