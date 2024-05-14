using System.Text.Json.Serialization;

namespace MobileApp.Models;

public record Utilizator
{
    [JsonPropertyName("numeUtilizator")] public string NumeUtilizator { get; set; }
    [JsonPropertyName("parola")] public string Parola { get; set; }
    [JsonPropertyName("prenume")] public string Prenume { get; set; }
    [JsonPropertyName("numeFamilie")] public string NumeFamilie { get; set; }
    [JsonPropertyName("sex")] public string Sex { get; set; }
    [JsonPropertyName("varsta")] public uint Varsta { get; set; }
    [JsonPropertyName("inaltime")] public uint Inaltime { get; set; }
    [JsonPropertyName("greutate")] public uint Greutate { get; set; }
    [JsonPropertyName("nivelActivitateFizica")] public NivelActivitateFizica NivelActivitateFizica { get; set; }
    [JsonPropertyName("necesarCaloric")] public uint NecesarCaloric { get; set; }
}
