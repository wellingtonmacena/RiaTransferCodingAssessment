using Newtonsoft.Json;

namespace RESTServer.Models
{
    public sealed class CustomerArraySingleton
    {
        private static CustomerArraySingleton instance = null;
        private static readonly object lockObject = new object();

        private Customer[] customers;

        private string filePath = "customers.json";

        private CustomerArraySingleton()
        {
            LoadData();
            Console.WriteLine("init");
        }

        public static CustomerArraySingleton Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new CustomerArraySingleton();
                    }
                    return instance;
                }
            }
        }


        public Customer[] Customers
        {
            get { return customers; }
            set
            {
                customers = value;
            }
        }

        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                customers = JsonConvert.DeserializeObject<Customer[]>(jsonData);
            }
            else
            {
                customers = new Customer[0];
            }
        }

        public void SaveData()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(customers, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine($"Array written to {filePath}.");

            }
            catch (Exception)
            {

            }
        }
    }
}