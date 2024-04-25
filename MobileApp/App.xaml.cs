using MobileApp.Views;

namespace MobileApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new PaginaConectare();
    }
}
