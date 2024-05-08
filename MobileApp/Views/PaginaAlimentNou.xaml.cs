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

    private void BtnPasUrmator_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaOcr();
    }
}