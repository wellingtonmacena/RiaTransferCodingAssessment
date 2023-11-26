using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RESTServer.Models
{
    public class Customer
    {
        [Key]
        [Column("id")]
        [DefaultValue(0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DefaultValue("firstName")]
        [Column("firstName")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [DefaultValue("lastName")]
        [Column("lastName")]
        [JsonProperty("lastName")]

        public string LastName { get; set; }
        [DefaultValue(19)]
        [Column("age")]
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

        public bool AllFieldAreComplete()
        {
            if (Id == null || Age == null || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                return false;

            return true;
        }

        public bool IsUnderAge()
        {
            return Age < 18;
        }
    }
}
