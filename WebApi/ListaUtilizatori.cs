namespace WebApi;

public static class ListaUtilizatori
{
    public static List<Utilizator> Utilizatori = new()
    {
        new Utilizator()
        {
            NumeUtilizator = "utilizatorTest_1",
            HashParola = "parolaTest_1",
            Prenume = "Test_1",
            NumeFamilie = "Test_1",
            Sex = 'm',
            Varsta = 20,
            Inaltime = 170,
            Greutate = 80,
            NivelActivitateFizica = NivelActivitateFizica.Moderat,
            NecesarCaloric = 2908
        },
        new Utilizator()
        {
            NumeUtilizator = "utilizatorTest_2",
            HashParola = "parolaTest_2",
            Prenume = "Test_2",
            NumeFamilie = "Test_2",
            Sex = 'f',
            Varsta = 24,
            Inaltime = 164,
            Greutate = 66,
            NivelActivitateFizica = NivelActivitateFizica.Intens,
            NecesarCaloric = 2537
        },
        new Utilizator()
        {
            NumeUtilizator = "utilizatorTest_3",
            HashParola = "parolaTest_3",
            Prenume = "Test_3",
            NumeFamilie = "Test_3",
            Sex = 'm',
            Varsta = 19,
            Inaltime = 184,
            Greutate = 110,
            NivelActivitateFizica = NivelActivitateFizica.Sedentar,
            NecesarCaloric = 2836
        },
    };
}
