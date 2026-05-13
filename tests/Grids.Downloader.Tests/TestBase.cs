using Microsoft.Extensions.Configuration;
using OutOfHome.DataProviders.Boards.Grids.Downloader.Tests.Extensions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Tests;

public class TestBase
{
    private readonly ITestOutputHelper Output;
    protected readonly IConfiguration Configuration;
    protected readonly JsonSerializerOptions defaultJsonOptions;

    protected TestBase(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));

        defaultJsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };

        var builder = new ConfigurationBuilder()
                .AddUserSecrets<TestBase>();

        Configuration = builder.Build();
    }
    protected string GetFilesFolder(string provider)
    {
        var workFolder = Configuration.GetWorkFolder();
        var filesFolder = Path.Combine(workFolder, provider);

        if (!Directory.Exists(filesFolder))
            Directory.CreateDirectory(filesFolder);

        return filesFolder;
    }
    protected void Log(string log) => Output.WriteLine(log);
}
