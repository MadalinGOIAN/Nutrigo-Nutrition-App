using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class AlimentNouViewModel : INotifyPropertyChanged
{
    public AlimentNouViewModel(string numeUtilizator)
    {
        NumeUtilizator = numeUtilizator;
        ComandaIntoarcereLaAdaugareAliment = new Command(IntoarceLaAdaugareAliment);
        ComandaPasUrmator = new Command(MergiLaOcr);
    }

    private void IntoarceLaAdaugareAliment()
    {
        Application.Current.MainPage = new PaginaAdaugareAliment(NumeUtilizator);
    }
    
    private void MergiLaOcr()
    {
        Application.Current.MainPage = new PaginaOcr(NumeUtilizator, DenumireAliment);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaAdaugareAliment { get; private set; }
    public ICommand ComandaPasUrmator { get; private set; }
    private string NumeUtilizator { get; init; }
    public string DenumireAliment
    {
        get => _denumireAliment;
        set
        {
            _denumireAliment = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(DenumireAliment)));
        }
    }

    private string _denumireAliment;
}
