using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaInregistrare : ContentPage
{
	public PaginaInregistrare()
	{
        InregistrareViewModel = new InregistrareViewModel();
        InregistrareViewModel.AfiseazaMesajUtilizatorExistent +=
            () => DisplayAlert("Eroare", "Numele de utilizator introdus există deja", "Ok");
        InregistrareViewModel.AfiseazaMesajInregistrareInvalida +=
            () => DisplayAlert("Eroare", "Înregistrarea a eșuat. Încercați din nou.", "Ok");

        BindingContext = InregistrareViewModel;
        InitializeComponent();
	}

    private InregistrareViewModel InregistrareViewModel { get; init; }

    private async void BtnInregistrare_Clicked(object sender, EventArgs e)
    {
        if (!SuntToateCampurileCompletate())
        {
            await DisplayAlert("Eroare", "Toate câmpurile sunt obligatorii.", "Ok");
            return;
        }

        if (!entryParola.Text.Equals(entryConfirmareParola.Text))
        {
            await DisplayAlert("Eroare", "Câmpuri \"Parolă\" și \"Confirmare parolă\" invalide.", "Ok");
            return;
        }

        InregistrareViewModel.ComandaInregistrare.Execute(null);
    }

    private bool SuntToateCampurileCompletate()
    {
        return (entryNumeUtilizator.Text != null) && (entryParola.Text != null) &&
            (entryConfirmareParola.Text != null) && (entryPrenume.Text != null) &&
            (entryNumeFamilie.Text != null) && (pickerSex.SelectedItem != null) &&
            (int.Parse(entryVarsta.Text) > 0) && (int.Parse(entryInaltime.Text) > 0) &&
            (int.Parse(entryGreutate.Text) > 0);
    }

    private void BtnConectare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaConectare();
    }

    private void entryNumeUtilizator_Completed(object sender, EventArgs e)
    {
        entryParola.Focus();
    }

    private void entryParola_Completed(object sender, EventArgs e)
    {
        entryConfirmareParola.Focus();
    }

    private void entryConfirmareParola_Completed(object sender, EventArgs e)
    {
        entryPrenume.Focus();
    }

    private void entryPrenume_Completed(object sender, EventArgs e)
    {
        entryNumeFamilie.Focus();
    }

    private void entryNumeFamilie_Completed(object sender, EventArgs e)
    {
        pickerSex.Focus();
    }

    private void pickerSex_SelectedIndexChanged(object sender, EventArgs e)
    {
        entryVarsta.Focus();
    }

    private void entryVarsta_Completed(object sender, EventArgs e)
    {
        entryInaltime.Focus();
    }

    private void entryInaltime_Completed(object sender, EventArgs e)
    {
        entryGreutate.Focus();
    }

    private void entryGreutate_Completed(object sender, EventArgs e)
    {
        pickerNivelActivitateFizica.Focus();
    }

    private void entryGreutate_Loaded(object sender, EventArgs e)
    {
        entryGreutate.Text = string.Empty;
    }

    private void entryInaltime_Loaded(object sender, EventArgs e)
    {
        entryInaltime.Text = string.Empty;
    }

    private void entryVarsta_Loaded(object sender, EventArgs e)
    {
        entryVarsta.Text = string.Empty;
    }
}