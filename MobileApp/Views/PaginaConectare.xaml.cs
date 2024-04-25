namespace MobileApp.Views;

public partial class PaginaConectare : ContentPage
{
	public PaginaConectare()
	{
		InitializeComponent();
	}

    private void BtnConectare_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new PaginaPrincipala();
    }

    private void BtnInregistrare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaInregistrare();
    }
}