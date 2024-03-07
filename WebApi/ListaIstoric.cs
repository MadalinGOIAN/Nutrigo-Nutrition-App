namespace WebApi;

public static class ListaIstoric
{
    public static List<Istoric> Istoric = new()
    {
        new Istoric()
        {
            NumeUtilizator = "utilizatorTest_1",
            DenumireAliment = "alimentTest1",
            Data = DateOnly.FromDateTime(DateTime.Today),
            CantitateConsumata = 100f,
            CaloriiConsumate = 250,
            GrasimiConsumate = 13f,
            GlucideConsumate = 11.5f,
            ProteineConsumate = 4.3f
        },
        new Istoric()
        {
            NumeUtilizator = "utilizatorTest_2",
            DenumireAliment = "alimentTest5",
            Data = DateOnly.FromDateTime(DateTime.Today),
            CantitateConsumata = 75.5f,
            CaloriiConsumate = 140,
            GrasimiConsumate = 13f,
            GlucideConsumate = 11.5f,
            ProteineConsumate = 4.3f
        },
        new Istoric()
        {
            NumeUtilizator = "utilizatorTest_2",
            DenumireAliment = "alimentTest6",
            Data = new DateOnly(2024,2,22),
            CantitateConsumata = 175.5f,
            CaloriiConsumate = 147,
            GrasimiConsumate = 23f,
            GlucideConsumate = 13.5f,
            ProteineConsumate = 7f
        },
        new Istoric()
        {
            NumeUtilizator = "utilizatorTest_1",
            DenumireAliment = "alimentTest6",
            Data = DateOnly.FromDateTime(DateTime.Today),
            CantitateConsumata = 175.5f,
            CaloriiConsumate = 147,
            GrasimiConsumate = 23f,
            GlucideConsumate = 13.5f,
            ProteineConsumate = 7f
        },
    };
}
