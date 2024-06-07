using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class PrincipalaViewModel : INotifyPropertyChanged
{
    public PrincipalaViewModel(string numeUtilizator) 
    {
        NumeUtilizator = numeUtilizator;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaObtinereInfoUtilizator = new Command(ObtineInfoUtilizator);
        ComandaObtinereIstoricZiCurenta = new Command(ObtineIstoricZiCurenta);
        ComandaDeconectare = new Command(DeconecteazaUtilizator);
        ComandaProfil = new Command(MergiLaProfil);
        ComandaExtindereIstoric = new Command(ExtindeIstoric);
    }

    private async void ObtineInfoUtilizator()
    {
        await ConexiuneHttps.TrimiteCerereHttpGetAsincron($"api/utilizatori/{NumeUtilizator}");

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            var utilizator =
                JsonSerializer.Deserialize<Utilizator>(ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result);

            Macronutrienti = new Macronutrienti()
            {
                Calorii = utilizator.NecesarCaloric,
                Grasimi = Convert.ToUInt32(utilizator.NecesarCaloric * 0.3f / 9),
                Glucide = Convert.ToUInt32(utilizator.NecesarCaloric * 0.55f / 4),
                Proteine = Convert.ToUInt32(utilizator.NecesarCaloric * 0.15f / 4),
            };

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Macronutrienti)));
        }
        else
        {
            AfiseazaMesajObtinereInfoNereusita();
        }
    }

    private async void ObtineIstoricZiCurenta()
    {
        await ConexiuneHttps.TrimiteCerereHttpGetAsincron($"api/istoric/{NumeUtilizator}");

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            var istoric = 
                JsonSerializer.Deserialize<List<Istoric>>(ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result);
            IstoricZiCurenta = istoric.FindAll(i => i.Data.Equals(DateTime.Today));

            if (IstoricZiCurenta.Count > 0)
            {
                ExistaIstoricZiCurenta = true;
                NuExistaInregistrari = !ExistaIstoricZiCurenta;

                switch (IstoricZiCurenta.Count)
                {
                    case 1:
                        ExistaOInregistrare = true;
                        Inregistrare1 = IstoricZiCurenta.ElementAt(0);
                        break;
                    
                    case 2:
                        ExistaOInregistrare = true;
                        ExistaDouaInregistrari = true;
                        Inregistrare1 = IstoricZiCurenta.ElementAt(0);
                        Inregistrare2 = IstoricZiCurenta.ElementAt(1);
                        break;

                    case 3:
                        ExistaOInregistrare = true;
                        ExistaDouaInregistrari = true;
                        ExistaTreiInregistrari = true;
                        Inregistrare1 = IstoricZiCurenta.ElementAt(0);
                        Inregistrare2 = IstoricZiCurenta.ElementAt(1);
                        Inregistrare3 = IstoricZiCurenta.ElementAt(2);
                        break;

                    default:
                        ExistaOInregistrare = true;
                        ExistaDouaInregistrari = true;
                        ExistaTreiInregistrari = true;
                        ExistaMaiMultDeTreiInregistrari = true;
                        Inregistrare1 = IstoricZiCurenta.ElementAt(0);
                        Inregistrare2 = IstoricZiCurenta.ElementAt(1);
                        Inregistrare3 = IstoricZiCurenta.ElementAt(2);
                        break;
                }
            }
            else
            {
                NuExistaInregistrari = !ExistaIstoricZiCurenta;
            }

            SumarZi = new SumarZi()
            {
                CaloriiTotale = IstoricZiCurenta.Sum(i => i.CaloriiConsumate),
                GrasimiTotale = IstoricZiCurenta.Sum(i => i.GrasimiConsumate),
                GlucideTotale = IstoricZiCurenta.Sum(i => i.GlucideConsumate),
                ProteineTotale = IstoricZiCurenta.Sum(i => i.ProteineConsumate)
            };

            Progres = new Progres()
            {
                Calorii = SumarZi.CaloriiTotale / Macronutrienti.Calorii,
                Grasimi = SumarZi.GrasimiTotale / Macronutrienti.Grasimi,
                Glucide = SumarZi.GlucideTotale / Macronutrienti.Glucide,
                Proteine = SumarZi.ProteineTotale / Macronutrienti.Proteine
            };

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(IstoricZiCurenta)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaIstoricZiCurenta)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(NuExistaInregistrari)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaOInregistrare)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaDouaInregistrari)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaTreiInregistrari)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaMaiMultDeTreiInregistrari)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare1)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare2)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare3)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(SumarZi)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Progres)));
        }
        else if (ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result
                .Equals("\"Nu exista inregistrari ale utilizatorului cautat\""))
        {
            NuExistaInregistrari = !ExistaIstoricZiCurenta;

            SumarZi = new SumarZi()
            {
                CaloriiTotale = 0f,
                GrasimiTotale = 0f,
                GlucideTotale = 0f,
                ProteineTotale = 0f
            };

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(NuExistaInregistrari)));
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(SumarZi)));
        }
        else
        {
            AfiseazaMesajObtinereIstoricNereusita();
        }
    }

    private async void DeconecteazaUtilizator()
    {
        await ConexiuneHttps.TrimiteCerereHttpDeleteAsincron(
            uriCerere: $"api/utilizatori/conectare/{NumeUtilizator}",
            esteDeconectare: true);

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            Application.Current.MainPage = new PaginaConectare();
        }
        else
        {
            AfiseazaMesajDeconectareNereusita();
        }
    }

    private void MergiLaProfil()
    {
        Application.Current.MainPage = new PaginaProfil(NumeUtilizator);
    }

    private void ExtindeIstoric()
    {
        Application.Current.MainPage = new PaginaIstoricDataCalendaristica(
            nameof(PaginaPrincipala), IstoricZiCurenta, NumeUtilizator);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaObtinereInfoUtilizator { get; private set; }
    public ICommand ComandaObtinereIstoricZiCurenta { get; private set; }
    public ICommand ComandaDeconectare { get; private set; }
    public ICommand ComandaProfil { get; private set; }
    public ICommand ComandaExtindereIstoric { get; private set; }
    public Action AfiseazaMesajObtinereInfoNereusita { get; set; }
    public Action AfiseazaMesajObtinereIstoricNereusita { get; set; }
    public Action AfiseazaMesajDeconectareNereusita { get; set; }
    public Macronutrienti Macronutrienti { get; set; }
    public List<Istoric> IstoricZiCurenta { get; set; }
    public Istoric Inregistrare1 { get; set; }
    public Istoric Inregistrare2 { get; set; }
    public Istoric Inregistrare3 { get; set; }
    public bool ExistaIstoricZiCurenta { get; set; }
    public bool ExistaOInregistrare { get; set; }
    public bool ExistaDouaInregistrari { get; set; }
    public bool ExistaTreiInregistrari { get; set; }
    public bool ExistaMaiMultDeTreiInregistrari { get; set; }
    public bool NuExistaInregistrari { get; set; }
    public SumarZi SumarZi { get; set; }
    public Progres Progres { get; set; }
    private string NumeUtilizator { get; init; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
}
