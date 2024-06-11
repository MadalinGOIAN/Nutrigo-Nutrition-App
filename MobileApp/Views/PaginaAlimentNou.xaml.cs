using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaAlimentNou : ContentPage
{
	public PaginaAlimentNou(string numeUtilizator)
	{
        AlimentNouViewModel = new AlimentNouViewModel(numeUtilizator);

        BindingContext = AlimentNouViewModel;
        InitializeComponent();
	}

    private AlimentNouViewModel AlimentNouViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        AlimentNouViewModel.ComandaIntoarcereLaAdaugareAliment.Execute(null);
    }

    private void BtnPasUrmator_Clicked(object sender, EventArgs e)
    {
        AlimentNouViewModel.ComandaPasUrmator.Execute(null);
    }
}