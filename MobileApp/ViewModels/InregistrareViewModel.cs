using CommunityToolkit.Maui.Alerts;
using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class InregistrareViewModel : INotifyPropertyChanged
{
    public InregistrareViewModel() 
    {
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaInregistrare = new Command(InregistreazaUtilizator);
    }

    private async void InregistreazaUtilizator()
    {
        var utilizator = new Utilizator()
        {
            NumeUtilizator = NumeUtilizator,
            Parola = Parola,
            Prenume = Prenume,
            NumeFamilie = NumeFamilie,
            Sex = Sex,
            Varsta = Varsta,
            Inaltime = Inaltime,
            Greutate = Greutate,
            NivelActivitateFizica = (NivelActivitateFizica)NivelActivitateFizica,
            NecesarCaloric = CalculeazaNecesarCaloric()
        };

        await ConexiuneHttps.TrimiteCerereHttpPostAsincron(
            uriCerere: "api/utilizatori",
            valori: utilizator,
            esteConectareUtilizator: false);

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            Application.Current.MainPage = new PaginaConectare();
            await Toast.Make("Cont creat cu succes", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
        else if (ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result
                .Equals("\"Acest nume de utilizator exista deja\""))
        {
            AfiseazaMesajUtilizatorExistent();
        }
        else
        {
            AfiseazaMesajInregistrareInvalida();
        }
    }

    private uint CalculeazaNecesarCaloric()
    {
        float rmb = CalculeazaRataMetabolicaBazala();
        float necesarCaloric = 0f;

        switch (NivelActivitateFizica)
        {
            case 0:
                necesarCaloric = rmb * 1.2f;
                break;

            case 1:
                necesarCaloric = rmb * 1.375f;
                break;

            case 2:
                necesarCaloric = rmb * 1.55f;
                break;

            case 3:
                necesarCaloric = rmb * 1.725f;
                break;
            
            case 4:
                necesarCaloric = rmb * 1.9f;
                break;
        }

        return Convert.ToUInt32(necesarCaloric);
    }

    private float CalculeazaRataMetabolicaBazala()
    {
        if (Sex.Equals("M"))
            return 66.5f + (13.8f * Greutate) + (5f * Inaltime) - (6.8f * Varsta);

        return 655.1f + (9.6f * Greutate) + (1.9f * Inaltime) - (4.7f * Varsta);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaInregistrare { get; private set; }
    public Action AfiseazaMesajUtilizatorExistent { get; set; }
    public Action AfiseazaMesajInregistrareInvalida { get; set; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
    public string NumeUtilizator
    {
        get => _numeUtilizator;
        set
        {
            _numeUtilizator = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumeUtilizator)));
        }
    }
    public string Parola
    {
        get => _parola;
        set
        {
            _parola = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Parola)));
        }
    }
    public string Prenume
    {
        get => _prenume;
        set
        {
            _prenume = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Prenume)));
        }
    }
    public string NumeFamilie
    {
        get => _numeFamilie;
        set
        {
            _numeFamilie = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(NumeFamilie)));
        }
    }
    public string Sex
    {
        get => _sex;
        set
        {
            _sex = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Sex)));
        }
    }
    public uint Varsta
    {
        get => _varsta;
        set
        {
            _varsta = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Varsta)));
        }
    }
    public uint Inaltime
    {
        get => _inaltime;
        set
        {
            _inaltime = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inaltime)));
        }
    }
    public uint Greutate
    {
        get => _greutate;
        set
        {
            _greutate = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Greutate)));
        }
    }
    public uint NivelActivitateFizica
    {
        get => _nivelActivitateFizica;
        set
        {
            _nivelActivitateFizica = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(NivelActivitateFizica)));
        }
    }

    private string _numeUtilizator;
    private string _parola;
    private string _prenume;
    private string _numeFamilie;
    private string _sex;
    private uint _varsta;
    private uint _inaltime;
    private uint _greutate;
    private uint _nivelActivitateFizica;
}
