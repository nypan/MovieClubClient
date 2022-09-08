using System.Net.WebSockets;

namespace ApiClientGenerator;
using NSwag;
using NSwag.CodeGeneration.CSharp;


class Program
{
    static async Task Main(string[] args)
    {
        var url = args[0];
        var generatePath = Path.Combine(Directory.GetCurrentDirectory(), args[1]);
        await GenerateCSharpClient(url, generatePath);

    }
    async static Task GenerateCSharpClient
        (string url,
        string generatedPath) =>
        await GenerateClient(
            document: await OpenApiDocument.FromUrlAsync(url),
            generatePath: generatedPath,
            generateCode: (OpenApiDocument document) =>
            {
                var settings = new CSharpClientGeneratorSettings
                {
                    UseBaseUrl = false,
                    ClassName = "MovieClubApiClient",
                    CSharpGeneratorSettings =
                    {
                        Namespace = "MovieClubClient"
                    }
                };
                var generator = new CSharpClientGenerator(document, settings);
                var code = generator.GenerateFile();
                return code;
            }
            );

    
    private async static Task GenerateClient(OpenApiDocument document, string generatePath, Func<OpenApiDocument, string> generateCode)
    {
        Console.WriteLine($"Generating {generatePath}...");

        var code = generateCode(document);

        await System.IO.File.WriteAllTextAsync(generatePath, code);
    }
}