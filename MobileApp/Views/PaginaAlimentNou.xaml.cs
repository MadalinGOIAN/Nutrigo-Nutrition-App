using CommunityToolkit.Maui.Alerts;

namespace MobileApp.Views;

public partial class PaginaAlimentNou : ContentPage
{
	public PaginaAlimentNou()
	{
		InitializeComponent();
	}

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAdaugareAliment();
    }

    private void BtnScanareCodBare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaScanareCodBare(nameof(PaginaAlimentNou));
    }

    private async void BtnSariPeste_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAdaugareAliment();
        await Toast.Make("Aliment creat cu succes", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
    }
}