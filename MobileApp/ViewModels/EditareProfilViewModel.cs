using CommunityToolkit.Maui.Alerts;
using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;


namespace MobileApp.ViewModels;

public class EditareProfilViewModel : INotifyPropertyChanged
{
    public EditareProfilViewModel(Utilizator utilizatorVechi)
    {
        UtilizatorVechi = utilizatorVechi;
        Prenume = utilizatorVechi.Prenume;
        NumeFamilie = utilizatorVechi.NumeFamilie;
        Varsta = utilizatorVechi.Varsta;
        Inaltime = utilizatorVechi.Inaltime;
        Greutate = utilizatorVechi.Greutate;
        NivelActivitateFizica = Convert.ToUInt32(utilizatorVechi.NivelActivitateFizica);

        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaActualizareUtilizator = new Command(ActualizeazaUtilizator);
        ComandaIntoarcereLaPaginaProfil = new Command(IntoarceLaPaginaProfil);
    }

    private async void ActualizeazaUtilizator()
    {
        var utilizatorActualizat = new Utilizator()
        {
            NumeUtilizator = UtilizatorVechi.NumeUtilizator,
            Parola = UtilizatorVechi.Parola,
            Prenume = Prenume,
            NumeFamilie = NumeFamilie,
            Sex = UtilizatorVechi.Sex,
            Varsta = Varsta,
            Inaltime = Inaltime,
            Greutate = Greutate,
            NivelActivitateFizica = (NivelActivitateFizica)NivelActivitateFizica,
            NecesarCaloric = RecalculeazaNecesarCaloric()
        };

        await ConexiuneHttps.TrimiteCerereHttpPutAsincron(
            uriCerere: $"api/utilizatori/{UtilizatorVechi.NumeUtilizator}",
            valori: utilizatorActualizat);

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            Application.Current.MainPage = new PaginaProfil(UtilizatorVechi.NumeUtilizator);
            await Toast.Make("Modificări salvate cu succes", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }
        else
        {
            AfiseazaMesajActualizareNereusita();
        }
    }

    private uint RecalculeazaNecesarCaloric()
    {
        float rmb = RecalculeazaRataMetabolicaBazala();
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

    private float RecalculeazaRataMetabolicaBazala()
    {
        if (UtilizatorVechi.Sex.Equals("M"))
            return 66.5f + (13.8f * Greutate) + (5f * Inaltime) - (6.8f * Varsta);

        return 655.1f + (9.6f * Greutate) + (1.9f * Inaltime) - (4.7f * Varsta);
    }

    private void IntoarceLaPaginaProfil()
    {
        Application.Current.MainPage = new PaginaProfil(UtilizatorVechi.NumeUtilizator);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaActualizareUtilizator { get; private set; }
    public ICommand ComandaIntoarcereLaPaginaProfil { get; private set; }
    public Action AfiseazaMesajActualizareNereusita { get; set; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
    public Utilizator UtilizatorVechi
    {
        get => _utilizatorVechi;
        set
        {
            _utilizatorVechi = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Utilizator)));
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

    private Utilizator _utilizatorVechi;
    private string _prenume;
    private string _numeFamilie;
    private uint _varsta;
    private uint _inaltime;
    private uint _greutate;
    private uint _nivelActivitateFizica;
}
