using MobileApp.ViewModels;

namespace MobileApp.Views;

public partial class PaginaOcr : ContentPage
{
	public PaginaOcr(string numeUtilizator, string denumireAliment)
	{
        OcrViewModel = new OcrViewModel(numeUtilizator, denumireAliment);
        OcrViewModel.AfiseazaMesaj +=
            () => DisplayAlert("", "Transmitere ok de param", "Ok");

        BindingContext = OcrViewModel;
        InitializeComponent();
	}

    private OcrViewModel OcrViewModel { get; init; }

    private void BtnIntoarcere_Clicked(object sender, EventArgs e)
    {
        OcrViewModel.ComandaIntoarcereLaAlimentNou.Execute(null);
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

    private async void BtnPoza_Clicked(object sender, EventArgs e)
    {
        var photoStream = await cameraView.TakePhotoAsync();
        var photoData = BinaryData.FromStream(photoStream);

        OcrViewModel.ExtrageValoriNutritionale(photoData);
    }
}