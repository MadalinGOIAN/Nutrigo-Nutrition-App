namespace MobileApp.Views;

public partial class PaginaIstoricDataCalendaristica : ContentPage
{
	public PaginaIstoricDataCalendaristica(string paginaAnterioara)
    {
        PaginaAnterioara = paginaAnterioara;
        InitializeComponent();
    }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        switch (PaginaAnterioara)
        {
            case nameof(PaginaIstoric):
                Application.Current.MainPage = new PaginaIstoric();
                break;

            case nameof(PaginaPrincipala):
                Application.Current.MainPage = new PaginaPrincipala();
                break;
        }
    }

    private string PaginaAnterioara { get; init; }
}