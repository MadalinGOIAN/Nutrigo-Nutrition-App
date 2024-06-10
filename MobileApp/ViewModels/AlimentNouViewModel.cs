using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class AlimentNouViewModel : INotifyPropertyChanged
{
    public AlimentNouViewModel(string numeUtilizator)
    {
        NumeUtilizator = numeUtilizator;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaIntoarcereLaAdaugareAliment = new Command(IntoarceLaAdaugareAliment);
    }

    private void IntoarceLaAdaugareAliment()
    {
        Application.Current.MainPage = new PaginaAdaugareAliment(NumeUtilizator);
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaAdaugareAliment { get; private set; }
    private string NumeUtilizator { get; init; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
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
