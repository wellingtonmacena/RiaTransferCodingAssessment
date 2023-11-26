using CosmoRequests.Models;
using RESTServerSimulator.Models;
using RestSharp;

namespace RESTServerSimulator
{
    internal class Program
    {
        static async Task Main()
        {
            const string baseUrl = "http://localhost:5090/api/v1/customers";
            List<string> firstNames =
                 new List<string>() { "Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" };
            List<string> lastNames =
                new List<string>() { "Liberty", "Ray", "Harrison", "Ronan", "Drew", "Powell", "Larsen", "Chan", "Anderson", "Lane" };
 
            var postTasks = new Task[5];
            for (int i = 0; i < postTasks.Length; i++)
            {
                List<Customer> customers = new List<Customer>();
                for (int index = 0; index < 3; index++)
                {
                    int randomNumberFirstNames = new Random().Next(firstNames.Count);
                    int randomNumberLastNames = new Random().Next(lastNames.Count);
                    int randomAge = new Random().Next(10, 90);
                    Customer customer = new Customer(firstNames[randomNumberFirstNames], lastNames[randomNumberLastNames], randomAge);
                    customers.Add(customer);
                }

                int customerId = i + 1;
                postTasks[i] = Task.Run(async () =>
                {
                    var client = new RestClient(baseUrl);
                    var request = new RestRequest();
                    request.AddBody(customers);
                    var response = client.ExecutePost(request);
                    Console.WriteLine($"POST Customer {customerId} - Status: {response.StatusCode}");
                });
            }

            await Task.WhenAll(postTasks);

            var getTasks = new Task[2];
            for (int i = 0; i < getTasks.Length; i++)
            {
                getTasks[i] = Task.Run(async () =>
                {
                    var client = new RestClient(baseUrl);
                    var request = new RestRequest();         
                    var response = client.ExecuteGet(request);

                    Console.WriteLine($"GET Customers - Status: {response.StatusCode}, Content: {response.Content}");
                });
            }
        
            await Task.WhenAll(getTasks);
        }
    }
}
