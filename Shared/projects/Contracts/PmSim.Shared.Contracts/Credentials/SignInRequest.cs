using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Credentials
{
    public class SignInRequest
    {
        [Required]
        [MaxLength(100)]
        [JsonPropertyName("username")]
        public string Login { get; set; }
        
        [Required]
        [MaxLength(100)]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}