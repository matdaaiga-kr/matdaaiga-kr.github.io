using MatdaAIga.LinkConverter.Controllers;
using MatdaAIga.LinkConverter.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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