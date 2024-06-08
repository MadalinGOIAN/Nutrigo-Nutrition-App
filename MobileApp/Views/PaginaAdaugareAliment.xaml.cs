using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaAdaugareAliment : ContentPage
{
	public PaginaAdaugareAliment(bool sectiuneAlimentSelectatDeschisa = false)
	{
		InitializeComponent();

        if (sectiuneAlimentSelectatDeschisa)
        {
            gridAlimentSelectat.IsEnabled = true;
            gridAlimentSelectat.IsVisible = true;
        }
	}
    
    public PaginaAdaugareAliment(string numeUtilizator, bool sectiuneAlimentSelectatDeschisa = false)
	{
        AdaugareAlimentViewModel = new AdaugareAlimentViewModel(numeUtilizator);
        AdaugareAlimentViewModel.AfiseazaMesajObtinereAlimenteNereusita +=
            () => DisplayAlert("Eroare", "Eroare la obținerea alimentelor", "Ok");
        AdaugareAlimentViewModel.AfiseazaMesajAdaugareNereusita +=
            () => DisplayAlert("Eroare", "Eroare la adăugarea alimentului", "Ok");

        BindingContext = AdaugareAlimentViewModel;
        InitializeComponent();

        if (sectiuneAlimentSelectatDeschisa)
        {
            gridAlimentSelectat.IsEnabled = true;
            gridAlimentSelectat.IsVisible = true;
        }
	}

    private AdaugareAlimentViewModel AdaugareAlimentViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        AdaugareAlimentViewModel.ComandaIntoarcereLaPaginaPrincipala.Execute(null);
    }

    private void EntryCautareAliment_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (entryCautareAliment.Text.Length.Equals(0))
        {
            btnStergereCautareAliment.IsEnabled = false;
            btnStergereCautareAliment.IsVisible = false;
        }
        else
        {
            btnStergereCautareAliment.IsEnabled = true;
            btnStergereCautareAliment.IsVisible = true;
        }
    }

    private void btnStergereCautareAliment_Clicked(object sender, EventArgs e)
    {
        entryCautareAliment.Text = "";
        AdaugareAlimentViewModel.ComandaAscundereRezultate.Execute(null);
    }

    private void BtnCreareAliment_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAlimentNou();
    }

    private void BtnScanareCodBare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaScanareCodBare(nameof(PaginaAdaugareAliment));
    }

    private void BtnIesireAlimentSelectat_Clicked(object sender, EventArgs e)
    {
        gridAlimentSelectat.IsEnabled = false;
        gridAlimentSelectat.IsVisible = false;
    }

    private void BtnSelectareAliment_Clicked(object sender, EventArgs e)
    {
        gridAlimentSelectat.IsEnabled = true;
        gridAlimentSelectat.IsVisible = true;

        AdaugareAlimentViewModel.CompleteazaCampAlimentSelectat((sender as Button).Text);
    }

    private async void BtnConfirmareAdaugareAliment_Clicked(object sender, EventArgs e)
    {
        gridAlimentSelectat.IsEnabled = false;
        gridAlimentSelectat.IsVisible = false;

        AdaugareAlimentViewModel.ComandaAdaugareInregistrareInIstoric.Execute(null);
    }

    private void entryCautareAliment_Completed(object sender, EventArgs e)
    {
        AdaugareAlimentViewModel.ComandaCautareAliment.Execute(null);
    }
}