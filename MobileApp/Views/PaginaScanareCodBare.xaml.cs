using Camera.MAUI;
using Camera.MAUI.ZXing;

namespace MobileApp.Views;

public partial class PaginaScanareCodBare : ContentPage
{
	public PaginaScanareCodBare()
	{
		InitializeComponent();

        cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();
        cameraView.BarCodeOptions = new BarcodeDecodeOptions
        {
            PossibleFormats = { BarcodeFormat.EAN_13 }
        };
	}

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PaginaAdaugareAliment();
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
            bool sectiuneAlimentSelectatDeschisa = true;
            Application.Current.MainPage = new PaginaAdaugareAliment(sectiuneAlimentSelectatDeschisa);
        });
    }
}