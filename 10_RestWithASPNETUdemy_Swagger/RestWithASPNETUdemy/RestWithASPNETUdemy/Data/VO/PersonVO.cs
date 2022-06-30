using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using RestWithASPNETUdemy.Model.Base;

namespace RestWithASPNETUdemy.Data.VO
{ 
    public class PersonVO : ISupportsHyperMedia
    {
        [JsonPropertyName("codigo")]
        public long Id { get; set; }

        [JsonPropertyName("nome")]
        public string? FirstName { get; set; }

        [JsonPropertyName("sobrenome")]
        public string? LastName { get; set; }

        [JsonIgnore]
        public string? Address { get; set; }

        [JsonPropertyName("sexo")]
        public string? Gender { get; set; }

        // -- Interface
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
