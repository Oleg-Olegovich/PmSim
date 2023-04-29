using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Credentials;

public class SignUpRequest : SignInRequest
{
    [Required]
    [MaxLength(100)]
    [JsonPropertyName("username")]
    public string Email { get; set; }
}