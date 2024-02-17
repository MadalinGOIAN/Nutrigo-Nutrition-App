namespace WebApi;

public static class ListaAlimente
{
    public static List<Aliment> Alimente = new()
    {
        new Aliment()
        {
            Denumire = "testProdus_1",
            CodBare = "codTest1",
            Calorii = 200,
            Grasimi = 1.3f,
            Glucide = 10f,
            Proteine = 1.1f
        },
        new Aliment()
        {
            Denumire = "testProdus_2",
            CodBare = "codTest_2",
            Calorii = 150,
            Grasimi = 4f,
            Glucide = 13.4f,
            Proteine = 5f
        },
        new Aliment()
        {
            Denumire = "testProdus_vrac_1",
            Calorii = 90,
            Grasimi = 1.3f,
            Glucide = 10f,
            Proteine = 1.1f
        },
        new Aliment()
        {
            Denumire = "testProdus_vrac_2",
            Calorii = 190,
            Grasimi = 3f,
            Glucide = 14.8f,
            Proteine = 3f
        },
        new Aliment()
        {
            Denumire = "testProdus_3",
            CodBare = "codTest_3",
            Calorii = 250,
            Grasimi = 14f,
            Glucide = 17.4f,
            Proteine = 5.8f
        }
    };
}
