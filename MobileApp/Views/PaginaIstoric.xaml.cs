using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaIstoric : ContentPage
{
	public PaginaIstoric()
	{
		InitializeComponent();
	}
    
    public PaginaIstoric(string numeUtilizator)
	{
        IstoricViewModel = new IstoricViewModel(numeUtilizator);
        IstoricViewModel.AfiseazaMesajObtinereIstoricNereusita +=
            () => DisplayAlert("Eroare", "Obținerea istoricului a eșuat", "Ok");

        BindingContext = IstoricViewModel;
		InitializeComponent();
	}

    private IstoricViewModel IstoricViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        IstoricViewModel.ComandaIntoarcereLaProfil.Execute(null);
    }

    private void BtnMaiMulte_Clicked(object sender, EventArgs e)
    {
        IstoricViewModel.ComandaExtindereIstoric.Execute(null);
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        IstoricViewModel.ComandaObtinereIstoric.Execute(null);
    }

    private void BtnDataInapoi_Clicked(object sender, EventArgs e)
    {
        IstoricViewModel.ComandaDataStanga.Execute(null);
    }
    
    private void BtnDataInainte_Clicked(object sender, EventArgs e)
    {
        IstoricViewModel.ComandaDataDreapta.Execute(null);
    }
}