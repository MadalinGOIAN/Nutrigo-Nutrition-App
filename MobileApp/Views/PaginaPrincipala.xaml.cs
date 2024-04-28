namespace MobileApp.Views;

public partial class PaginaPrincipala : ContentPage
{
	public PaginaPrincipala()
	{
		InitializeComponent();
	}

    private void MaiMulteBtn_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new PaginaIstoricDataCalendaristica();
    }
}