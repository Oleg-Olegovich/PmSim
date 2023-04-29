using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Credentials;

public class PaymentRequest : BasicRequest
{
    [JsonPropertyName("payment")]
    public int Payment { get; set; }
}