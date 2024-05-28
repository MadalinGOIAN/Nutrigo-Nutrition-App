using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class IstoricViewModel : INotifyPropertyChanged
{
    public IstoricViewModel(string numeUtilizator)
    {
        NumeUtilizator = numeUtilizator;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        IstoricOrdonatDupaData = new();
        ComandaIntoarcereLaProfil = new Command(IntoarceLaProfil);
        ComandaObtinereIstoric = new Command(ObtineIstoricUtilizator);
        ComandaDataDreapta = new Command(AlegeDataDinainte);
        ComandaDataStanga = new Command(AlegeDataDinapoi);
    }

    private void IntoarceLaProfil()
    {
        Application.Current.MainPage = new PaginaProfil(NumeUtilizator);
    }

    private async void ObtineIstoricUtilizator()
    {
        await ConexiuneHttps.TrimiteCerereHttpGetAsincron($"api/istoric/{NumeUtilizator}");

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            TotIstoriculUtilizator =
                JsonSerializer.Deserialize<List<Istoric>>(ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result);

            foreach (Istoric inregistrare in TotIstoriculUtilizator)
            {
                if (IstoricOrdonatDupaData.ContainsKey(inregistrare.Data))
                    IstoricOrdonatDupaData[inregistrare.Data].Add(inregistrare);
                else
                    IstoricOrdonatDupaData.Add(inregistrare.Data, new List<Istoric>() { inregistrare });
            }

            DatiDisponibile = IstoricOrdonatDupaData.Keys.Reverse().ToArray();
            IndexData = 0;
            DataSelectata = DatiDisponibile.FirstOrDefault();

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(DataSelectata)));
            AfiseazaIstoricDinDataSelectata();
        }
        else
        {
            AfiseazaMesajObtinereIstoricNereusita();
        }
    }

    private void AlegeDataDinapoi()
    {
        try
        {
            IndexData++;
            DataSelectata = DatiDisponibile.ElementAt(IndexData);
        }
        catch(ArgumentOutOfRangeException e)
        {
            IndexData = 0;
            DataSelectata = DatiDisponibile.ElementAt(IndexData);
        }

        PropertyChanged(this, new PropertyChangedEventArgs(nameof(DataSelectata)));
        AfiseazaIstoricDinDataSelectata();
    }

    private void AlegeDataDinainte()
    {
        try
        {
            IndexData--;
            DataSelectata = DatiDisponibile.ElementAt(IndexData);
        }
        catch (ArgumentOutOfRangeException e)
        {
            IndexData = DatiDisponibile.Length - 1;
            DataSelectata = DatiDisponibile.ElementAt(IndexData);
        }

        PropertyChanged(this, new PropertyChangedEventArgs(nameof(DataSelectata)));
        AfiseazaIstoricDinDataSelectata();
    }

    private void AfiseazaIstoricDinDataSelectata()
    {
        if (IstoricOrdonatDupaData[DataSelectata].Count > 0)
        {
            ExistaIstoricZiCurenta = true;
            NuExistaInregistrari = !ExistaIstoricZiCurenta;

            switch (IstoricOrdonatDupaData[DataSelectata].Count)
            {
                case 1:
                    ExistaOInregistrare = true;
                    ExistaDouaInregistrari = false;
                    ExistaTreiInregistrari = false;
                    ExistaMaiMultDeTreiInregistrari = false;
                    Inregistrare1 = IstoricOrdonatDupaData[DataSelectata].ElementAt(0);
                    break;

                case 2:
                    ExistaOInregistrare = true;
                    ExistaDouaInregistrari = true;
                    ExistaTreiInregistrari = false;
                    ExistaMaiMultDeTreiInregistrari = false;
                    Inregistrare1 = IstoricOrdonatDupaData[DataSelectata].ElementAt(0);
                    Inregistrare2 = IstoricOrdonatDupaData[DataSelectata].ElementAt(1);
                    break;

                case 3:
                    ExistaOInregistrare = true;
                    ExistaDouaInregistrari = true;
                    ExistaTreiInregistrari = true;
                    ExistaMaiMultDeTreiInregistrari = false;
                    Inregistrare1 = IstoricOrdonatDupaData[DataSelectata].ElementAt(0);
                    Inregistrare2 = IstoricOrdonatDupaData[DataSelectata].ElementAt(1);
                    Inregistrare3 = IstoricOrdonatDupaData[DataSelectata].ElementAt(2);
                    break;

                default:
                    ExistaOInregistrare = true;
                    ExistaDouaInregistrari = true;
                    ExistaTreiInregistrari = true;
                    ExistaMaiMultDeTreiInregistrari = true;
                    Inregistrare1 = IstoricOrdonatDupaData[DataSelectata].ElementAt(0);
                    Inregistrare2 = IstoricOrdonatDupaData[DataSelectata].ElementAt(1);
                    Inregistrare3 = IstoricOrdonatDupaData[DataSelectata].ElementAt(2);
                    break;
            }
        }
        else
        {
            NuExistaInregistrari = !ExistaIstoricZiCurenta;
        }

        PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaIstoricZiCurenta)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(NuExistaInregistrari)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaOInregistrare)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaDouaInregistrari)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaTreiInregistrari)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaMaiMultDeTreiInregistrari)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare1)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare2)));
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare3)));
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaProfil { get; private set; }
    public ICommand ComandaObtinereIstoric { get; private set; }
    public ICommand ComandaDataDreapta { get; private set; }
    public ICommand ComandaDataStanga { get; private set; }
    public Action AfiseazaMesajObtinereIstoricNereusita { get; set; }
    public DateTime DataSelectata { get; private set; }
    public Istoric Inregistrare1 { get; set; }
    public Istoric Inregistrare2 { get; set; }
    public Istoric Inregistrare3 { get; set; }
    public bool ExistaIstoricZiCurenta { get; set; }
    public bool ExistaOInregistrare { get; set; }
    public bool ExistaDouaInregistrari { get; set; }
    public bool ExistaTreiInregistrari { get; set; }
    public bool ExistaMaiMultDeTreiInregistrari { get; set; }
    public bool NuExistaInregistrari { get; set; }
    private List<Istoric> TotIstoriculUtilizator { get; set; }
    private Dictionary<DateTime, List<Istoric>> IstoricOrdonatDupaData { get; set; }
    private DateTime[] DatiDisponibile { get; set; }
    private int IndexData { get; set; }
    private string NumeUtilizator { get; init; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
}
