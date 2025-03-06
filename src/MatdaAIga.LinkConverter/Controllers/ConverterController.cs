using MatdaAIga.LinkConverter.Models;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Controllers;

public class ConverterController: IConverterController 
{
    public async Task RunAsync(string[] args)
    {
        if (args.Length != 1)
        {
            throw new ArgumentException("Arguments error");
        }
        try
        {
            string inputFilePath = args[0];
            var service = new ConverterServices();
            LinkCollection data = await service.LoadAsync(inputFilePath);
            string markdown = await service.ConvertAsync(data);
            await service.SaveAsync(markdown, inputFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
    }
}