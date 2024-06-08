using System.Text.Json.Serialization;

namespace MobileApp.Models;

public record Istoric
{
    [JsonPropertyName("numeUtilizator")] public string NumeUtilizator { get; set; }
    [JsonPropertyName("denumireAliment")] public string DenumireAliment { get; set; }
    [JsonPropertyName("data")] public DateTime Data { get; set; }
    [JsonPropertyName("cantitateConsumata")] public float CantitateConsumata { get; set; }
    [JsonPropertyName("caloriiConsumate")] public int CaloriiConsumate { get; set; }
    [JsonPropertyName("grasimiConsumate")] public float GrasimiConsumate { get; set; }
    [JsonPropertyName("glucideConsumate")] public float GlucideConsumate { get; set; }
    [JsonPropertyName("proteineConsumate")] public float ProteineConsumate { get; set; }
}