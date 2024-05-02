namespace MobileApp.Views;

public partial class PaginaEditareProfil : ContentPage
{
	public PaginaEditareProfil()
	{
		InitializeComponent();
	}

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaProfil();
    }

    private void BtnSalvare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaProfil();
    }
}