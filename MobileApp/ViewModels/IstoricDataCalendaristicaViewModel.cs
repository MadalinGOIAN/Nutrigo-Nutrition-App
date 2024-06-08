using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class IstoricDataCalendaristicaViewModel : INotifyPropertyChanged
{
    public IstoricDataCalendaristicaViewModel(string paginaAnterioara, List<Istoric> istoric, string numeUtilizator)
    {
        PaginaAnterioara = paginaAnterioara;
        Istoric = istoric;
        NumeUtilizator = numeUtilizator;
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaIntoarcereLaPaginaAnterioara = new Command(IntoarceLaPaginaAnterioara);
        ComandaAfisareIstoric = new Command(AfiseazaIstoric);
    }

    private void IntoarceLaPaginaAnterioara()
    {
        switch (PaginaAnterioara)
        {
            case nameof(PaginaIstoric):
                Application.Current.MainPage = new PaginaIstoric(NumeUtilizator);
                break;

            case nameof(PaginaPrincipala):
                Application.Current.MainPage = new PaginaPrincipala(NumeUtilizator);
                break;
        }
    }

    private void AfiseazaIstoric()
    {
        Data = Istoric.FirstOrDefault().Data;

        switch (Istoric.Count)
        {
            case 4:
                Inregistrare1 = Istoric.ElementAt(0); ExistaInregistrare1 = true;
                Inregistrare2 = Istoric.ElementAt(1); ExistaInregistrare2 = true;
                Inregistrare3 = Istoric.ElementAt(2); ExistaInregistrare3 = true;
                Inregistrare4 = Istoric.ElementAt(3); ExistaInregistrare4 = true;
                break;
            
            case 5:
                Inregistrare1 = Istoric.ElementAt(0); ExistaInregistrare1 = true;
                Inregistrare2 = Istoric.ElementAt(1); ExistaInregistrare2 = true;
                Inregistrare3 = Istoric.ElementAt(2); ExistaInregistrare3 = true;
                Inregistrare4 = Istoric.ElementAt(3); ExistaInregistrare4 = true;
                Inregistrare5 = Istoric.ElementAt(4); ExistaInregistrare5 = true;
                break;

            case 6:
                Inregistrare1 = Istoric.ElementAt(0); ExistaInregistrare1 = true;
                Inregistrare2 = Istoric.ElementAt(1); ExistaInregistrare2 = true;
                Inregistrare3 = Istoric.ElementAt(2); ExistaInregistrare3 = true;
                Inregistrare4 = Istoric.ElementAt(3); ExistaInregistrare4 = true;
                Inregistrare5 = Istoric.ElementAt(4); ExistaInregistrare5 = true;
                Inregistrare6 = Istoric.ElementAt(5); ExistaInregistrare6 = true;
                break;

            case 7:
                Inregistrare1 = Istoric.ElementAt(0); ExistaInregistrare1 = true;
                Inregistrare2 = Istoric.ElementAt(1); ExistaInregistrare2 = true;
                Inregistrare3 = Istoric.ElementAt(2); ExistaInregistrare3 = true;
                Inregistrare4 = Istoric.ElementAt(3); ExistaInregistrare4 = true;
                Inregistrare5 = Istoric.ElementAt(4); ExistaInregistrare5 = true;
                Inregistrare6 = Istoric.ElementAt(5); ExistaInregistrare6 = true;
                Inregistrare7 = Istoric.ElementAt(6); ExistaInregistrare7 = true;
                break;

            case 8:
                Inregistrare1 = Istoric.ElementAt(0); ExistaInregistrare1 = true;
                Inregistrare2 = Istoric.ElementAt(1); ExistaInregistrare2 = true;
                Inregistrare3 = Istoric.ElementAt(2); ExistaInregistrare3 = true;
                Inregistrare4 = Istoric.ElementAt(3); ExistaInregistrare4 = true;
                Inregistrare5 = Istoric.ElementAt(4); ExistaInregistrare5 = true;
                Inregistrare6 = Istoric.ElementAt(5); ExistaInregistrare6 = true;
                Inregistrare7 = Istoric.ElementAt(6); ExistaInregistrare7 = true;
                Inregistrare8 = Istoric.ElementAt(7); ExistaInregistrare8 = true;
                break;

            case 9:
                Inregistrare1 = Istoric.ElementAt(0); ExistaInregistrare1 = true;
                Inregistrare2 = Istoric.ElementAt(1); ExistaInregistrare2 = true;
                Inregistrare3 = Istoric.ElementAt(2); ExistaInregistrare3 = true;
                Inregistrare4 = Istoric.ElementAt(3); ExistaInregistrare4 = true;
                Inregistrare5 = Istoric.ElementAt(4); ExistaInregistrare5 = true;
                Inregistrare6 = Istoric.ElementAt(5); ExistaInregistrare6 = true;
                Inregistrare7 = Istoric.ElementAt(6); ExistaInregistrare7 = true;
                Inregistrare8 = Istoric.ElementAt(7); ExistaInregistrare8 = true;
                Inregistrare9 = Istoric.ElementAt(8); ExistaInregistrare9 = true;
                break;

            case 10:
                Inregistrare1 = Istoric.ElementAt(0); ExistaInregistrare1 = true;
                Inregistrare2 = Istoric.ElementAt(1); ExistaInregistrare2 = true;
                Inregistrare3 = Istoric.ElementAt(2); ExistaInregistrare3 = true;
                Inregistrare4 = Istoric.ElementAt(3); ExistaInregistrare4 = true;
                Inregistrare5 = Istoric.ElementAt(4); ExistaInregistrare5 = true;
                Inregistrare6 = Istoric.ElementAt(5); ExistaInregistrare6 = true;
                Inregistrare7 = Istoric.ElementAt(6); ExistaInregistrare7 = true;
                Inregistrare8 = Istoric.ElementAt(7); ExistaInregistrare8 = true;
                Inregistrare9 = Istoric.ElementAt(8); ExistaInregistrare9 = true;
                Inregistrare10 = Istoric.ElementAt(9); ExistaInregistrare10 = true;
                break;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaIntoarcereLaPaginaAnterioara { get; private set; }
    public ICommand ComandaAfisareIstoric { get; private set; }
    private string PaginaAnterioara { get; init; }
    public List<Istoric> Istoric { get; set; }
    private string NumeUtilizator { get; init; }
    private ConexiuneHttpsSingleton ConexiuneHttps { get; init; }
    
    #region Proprietati
    public DateTime Data 
    { 
        get => _data;
        set 
        {
            _data = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Data)));
        }
    }
    public Istoric Inregistrare1 
    { 
        get => _inregistrare1;
        set 
        {
            _inregistrare1 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare1)));
        }
    }
    public Istoric Inregistrare2 
    { 
        get => _inregistrare2;
        set 
        {
            _inregistrare2 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare2)));
        }
    }
    public Istoric Inregistrare3 
    { 
        get => _inregistrare3;
        set 
        {
            _inregistrare3 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare3)));
        }
    }
    public Istoric Inregistrare4 
    { 
        get => _inregistrare4;
        set 
        {
            _inregistrare4 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare4)));
        }
    }
    public Istoric Inregistrare5 
    { 
        get => _inregistrare5;
        set 
        {
            _inregistrare5 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare5)));
        }
    }
    public Istoric Inregistrare6 
    { 
        get => _inregistrare6;
        set 
        {
            _inregistrare6 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare6)));
        }
    }
    public Istoric Inregistrare7 
    { 
        get => _inregistrare7;
        set 
        {
            _inregistrare7 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare7)));
        }
    }
    public Istoric Inregistrare8 
    { 
        get => _inregistrare8;
        set 
        {
            _inregistrare8 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare8)));
        }
    }
    public Istoric Inregistrare9 
    { 
        get => _inregistrare9;
        set 
        {
            _inregistrare9 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare9)));
        }
    }
    public Istoric Inregistrare10 
    { 
        get => _inregistrare10;
        set 
        {
            _inregistrare10 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Inregistrare10)));
        }
    }
    public bool ExistaInregistrare1 
    { 
        get => _existaInregistrare1;
        set 
        {
            _existaInregistrare1 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare1)));
        }
    }
    public bool ExistaInregistrare2 
    { 
        get => _existaInregistrare2;
        set 
        {
            _existaInregistrare2 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare2)));
        }
    }
    public bool ExistaInregistrare3 
    { 
        get => _existaInregistrare3;
        set 
        {
            _existaInregistrare3 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare3)));
        }
    }
    public bool ExistaInregistrare4 
    { 
        get => _existaInregistrare4;
        set 
        {
            _existaInregistrare4 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare4)));
        }
    }
    public bool ExistaInregistrare5 
    { 
        get => _existaInregistrare5;
        set 
        {
            _existaInregistrare5 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare5)));
        }
    }
    public bool ExistaInregistrare6 
    { 
        get => _existaInregistrare6;
        set 
        {
            _existaInregistrare6 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare6)));
        }
    }
    public bool ExistaInregistrare7 
    { 
        get => _existaInregistrare7;
        set 
        {
            _existaInregistrare7 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare7)));
        }
    }
    public bool ExistaInregistrare8 
    { 
        get => _existaInregistrare8;
        set 
        {
            _existaInregistrare8 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare8)));
        }
    }
    public bool ExistaInregistrare9 
    { 
        get => _existaInregistrare9;
        set 
        {
            _existaInregistrare9 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare9)));
        }
    }
    public bool ExistaInregistrare10 
    { 
        get => _existaInregistrare10;
        set 
        {
            _existaInregistrare10 = value;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExistaInregistrare10)));
        }
    }
    #endregion

    private DateTime _data;
    private Istoric _inregistrare1;
    private Istoric _inregistrare2;
    private Istoric _inregistrare3;
    private Istoric _inregistrare4;
    private Istoric _inregistrare5;
    private Istoric _inregistrare6;
    private Istoric _inregistrare7;
    private Istoric _inregistrare8;
    private Istoric _inregistrare9;
    private Istoric _inregistrare10;
    private bool _existaInregistrare1;
    private bool _existaInregistrare2;
    private bool _existaInregistrare3;
    private bool _existaInregistrare4;
    private bool _existaInregistrare5;
    private bool _existaInregistrare6;
    private bool _existaInregistrare7;
    private bool _existaInregistrare8;
    private bool _existaInregistrare9;
    private bool _existaInregistrare10;
}
