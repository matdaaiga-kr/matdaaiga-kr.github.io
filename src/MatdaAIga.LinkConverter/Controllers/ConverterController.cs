using MatdaAIga.LinkConverter.Services;
using MatdaAIga.LinkConverter.Options;

namespace MatdaAIga.LinkConverter.Controllers;

public class ConverterController(IConverterService service): IConverterController
{
    private readonly IConverterService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task RunAsync(string[] args)
    {
        var options = ArgumentOptions.Parse(args);
        if (options.Help)
        {
            this.DisplayHelp();
            return;
        }

        string filepath = options.Filepath;

        var data = await this._service.LoadAsync(filepath).ConfigureAwait(false);
        var markdown = await this._service.ConvertAsync(data).ConfigureAwait(false);
        await this._service.SaveAsync(markdown, filepath).ConfigureAwait(false);
    }

    private void DisplayHelp()
    {
        Console.WriteLine("Usage >>");
        Console.WriteLine("  -p, --file-path    Specify the file path to convert");
        Console.WriteLine("  -h, --help     Display help");
    }
}