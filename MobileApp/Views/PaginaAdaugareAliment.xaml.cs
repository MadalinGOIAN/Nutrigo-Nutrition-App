using CommunityToolkit.Maui.Alerts;

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

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaPrincipala();
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
    }

    private void BtnCreareAliment_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAlimentNou();
    }

    private void BtnScanareCodBare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaScanareCodBare();
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
    }

    private async void BtnConfirmareAdaugareAliment_Clicked(object sender, EventArgs e)
    {
        await Toast.Make("Alimentul a fost adãugat", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        Application.Current.MainPage = new PaginaPrincipala();
    }
}