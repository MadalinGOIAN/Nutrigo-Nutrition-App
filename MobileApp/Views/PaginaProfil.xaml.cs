using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaProfil : ContentPage
{
	public PaginaProfil()
	{
		InitializeComponent();
	}

    public PaginaProfil(string numeUtilizator)
    {
        ProfilViewModel = new ProfilViewModel(numeUtilizator);
        ProfilViewModel.AfiseazaMesajObtinereInfoNereusita +=
            () => DisplayAlert("Eroare", "Eroare la obținerea informațiilor despre utilizator", "Ok");

        BindingContext = ProfilViewModel;
        InitializeComponent();
    }

    private ProfilViewModel ProfilViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        ProfilViewModel.ComandaIntoarcereLaPaginaPrincipala.Execute(null);
    }

    private void BtnEditareProfil_Clicked(object sender, EventArgs e)
    {
        ProfilViewModel.ComandaEditareProfil.Execute(null);
    }

    private void BtnIstoric_Clicked(object sender, EventArgs e)
    {
        ProfilViewModel.ComandaIstoric.Execute(null);
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        ProfilViewModel.ComandaObtinereInfoUtilizator.Execute(null);
    }
}