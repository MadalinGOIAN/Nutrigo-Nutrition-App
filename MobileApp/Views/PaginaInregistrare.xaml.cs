using CommunityToolkit.Maui.Alerts;

namespace MobileApp.Views;

public partial class PaginaInregistrare : ContentPage
{
	public PaginaInregistrare()
	{
		InitializeComponent();
	}

    private async void BtnInregistrare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaConectare();
        await Toast.Make("Cont creat cu succes", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
    }

    private void BtnConectare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaConectare();
    }
}