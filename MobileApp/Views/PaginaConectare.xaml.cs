using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaConectare : ContentPage
{
	public PaginaConectare()
	{
        ConectareViewModel = new ConectareViewModel();
        ConectareViewModel.AfiseazaMesajConectareInvalida +=
            () => DisplayAlert("Eroare", "Nume utilizator sau parolă invalidă. Încercați din nou.", "Ok");

        BindingContext = ConectareViewModel;
		InitializeComponent();
	}
    private ConectareViewModel ConectareViewModel { get; init; }

    private void BtnInregistrare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaInregistrare();
    }


    private void entryNumeUtilizator_Completed(object sender, EventArgs e)
    {
        entryParola.Focus();
    }

    private void entryParola_Completed(object sender, EventArgs e)
    {
        ConectareViewModel.ComandaConectare.Execute(null);
    }
}