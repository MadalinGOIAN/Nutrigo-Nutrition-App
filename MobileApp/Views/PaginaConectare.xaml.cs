using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaConectare : ContentPage
{
	public PaginaConectare()
	{
        ConectareViewModel = new ConectareViewModel();
        ConectareViewModel.AfiseazaMesajUtilizatorDejaConectat +=
            () => DisplayAlert("Eroare", "Utilizatorul este deja conectat.", "Ok");
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

    private async void EvenimentFormConectareCompletat(object sender, EventArgs e)
    {
        if (!SuntToateCampurileCompletate())
        {
            await DisplayAlert("Eroare", "Toate câmpurile sunt obligatorii.", "Ok");
            return;
        }

        ConectareViewModel.ComandaConectare.Execute(null);
    }

    private bool SuntToateCampurileCompletate()
    {
        return (entryNumeUtilizator.Text != null) && (entryParola.Text != null);
    }
}