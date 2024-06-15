using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaValidareValori : ContentPage
{
	public PaginaValidareValori(
        string numeUtilizator,
        string denumireAliment,
        string caloriiAliment,
        string grasimiAliment,
        string glucideAliment,
        string proteineAliment)
	{
        ValidareValoriViewModel = new ValidareValoriViewModel(
            numeUtilizator, denumireAliment, caloriiAliment, grasimiAliment, glucideAliment, proteineAliment);

        BindingContext = ValidareValoriViewModel;
        InitializeComponent();
	}

    private ValidareValoriViewModel ValidareValoriViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        ValidareValoriViewModel.ComandaIntoarcereLaOcr.Execute(null);
    }
    
    private void BtnPasUrmator_Clicked(object sender, EventArgs e)
    {
            
    }
}