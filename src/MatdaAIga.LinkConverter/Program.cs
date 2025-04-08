using MatdaAIga.LinkConverter.Controllers;
using MatdaAIga.LinkConverter.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// The entry point for the application
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point for the application
    /// </summary>
    /// <param name="args">The command-line arguments</param>
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseConsoleLifetime()
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IConverterService, ConverterService>();
                services.AddScoped<ConverterController>();
            })
            .Build();

        var controller = host.Services.GetRequiredService<ConverterController>();
        await controller.RunAsync(args);
    }
}