using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DTO.Neo4j
{
    public class UserDTOn
    {
        [JsonProperty(PropertyName = "id")]
        public int userId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string firstName { get; set; }
        [JsonProperty(PropertyName = "surname")]
        public string lastName { get; set; }
        [JsonProperty(PropertyName = "login")]
        public string login { get; set; }
    }
}
