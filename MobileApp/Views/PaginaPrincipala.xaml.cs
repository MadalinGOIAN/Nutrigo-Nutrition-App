namespace MobileApp.Views;

public partial class PaginaPrincipala : ContentPage
{
	public PaginaPrincipala()
	{
		InitializeComponent();
	}

    private void BtnMaiMulte_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new PaginaIstoricDataCalendaristica(nameof(PaginaPrincipala));
    }

    private void BtnMeniu_Clicked(object sender, EventArgs e)
    {
        meniuExtins.IsEnabled = true;
        meniuExtins.IsVisible = true;
    }

    private void BtnInchidereMeniu_Clicked(object sender, EventArgs e)
    {
        meniuExtins.IsEnabled = false;
        meniuExtins.IsVisible = false;
    }

    private void BtnDeconectare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaConectare();
    }

    private void BtnProfil_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaProfil();
    }

    private void BtnAdaugareAliment_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAdaugareAliment();
    }
}