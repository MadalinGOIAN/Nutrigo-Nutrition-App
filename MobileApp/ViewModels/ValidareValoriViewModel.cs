using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class ValidareValoriViewModel : INotifyPropertyChanged
{
    public ValidareValoriViewModel(
        string numeUtilizator,
        string denumireAliment,
        string caloriiAliment,
        string grasimiAliment,
        string glucideAliment,
        string proteineAliment)
    {
        NumeUtilizator = numeUtilizator;
        DenumireAliment = denumireAliment;
        CaloriiAliment = caloriiAliment;
        GrasimiAliment = grasimiAliment.Replace(',', '.');
        GlucideAliment = glucideAliment.Replace(',', '.');
        ProteineAliment = proteineAliment.Replace(',', '.');
        ComandaIntoarcereLaOcr = new Command(IntoarceLaOcr);
        ComandaScanareCodBare = new Command(MergiLaScanareCodBare);
    }

    private void IntoarceLaOcr()
    {
        Application.Current.MainPage = new PaginaOcr(NumeUtilizator, DenumireAliment);
    }

    private void MergiLaScanareCodBare()
    {
        Application.Current.MainPage = new PaginaScanareCodBare(
            nameof(PaginaValidareValori),
            NumeUtilizator,
            DenumireAliment,
            CaloriiAliment,
            GrasimiAliment,
            GlucideAliment,
            ProteineAliment);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaOcr { get; private set; }
    public ICommand ComandaScanareCodBare { get; private set; }
    private string NumeUtilizator { get; init; }
    private string DenumireAliment { get; init; }
    public string CaloriiAliment
    {
        get => _caloriiAliment;
        set
        {
            _caloriiAliment = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(CaloriiAliment)));
        }
    }
    public string GrasimiAliment
    {
        get => _grasimiAliment;
        set
        {
            _grasimiAliment = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(GrasimiAliment)));
        }
    }
    public string GlucideAliment
    {
        get => _glucideAliment;
        set
        {
            _glucideAliment = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(GlucideAliment)));
        }
    }
    public string ProteineAliment
    {
        get => _proteineAliment;
        set
        {
            _proteineAliment = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ProteineAliment)));
        }
    }

    private string _caloriiAliment;
    private string _grasimiAliment;
    private string _glucideAliment;
    private string _proteineAliment;
}
