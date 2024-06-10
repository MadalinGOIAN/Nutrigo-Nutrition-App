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

    private void IntoarceLaPaginaAnterioara()
    {
        switch (PaginaAnterioara)
        {
            case nameof(PaginaAdaugareAliment):
                Application.Current.MainPage = new PaginaAdaugareAliment(NumeUtilizator);
                break;

            case nameof(PaginaAlimentNou):
                //TODO: Don't forget to change this after MVVM adaptation
                // Application.Current.MainPage = new PaginaAlimentNou();
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

            case nameof(PaginaAlimentNou):
                Application.Current.MainPage = new PaginaAdaugareAliment();
                await Toast.Make("Aliment creat cu succes", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
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

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaPaginaAnterioara { get; private set; }
    public Action AfiseazaMesajAlimentNegasit { get; set; }
    private string PaginaAnterioara { get; init; }
    private string NumeUtilizator { get; init; }
    private Aliment AlimentGasit { get; set; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
}
