using System.Security.Cryptography;
using WebApi.Tipuri;

namespace WebApi.Utilitati;

public static class EncriptorParola
{
    public static string CripteazaParola(string parolaIntrodusa)
    {
        var sare = new byte[Configuratie.NumarOctetiSare];
        RandomNumberGenerator.Create().GetBytes(sare);

        var pbkdf2 = new Rfc2898DeriveBytes(parolaIntrodusa, sare, Configuratie.NumarIteratii, Configuratie.Algoritm);
        var cheie = pbkdf2.GetBytes(Configuratie.NumarOctetiCheie);

        var hash = new byte[Configuratie.NumarOctetiHash];
        Array.Copy(sare, 0, hash, 0, Configuratie.NumarOctetiSare);
        Array.Copy(cheie, 0, hash, 16, Configuratie.NumarOctetiCheie);

        var parolaCriptata = Convert.ToBase64String(hash);
        return parolaCriptata;
    }

    public static bool VerificaParola(string parolaIntrodusa, string parolaCriptata)
    {
        var hash = Convert.FromBase64String(parolaCriptata);
        var sare = new byte[Configuratie.NumarOctetiSare];
        Array.Copy(hash, 0, sare, 0, Configuratie.NumarOctetiSare);

        var pbkdf2 = new Rfc2898DeriveBytes(parolaIntrodusa, sare, Configuratie.NumarIteratii, Configuratie.Algoritm);
        var cheie = pbkdf2.GetBytes(Configuratie.NumarOctetiCheie);

        for (int i = 0; i < Configuratie.NumarOctetiCheie; i++)
        {
            if (!hash.ElementAt(i + Configuratie.NumarOctetiSare).Equals(cheie.ElementAt(i)))
                return false;
        }

        return true;
    }

    private static ConfiguratieCriptare Configuratie { get; } = new ConfiguratieCriptare(
        Algoritm: HashAlgorithmName.SHA256,
        NumarOctetiSare: 16,
        NumarOctetiCheie: 20,
        NumarOctetiHash: 36,     // SHA256 genereaza hash pe 36 octeti
        NumarIteratii: 1000);
}
