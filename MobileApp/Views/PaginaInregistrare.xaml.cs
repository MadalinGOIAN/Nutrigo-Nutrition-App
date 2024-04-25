namespace MobileApp.Views;

public partial class PaginaInregistrare : ContentPage
{
	public PaginaInregistrare()
	{
		InitializeComponent();
	}

    private void BtnInregistrare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaConectare();
    }

    private void BtnConectare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaConectare();
    }
}