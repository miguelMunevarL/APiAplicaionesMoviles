using ApiConsumos.Interfaces;
using ApiConsumos.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Maui;

namespace ApiConsumos
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit(options => options.SetShouldEnableSnackbarOnWindows(true));

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient<ITodo, ToDoService>();
            return builder.Build();
        }
    }
}