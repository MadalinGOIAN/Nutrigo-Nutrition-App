using Camera.MAUI;
using Camera.MAUI.ZXing;
using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaScanareCodBare : ContentPage
{
	public PaginaScanareCodBare(string paginaAnterioara)
	{
		InitializeComponent();

        cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();
        cameraView.BarCodeOptions = new BarcodeDecodeOptions
        {
            PossibleFormats = { BarcodeFormat.EAN_13, BarcodeFormat.EAN_8 }
        };
	}
    
    public PaginaScanareCodBare(string paginaAnterioara, string numeUtilizator)
	{
        ScanareCodBareViewModel = new ScanareCodBareViewModel(paginaAnterioara, numeUtilizator);
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