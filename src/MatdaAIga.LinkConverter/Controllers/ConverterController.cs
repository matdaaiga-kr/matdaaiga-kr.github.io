using MatdaAIga.LinkConverter.Services;
using MatdaAIga.LinkConverter.Options;

namespace MatdaAIga.LinkConverter.Controllers;

/// <summary>
/// This represents the controller entity that invokes the conversion workflow
/// </summary>
public class ConverterController(IConverterService service): IConverterController
{
    private readonly IConverterService _service = service ?? throw new ArgumentNullException(nameof(service));

    /// <inheritdoc />
    public async Task RunAsync(string[] args)
    {
        var options = ArgumentOptions.Parse(args);
        if (options.Help)
        {
            this.DisplayHelp();
            return;
        }

        var data = await this._service.LoadAsync(options.Filepath).ConfigureAwait(false);
        var markdown = await this._service.ConvertAsync(data).ConfigureAwait(false);
        await this._service.SaveAsync(markdown, options.Filepath).ConfigureAwait(false);
    }

    /// <summary>
    /// Displays help
    /// </summary>
    private void DisplayHelp()
    {
        Console.WriteLine("Usage >>");
        Console.WriteLine("  -f | --filepath    Specify the absolute filepath of a YAML file to convert");
        Console.WriteLine("  -h | --help     Display help");
    }
}