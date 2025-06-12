using FileServer.Middleware;
using Microsoft.Extensions.FileProviders;

namespace FileServer
{
    public class Program
    {
        public const string FILE_PATH = "files";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            var fileProvider = InitFileProvider(builder.Environment, FILE_PATH);

            // Zugriff auf Dateien auf Server ermoeglichen
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = $"/{FILE_PATH}"
            });

            // Zum Anzeigen der Dateien im Browser
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = fileProvider,
                RequestPath = $"/{FILE_PATH}"
            });


            // POST Requests auf Api Key pruefen
            app.UseSecuredAccess();

            // Minimal API Style
            app.MapPost("/upload", Upload);
            app.MapGet("/download/{fileName}", Download);

            app.Run();


            static PhysicalFileProvider InitFileProvider(IWebHostEnvironment environment, string path)
            {
                var rootPath = Path.Combine(environment.ContentRootPath, path);
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                return new PhysicalFileProvider(rootPath);
            }
        }


        private static async Task<IResult> Upload(HttpRequest request, CancellationToken cancellationToken)
        {
            var file = request.Form.Files.FirstOrDefault();
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), FILE_PATH, file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                var url = $"{request.Scheme}://{request.Host}/{FILE_PATH}/{file.FileName}";
                return Results.Content(url, "text/plain");
            }

            return Results.BadRequest("No files were uploaded.");
        }

        private static IResult Download(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), FILE_PATH, fileName);
            if (File.Exists(path))
            {
                return Results.File(path, "application/octet-stream", fileName);
            }
            return Results.NotFound("Datei nicht gefunden");
        }
    }
}
