using Camera.MAUI;
using Camera.MAUI.ZXing;
using CommunityToolkit.Maui.Alerts;

namespace MobileApp.Views;

public partial class PaginaScanareCodBare : ContentPage
{
	public PaginaScanareCodBare(string paginaAnterioara)
	{
        PaginaAnterioara = paginaAnterioara;
		InitializeComponent();

        cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();
        cameraView.BarCodeOptions = new BarcodeDecodeOptions
        {
            PossibleFormats = { BarcodeFormat.EAN_13, BarcodeFormat.EAN_8 }
        };
	}

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        switch (PaginaAnterioara)
        {
            case nameof(PaginaAdaugareAliment):
                Application.Current.MainPage = new PaginaAdaugareAliment();
                break;

            case nameof(PaginaAlimentNou):
                Application.Current.MainPage = new PaginaAlimentNou();
                break;
        }
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
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            switch (PaginaAnterioara)
            {
                case nameof(PaginaAdaugareAliment):
                    Application.Current.MainPage = new PaginaAdaugareAliment(sectiuneAlimentSelectatDeschisa: true);
                    break;

                case nameof(PaginaAlimentNou):
                    Application.Current.MainPage = new PaginaAdaugareAliment();
                    await Toast.Make("Aliment creat cu succes", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                    break;
            }
        });
    }

    private string PaginaAnterioara { get; init; }
}