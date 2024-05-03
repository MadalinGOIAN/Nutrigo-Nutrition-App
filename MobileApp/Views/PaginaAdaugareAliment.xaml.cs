namespace MobileApp.Views;

public partial class PaginaAdaugareAliment : ContentPage
{
	public PaginaAdaugareAliment()
	{
		InitializeComponent();
	}

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaPrincipala();
    }

    private void EntryCautareAliment_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (entryCautareAliment.Text.Length.Equals(0))
        {
            btnStergereCautareAliment.IsEnabled = false;
            btnStergereCautareAliment.IsVisible = false;
        }
        else
        {
            btnStergereCautareAliment.IsEnabled = true;
            btnStergereCautareAliment.IsVisible = true;
        }
    }

    private void btnStergereCautareAliment_Clicked(object sender, EventArgs e)
    {
        entryCautareAliment.Text = "";
    }

    private void BtnCreareAliment_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAlimentNou();
    }

    private void BtnScanareCodBare_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaScanareCodBare();
    }
}