using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class ProfilViewModel :INotifyPropertyChanged
{
    public ProfilViewModel(string numeUtilizator)
    {
        NumeUtilizator = numeUtilizator;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaObtinereInfoUtilizator = new Command(ObtineInfoUtilizator);
        ComandaIntoarcereLaPaginaPrincipala = new Command(IntoarceLaPaginaPrincipala);
    }

    private async void ObtineInfoUtilizator()
    {
        await ConexiuneHttps.TrimiteCerereHttpGetAsincron($"api/utilizatori/{NumeUtilizator}");

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            Utilizator = 
                JsonSerializer.Deserialize<Utilizator>(ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result);

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Utilizator)));
        }
        else
        {
            AfiseazaMesajObtinereInfoNereusita();
        }
    }

    private void IntoarceLaPaginaPrincipala()
    {
        Application.Current.MainPage = new PaginaPrincipala(NumeUtilizator);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaObtinereInfoUtilizator { get; private set; }
    public ICommand ComandaIntoarcereLaPaginaPrincipala { get; private set; }
    public Action AfiseazaMesajObtinereInfoNereusita { get; set; }
    public Utilizator Utilizator { get; set; }
    private string NumeUtilizator { get; init; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
}
