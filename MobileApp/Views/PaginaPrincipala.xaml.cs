using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaPrincipala : ContentPage
{
    public PaginaPrincipala()
    {
        InitializeComponent();
    }

    public PaginaPrincipala(string numeUtilizator)
	{
        PrincipalaViewModel = new PrincipalaViewModel(numeUtilizator);
        PrincipalaViewModel.AfiseazaMesajObtinereInfoNereusita +=
            () => DisplayAlert("Eroare", "Eroare la obținerea informațiilor despre utilizator", "Ok");
        PrincipalaViewModel.AfiseazaMesajObtinereIstoricNereusita +=
            () => DisplayAlert("Eroare", "Eroare la obținerea istoricului", "Ok");

        BindingContext = PrincipalaViewModel;
        InitializeComponent();
	}

    private PrincipalaViewModel PrincipalaViewModel { get; init; }

    private void BtnMaiMulte_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new PaginaIstoricDataCalendaristica(nameof(PaginaPrincipala));
    }

    private void BtnMeniu_Clicked(object sender, EventArgs e)
    {
        meniuExtins.IsEnabled = true;
        meniuExtins.IsVisible = true;
    }

    private void BtnInchidereMeniu_Clicked(object sender, EventArgs e)
    {
        meniuExtins.IsEnabled = false;
        meniuExtins.IsVisible = false;
    }

    private void BtnDeconectare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaConectare();
    }

    private void BtnProfil_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaProfil();
    }

    private void BtnAdaugareAliment_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAdaugareAliment();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        PrincipalaViewModel.ComandaObtinereInfoUtilizator.Execute(null);
    }

    private void FrameAlimenteConsumate_Loaded(object sender, EventArgs e)
    {
        PrincipalaViewModel.ComandaObtinereIstoricZiCurenta.Execute(null);
    }

    private void progressBarCalorii_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Progress"))
        {
            var diferenta = PrincipalaViewModel.Macronutrienti.Calorii - PrincipalaViewModel.SumarZi.CaloriiTotale;

            if (diferenta >= 1)
            {
                labelCaloriiRamase.Text = $"{diferenta:0.##} kcal rămase";
            }
            else if (diferenta <= -1)
            {
                progressBarCalorii.ProgressColor = Color.FromArgb("DB2B2B");
                labelCaloriiRamase.Text = $"{-diferenta:0.##} kcal peste";
            }
            else
            {
                labelCaloriiRamase.Text = "Obiectiv atins";
            }
        }
    }

    private void progressBarGrasimi_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Progress"))
        {
            var diferenta = PrincipalaViewModel.Macronutrienti.Grasimi - PrincipalaViewModel.SumarZi.GrasimiTotale;

            if (diferenta >= 1)
            {
                labelGrasimiRamase.Text = $"{diferenta:0.##} g rămase";
            }
            else if (diferenta <= -1)
            {
                progressBarGrasimi.ProgressColor = Color.FromArgb("DB2B2B");
                labelGrasimiRamase.Text = $"{-diferenta:0.##} g peste";
            }
            else
            {
                labelGrasimiRamase.Text = "Obiectiv atins";
            }
        }
    }

    private void progressBarGlucide_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Progress"))
        {
            var diferenta = PrincipalaViewModel.Macronutrienti.Glucide - PrincipalaViewModel.SumarZi.GlucideTotale;

            if (diferenta >= 1)
            {
                labelGlucideRamase.Text = $"{diferenta:0.##} g rămase";
            }
            else if (diferenta <= -1)
            {
                progressBarGlucide.ProgressColor = Color.FromArgb("DB2B2B");
                labelGlucideRamase.Text = $"{-diferenta:0.##} g peste";
            }
            else
            {
                labelGlucideRamase.Text = "Obiectiv atins";
            }
        }
    }

    private void progressBarProteine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Progress"))
        {
            var diferenta = PrincipalaViewModel.Macronutrienti.Proteine - PrincipalaViewModel.SumarZi.ProteineTotale;

            if (diferenta >= 1)
            {
                labelProteineRamase.Text = $"{diferenta:0.##} g rămase";
            }
            else if (diferenta <= -1)
            {
                progressBarProteine.ProgressColor = Color.FromArgb("DB2B2B");
                labelProteineRamase.Text = $"{-diferenta:0.##} g peste";
            }
            else
            {
                labelProteineRamase.Text = "Obiectiv atins";
            }
        }
    }
}