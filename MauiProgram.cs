using Microsoft.Extensions.Logging;
using Avance2Progreso.Repositories;
namespace Avance2Progreso
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            string dbPath = FileAccessHelper.GetLocalFilePath("GabrielCalderon.db3");
            
            builder.Services.AddSingleton<UserRepository>(s => ActivatorUtilities.CreateInstance<UserRepository>(s, dbPath));
            builder.Services.AddSingleton<CompetenciasRepository>(a => ActivatorUtilities.CreateInstance<CompetenciasRepository>(a, dbPath));
            builder.Services.AddSingleton<HttpClient>(sp =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5206/api/Users")
                };
            });
            builder.Services.AddSingleton<Services.UserService>();
            
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
    //Creo un FileAccessHelper porque no funcionaba directamente con el codigo el anterior
    public static class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folderPath, filename);
        }
    }
}
