using CommunityToolkit.Maui.Alerts;
using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class AdaugareAlimentViewModel : INotifyPropertyChanged
{
    public AdaugareAlimentViewModel(string numeUtilizator, Aliment? alimentSelectat = null)
    {
        NumeUtilizator = numeUtilizator;
        AlimentSelectat = alimentSelectat;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaIntoarcereLaPaginaPrincipala = new Command(IntoarceLaPaginaPrincipala);
        ComandaCautareAliment = new Command(CautaAliment);
        ComandaAscundereRezultate = new Command(AscundeRezultate);
        ComandaAdaugareInregistrareInIstoric = new Command(AdaugaInregistrareInIstoric);
        ComandaScanareCodBare = new Command(MergiLaScanareCodBare);
        ComandaAlimentNou = new Command(MergiLaAlimentNou);
    }

    private void IntoarceLaPaginaPrincipala()
    {
        Application.Current.MainPage = new PaginaPrincipala(NumeUtilizator);
    }

    private async void CautaAliment()
    {
        await ConexiuneHttps.TrimiteCerereHttpGetAsincron($"api/alimente/denumire/{DenumireAliment}");

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            AlimenteGasite =
                JsonSerializer.Deserialize<List<Aliment>>(ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result);

            AfiseazaRezultate();
        }
        else if (ConexiuneHttps.Raspuns.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
        {
            AscundeRezultate();
            await Toast.Make("Alimentul căutat nu a fost găsit", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
        else
        {
            AfiseazaMesajObtinereAlimenteNereusita();
        }
    }

    private void AfiseazaRezultate()
    {
        switch (AlimenteGasite.Count)
        {
            case 1:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                ExistaAliment2 = false; ExistaAliment3 = false;
                ExistaAliment4 = false; ExistaAliment5 = false;
                ExistaAliment6 = false; ExistaAliment7 = false;
                ExistaAliment8 = false;
                break; 
            
            case 2:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                Aliment2 = AlimenteGasite.ElementAt(1); ExistaAliment2 = true;
                ExistaAliment3 = false; ExistaAliment4 = false; 
                ExistaAliment5 = false; ExistaAliment6 = false;
                ExistaAliment7 = false; ExistaAliment8 = false;
                break;

            case 3:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                Aliment2 = AlimenteGasite.ElementAt(1); ExistaAliment2 = true;
                Aliment3 = AlimenteGasite.ElementAt(2); ExistaAliment3 = true;
                ExistaAliment4 = false; ExistaAliment5 = false;
                ExistaAliment6 = false; ExistaAliment7 = false;
                ExistaAliment8 = false;
                break;

            case 4:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                Aliment2 = AlimenteGasite.ElementAt(1); ExistaAliment2 = true;
                Aliment3 = AlimenteGasite.ElementAt(2); ExistaAliment3 = true;
                Aliment4 = AlimenteGasite.ElementAt(3); ExistaAliment4 = true;
                ExistaAliment5 = false; ExistaAliment6 = false; 
                ExistaAliment7 = false; ExistaAliment8 = false;
                break;

            case 5:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                Aliment2 = AlimenteGasite.ElementAt(1); ExistaAliment2 = true;
                Aliment3 = AlimenteGasite.ElementAt(2); ExistaAliment3 = true;
                Aliment4 = AlimenteGasite.ElementAt(3); ExistaAliment4 = true;
                Aliment5 = AlimenteGasite.ElementAt(4); ExistaAliment5 = true;
                ExistaAliment6 = false; ExistaAliment7 = false; ExistaAliment8 = false;
                break;

            case 6:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                Aliment2 = AlimenteGasite.ElementAt(1); ExistaAliment2 = true;
                Aliment3 = AlimenteGasite.ElementAt(2); ExistaAliment3 = true;
                Aliment4 = AlimenteGasite.ElementAt(3); ExistaAliment4 = true;
                Aliment5 = AlimenteGasite.ElementAt(4); ExistaAliment5 = true;
                Aliment6 = AlimenteGasite.ElementAt(5); ExistaAliment6 = true;
                ExistaAliment7 = false; ExistaAliment8 = false;
                break;

            case 7:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                Aliment2 = AlimenteGasite.ElementAt(1); ExistaAliment2 = true;
                Aliment3 = AlimenteGasite.ElementAt(2); ExistaAliment3 = true;
                Aliment4 = AlimenteGasite.ElementAt(3); ExistaAliment4 = true;
                Aliment5 = AlimenteGasite.ElementAt(4); ExistaAliment5 = true;
                Aliment6 = AlimenteGasite.ElementAt(5); ExistaAliment6 = true;
                Aliment7 = AlimenteGasite.ElementAt(6); ExistaAliment7 = true;
                ExistaAliment8 = false;
                break;

            case 8:
                Aliment1 = AlimenteGasite.ElementAt(0); ExistaAliment1 = true;
                Aliment2 = AlimenteGasite.ElementAt(1); ExistaAliment2 = true;
                Aliment3 = AlimenteGasite.ElementAt(2); ExistaAliment3 = true;
                Aliment4 = AlimenteGasite.ElementAt(3); ExistaAliment4 = true;
                Aliment5 = AlimenteGasite.ElementAt(4); ExistaAliment5 = true;
                Aliment6 = AlimenteGasite.ElementAt(5); ExistaAliment6 = true;
                Aliment7 = AlimenteGasite.ElementAt(6); ExistaAliment7 = true;
                Aliment8 = AlimenteGasite.ElementAt(7); ExistaAliment8 = true;
                break;
        }
    }

    private void AscundeRezultate()
    {
        ExistaAliment1 = false; ExistaAliment2 = false;
        ExistaAliment3 = false; ExistaAliment4 = false;
        ExistaAliment5 = false; ExistaAliment6 = false;
        ExistaAliment7 = false; ExistaAliment8 = false;
    }

    public void CompleteazaCampAlimentSelectat(string denumireAliment)
    {
        AlimentSelectat = AlimenteGasite.Find(a => a.Denumire.Equals(denumireAliment));
    }

    private async void AdaugaInregistrareInIstoric()
    {
        Istoric inregistrareNoua = new Istoric()
        {
            NumeUtilizator = NumeUtilizator,
            DenumireAliment = AlimentSelectat.Denumire,
            Data = DateTime.Now,
            CantitateConsumata = float.Parse(Cantitate),
            CaloriiConsumate = Convert.ToInt32(CalculeazaValorileConsumate(AlimentSelectat.Calorii)),
            GrasimiConsumate = CalculeazaValorileConsumate(AlimentSelectat.Grasimi),
            GlucideConsumate = CalculeazaValorileConsumate(AlimentSelectat.Glucide),
            ProteineConsumate = CalculeazaValorileConsumate(AlimentSelectat.Proteine),
        };

        await ConexiuneHttps.TrimiteCerereHttpPostAsincron(
            uriCerere: "api/istoric",
            valori: inregistrareNoua,
            esteConectareUtilizator: false);

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            await Toast.Make("Alimentul a fost adăugat", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
        else
        {
            AfiseazaMesajAdaugareNereusita();
        }
    }

    private float CalculeazaValorileConsumate(float valori)
    {
        return valori * float.Parse(Cantitate) / 100f;
    }

    private void MergiLaScanareCodBare()
    {
        Application.Current.MainPage = new PaginaScanareCodBare(nameof(PaginaAdaugareAliment), NumeUtilizator);
    }
    
    private void MergiLaAlimentNou()
    {
        Application.Current.MainPage = new PaginaAlimentNou(NumeUtilizator);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaPaginaPrincipala { get; private set; }
    public ICommand ComandaCautareAliment { get; private set; }
    public ICommand ComandaAscundereRezultate { get; private set; }
    public ICommand ComandaAdaugareInregistrareInIstoric { get; private set; }
    public ICommand ComandaScanareCodBare { get; private set; }
    public ICommand ComandaAlimentNou { get; private set; }
    public Action AfiseazaMesajObtinereAlimenteNereusita { get; set; }
    public Action AfiseazaMesajAdaugareNereusita { get; set; }
    private List<Aliment> AlimenteGasite { get; set; }
    private string NumeUtilizator { get; init; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }

    #region Proprietati
    public string DenumireAliment
    {
        get => _denumireAliment;
        set
        {
            _denumireAliment = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(DenumireAliment)));
        }
    }
    public string Cantitate
    {
        get => _cantitate;
        set
        {
            _cantitate = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Cantitate)));
        }
    }
    public Aliment AlimentSelectat
    {
        get => _alimentSelectat;
        set
        {
            _alimentSelectat = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(AlimentSelectat)));
        }
    }
    public Aliment Aliment1
    {
        get => _aliment1;
        set
        {
            _aliment1 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment1)));
        }
    }
    public Aliment Aliment2
    {
        get => _aliment2;
        set
        {
            _aliment2 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment2)));
        }
    }
    public Aliment Aliment3
    {
        get => _aliment3;
        set
        {
            _aliment3 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment3)));
        }
    }
    public Aliment Aliment4
    {
        get => _aliment4;
        set
        {
            _aliment4 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment4)));
        }
    }
    public Aliment Aliment5
    {
        get => _aliment5;
        set
        {
            _aliment5 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment5)));
        }
    }
    public Aliment Aliment6
    {
        get => _aliment6;
        set
        {
            _aliment6 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment6)));
        }
    }
    public Aliment Aliment7
    {
        get => _aliment7;
        set
        {
            _aliment7 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment7)));
        }
    }
    public Aliment Aliment8
    {
        get => _aliment8;
        set
        {
            _aliment8 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Aliment8)));
        }
    }
    public bool ExistaAliment1
    {
        get => _existaAliment1;
        set
        {
            _existaAliment1 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment1)));
        }
    }
    public bool ExistaAliment2
    {
        get => _existaAliment2;
        set
        {
            _existaAliment2 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment2)));
        }
    }
    public bool ExistaAliment3
    {
        get => _existaAliment3;
        set
        {
            _existaAliment3 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment3)));
        }
    }
    public bool ExistaAliment4
    {
        get => _existaAliment4;
        set
        {
            _existaAliment4 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment4)));
        }
    }
    public bool ExistaAliment5
    {
        get => _existaAliment5;
        set
        {
            _existaAliment5 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment5)));
        }
    }
    public bool ExistaAliment6
    {
        get => _existaAliment6;
        set
        {
            _existaAliment6 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment6)));
        }
    }
    public bool ExistaAliment7
    {
        get => _existaAliment7;
        set
        {
            _existaAliment7 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment7)));
        }
    }
    public bool ExistaAliment8
    {
        get => _existaAliment8;
        set
        {
            _existaAliment8 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaAliment8)));
        }
    }
    #endregion

    private string _denumireAliment;
    private string _cantitate;
    private Aliment _alimentSelectat;
    private Aliment _aliment1;
    private Aliment _aliment2;
    private Aliment _aliment3;
    private Aliment _aliment4;
    private Aliment _aliment5;
    private Aliment _aliment6;
    private Aliment _aliment7;
    private Aliment _aliment8;
    private bool _existaAliment1;
    private bool _existaAliment2;
    private bool _existaAliment3;
    private bool _existaAliment4;
    private bool _existaAliment5;
    private bool _existaAliment6;
    private bool _existaAliment7;
    private bool _existaAliment8;
}
