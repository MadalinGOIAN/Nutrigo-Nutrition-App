using Camera.MAUI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace MobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCameraView()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Inter-Black.ttf", "InterBlack");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(
            "EntryPersonalizat",
            (manipulator, element) =>
            {
#if ANDROID
                manipulator.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.White.ToAndroid());
                manipulator.PlatformView.TextCursorDrawable.SetTint(Color.FromRgb(0x28, 0x3D, 0x88).ToInt());
                manipulator.PlatformView.SetPadding(0, 0, 0, 0);
#endif
            });

        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(
            "PickerPersonalizat",
            (manipulator, element) =>
            {
#if ANDROID
                manipulator.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.White.ToAndroid());
                manipulator.PlatformView.SetPadding(0, 0, 0, 0);
#endif
            });

        return builder.Build();
    }
}
