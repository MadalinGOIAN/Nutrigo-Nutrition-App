﻿using MobileApp.Models;
using MobileApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace MobileApp.ViewModels;

public class ConectareViewModel : INotifyPropertyChanged
{
    public ConectareViewModel() 
    {
        ConexiuneHttps = ConexiuneHttpsSingleton.ObtineInstanta();
        ComandaConectare = new Command(ConecteazaUtilizator);
    }

    private async void ConecteazaUtilizator()
    {
        var valoriConectare = new Dictionary<string, string>()
        {
            { "numeUtilizatorConectat", NumeUtilizator },
            { "parola", Parola }
        };

        await ConexiuneHttps.TrimiteCerereHttpPostAsincron(
            uriCerere: "api/utilizatori/conectare",
            valori: valoriConectare,
            esteConectareUtilizator: true);

        if (ConexiuneHttps.Raspuns.IsSuccessStatusCode)
        {
            Application.Current.MainPage = new PaginaPrincipala(NumeUtilizator);
        }
        else if (ConexiuneHttps.Raspuns.Content.ReadAsStringAsync().Result
                .Equals("\"Utilizatorul este deja conectat\""))
        {
            AfiseazaMesajUtilizatorDejaConectat();
        }
        else
        {
            AfiseazaMesajConectareInvalida();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
    public ICommand ComandaConectare { get; private set; }
    public Action AfiseazaMesajConectareInvalida { get; set; }
    public Action AfiseazaMesajUtilizatorDejaConectat { get; set; }

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

    private string _numeUtilizator;
    private string _parola;
}