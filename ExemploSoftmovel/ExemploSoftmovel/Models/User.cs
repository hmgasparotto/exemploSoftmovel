using Newtonsoft.Json;
using SQLite;

namespace ExemploSoftmovel.Models
{
    [Table ("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [MaxLength(8)]
        public string Password { get; set; }
    }
}
