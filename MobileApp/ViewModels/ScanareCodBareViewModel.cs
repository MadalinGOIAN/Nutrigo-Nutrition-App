using CommunityToolkit.Maui.Alerts;
using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class ScanareCodBareViewModel : INotifyPropertyChanged
{
    public ScanareCodBareViewModel(string paginaAnterioara, string numeUtilizator)
    {
        PaginaAnterioara = paginaAnterioara;
        NumeUtilizator = numeUtilizator;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaIntoarcereLaPaginaAnterioara = new Command(IntoarceLaPaginaAnterioara);
    }
    
    public ScanareCodBareViewModel(
        string paginaAnterioara,
        string numeUtilizator,
        string denumireAliment,
        string caloriiAliment,
        string grasimiAliment,
        string glucideAliment,
        string proteineAliment)
    {
        PaginaAnterioara = paginaAnterioara;
        NumeUtilizator = numeUtilizator;
        DenumireAliment = denumireAliment;
        CaloriiAliment = caloriiAliment;
        GrasimiAliment = grasimiAliment;
        GlucideAliment = glucideAliment;
        ProteineAliment = proteineAliment;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaIntoarcereLaPaginaAnterioara = new Command(IntoarceLaPaginaAnterioara);
    }

    private void IntoarceLaPaginaAnterioara()
    {
        switch (PaginaAnterioara)
        {
            case nameof(PaginaAdaugareAliment):
                Application.Current.MainPage = new PaginaAdaugareAliment(NumeUtilizator);
                break;

            case nameof(PaginaValidareValori):
                Application.Current.MainPage = new PaginaValidareValori(
                    NumeUtilizator, DenumireAliment, CaloriiAliment, GrasimiAliment, GlucideAliment, ProteineAliment);
                break;
        }
    }

    public async void ExtrageCodBare(string codBare)
    {
        switch (PaginaAnterioara)
        {
            case nameof(PaginaAdaugareAliment):
                CautaAliment(codBare);
                break;

            case nameof(PaginaValidareValori):
                InregireazaAliment(
                    DenumireAliment, codBare, CaloriiAliment, GrasimiAliment, GlucideAliment, ProteineAliment);
                break;
        }
    }

    private async void CautaAliment(string codBare)
    {
        await ConexiuneHttps.TrimiteCerereHttpGetAsincron($"api/alimente/codBare/{codBare}");

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            AlimentGasit = 
                JsonSerializer.Deserialize<Aliment>(ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result);

            Application.Current.MainPage = new PaginaAdaugareAliment(
                numeUtilizator: NumeUtilizator, 
                alimentSelectat: AlimentGasit,
                sectiuneAlimentSelectatDeschisa: true);
        }
        else
        {
            AfiseazaMesajAlimentNegasit();
        }
    }

    private async void InregireazaAliment(
        string denumireAliment,
        string codBare,
        string caloriiAliment,
        string grasimiAliment,
        string glucideAliment,
        string proteineAliment)
    {
        var aliment = new Aliment()
        {
            Denumire = denumireAliment,
            CodBare = codBare,
            Calorii = int.Parse(caloriiAliment),
            Grasimi = float.Parse(grasimiAliment),
            Glucide = float.Parse(glucideAliment),
            Proteine = float.Parse(proteineAliment)
        };

        await ConexiuneHttps.TrimiteCerereHttpPostAsincron(
            uriCerere: "api/alimente",
            valori: aliment,
            esteConectareUtilizator: false);

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            Application.Current.MainPage = new PaginaAdaugareAliment(NumeUtilizator);
            await Toast.Make("Aliment înregistrat cu succes", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
        else 
        {
            AfiseazaMesajEroareAdaugareAlimentNou();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaPaginaAnterioara { get; private set; }
    public Action AfiseazaMesajAlimentNegasit { get; set; }
    public Action AfiseazaMesajEroareAdaugareAlimentNou { get; set; }
    private string PaginaAnterioara { get; init; }
    private string NumeUtilizator { get; init; }
    private string DenumireAliment { get; init; }
    private string CaloriiAliment { get; init; }
    private string GrasimiAliment { get; init; }
    private string GlucideAliment { get; init; }
    private string ProteineAliment { get; init; }
    private Aliment AlimentGasit { get; set; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
}
