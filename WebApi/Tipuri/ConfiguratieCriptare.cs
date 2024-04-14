using System.Security.Cryptography;

namespace WebApi.Tipuri;

public record ConfiguratieCriptare(
        HashAlgorithmName Algoritm,
        int NumarOctetiSare,
        int NumarOctetiCheie,
        int NumarOctetiHash,
        int NumarIteratii);
