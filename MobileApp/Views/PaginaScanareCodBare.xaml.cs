using Camera.MAUI;
using Camera.MAUI.ZXing;
using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaScanareCodBare : ContentPage
{
	public PaginaScanareCodBare(
        string paginaAnterioara,
        string numeUtilizator,
        string denumireAliment = null,
        string caloriiAliment = null,
        string grasimiAliment = null,
        string glucideAliment = null,
        string proteineAliment = null)
	{
        switch (paginaAnterioara)
        {
            case nameof(PaginaAdaugareAliment):
                ScanareCodBareViewModel = new ScanareCodBareViewModel(paginaAnterioara, numeUtilizator);
                break;

            case nameof(PaginaValidareValori):
                ScanareCodBareViewModel = new ScanareCodBareViewModel(
                    paginaAnterioara, 
                    numeUtilizator, 
                    denumireAliment, 
                    caloriiAliment, 
                    grasimiAliment, 
                    glucideAliment,
                    proteineAliment);

                ScanareCodBareViewModel.AfiseazaMesajEroareAdaugareAlimentNou +=
                    () => DisplayAlert("Eroare", "Eroare la înregistrarea alimentului", "Ok");
                break;
        }

        ScanareCodBareViewModel.AfiseazaMesajAlimentNegasit +=
            () => DisplayAlert("Aliment inexistent", "Alimentul nu a fost gãsit", "Ok");

        BindingContext = ScanareCodBareViewModel;
        InitializeComponent();

        cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();
        cameraView.BarCodeOptions = new BarcodeDecodeOptions
        {
            PossibleFormats = { BarcodeFormat.EAN_13, BarcodeFormat.EAN_8 }
        };
	}

    private ScanareCodBareViewModel ScanareCodBareViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        ScanareCodBareViewModel.ComandaIntoarcereLaPaginaAnterioara.Execute(null);
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.FirstOrDefault();

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(500);
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }

    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ScanareCodBareViewModel.ExtrageCodBare(args.Result.FirstOrDefault().Text);
        });
    }
}