using Newtonsoft.Json;
using System.Text;

namespace RESTServerSimulator.Models
{
    public class Customer : IComparable<Customer>
    {
        public Customer(string firstName, string lastName, int age)
        {
            Id = 1;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        public override string? ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{{Id: {Id},  ");
            stringBuilder.AppendLine($"FirstName: {FirstName},  ");
            stringBuilder.AppendLine($"LastName: {LastName},  ");
            stringBuilder.AppendLine($"Age: {Age},  ");

            return stringBuilder.ToString();
        }

        public bool AreAllFieldSupplied()
        {
            if (Id == null || Age == null || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                return false;

            return true;
        }

        public bool IsUnderAge()
        {
            return Age < 18;
        }

        public int CompareTo(Customer? other)
        {
            int lastNameComparison = string.Compare(this.LastName, other.LastName, StringComparison.OrdinalIgnoreCase);

            if (lastNameComparison == 0) // If last names are equal, compare by first names
            {
                return string.Compare(this.FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase);
            }

            return lastNameComparison;
        }
    }
}