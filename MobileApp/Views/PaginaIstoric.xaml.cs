namespace MobileApp.Views;

public partial class PaginaIstoric : ContentPage
{
	public PaginaIstoric()
	{
		InitializeComponent();
	}

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaProfil();
    }

    private void BtnMaiMulte_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaIstoricDataCalendaristica();
    }
}