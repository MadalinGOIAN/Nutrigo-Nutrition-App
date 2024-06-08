using System.Text.Json.Serialization;

namespace MobileApp.Models;

public record Aliment
{
    [JsonPropertyName("denumire")] public string Denumire { get; set; }
    [JsonPropertyName("codBare")] public string? CodBare { get; set; }
    [JsonPropertyName("calorii")] public int Calorii { get; set; }
    [JsonPropertyName("grasimi")] public float Grasimi { get; set; }
    [JsonPropertyName("glucide")] public float Glucide { get; set; }
    [JsonPropertyName("proteine")] public float Proteine { get; set; }
}
