using MobileApp.Models;
using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaIstoricDataCalendaristica : ContentPage
{
	public PaginaIstoricDataCalendaristica(string paginaAnterioara, List<Istoric> istoric, string numeUtilizator)
    {
        IstoricDataCalendaristicaViewModel = new IstoricDataCalendaristicaViewModel(
            paginaAnterioara, istoric, numeUtilizator);

        BindingContext = IstoricDataCalendaristicaViewModel;
        InitializeComponent();
    }

    private IstoricDataCalendaristicaViewModel IstoricDataCalendaristicaViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        IstoricDataCalendaristicaViewModel.ComandaIntoarcereLaPaginaAnterioara.Execute(null);
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        IstoricDataCalendaristicaViewModel.ComandaAfisareIstoric.Execute(null);
    }
}