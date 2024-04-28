namespace MobileApp.Views;

public partial class PaginaProfil : ContentPage
{
	public PaginaProfil()
	{
		InitializeComponent();
	}

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaPrincipala();
    }

    private void BtnEditareProfil_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaEditareProfil();
    }

    private void BtnIstoric_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaIstoric();
    }
}