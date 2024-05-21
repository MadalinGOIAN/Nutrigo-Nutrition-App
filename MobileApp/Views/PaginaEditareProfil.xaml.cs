using MobileApp.Models;
using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaEditareProfil : ContentPage
{
	public PaginaEditareProfil()
	{
		InitializeComponent();
	}
    
    public PaginaEditareProfil(Utilizator utilizator)
	{
        EditareProfilViewModel = new EditareProfilViewModel(utilizator);
        EditareProfilViewModel.AfiseazaMesajActualizareNereusita +=
            () => DisplayAlert("Eroare", "Eroare la salvarea modificãrilor", "Ok");

        BindingContext = EditareProfilViewModel;
		InitializeComponent();
	}

    private EditareProfilViewModel EditareProfilViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        EditareProfilViewModel.ComandaIntoarcereLaPaginaProfil.Execute(null);
    }

    private void BtnSalvare_Clicked(object sender, EventArgs e)
    {
        EditareProfilViewModel.ComandaActualizareUtilizator.Execute(null);
    }
}